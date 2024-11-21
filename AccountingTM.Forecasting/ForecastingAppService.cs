using Microsoft.ML;
using Microsoft.ML.Data;

namespace AccountingTM.Forecasting
{
	public class ForecastingAppService
	{
		public float AnalysisConsumable(ConsumableAnalisisModel input)
		{
			List<string> types = new List<string> { "Cartridge", "Batteries", "Lamps", "Accumulators", "Cables" };

			// Бренды и модели
			List<string> brands = new List<string> { "HP", "Sony", "Philips", "Duracell", "Samsung" };
			List<string> models = new List<string> { "ModelX", "ModelY", "ModelZ", "ModelW", "ModelQ" };

			// Создание списка расходных материалов
			List<ConsumableAnalisisModel> consumables = new List<ConsumableAnalisisModel>();

			// Генерация данных за каждый месяц с 2021 по 2023 год
			Random random = new Random();
			for (int year = 2021; year <= 2023; year++)
			{
				for (int month = 1; month <= 12; month++)
				{
					for (int i = 0; i < 5; i++) // 5 записей на каждый месяц
					{
						consumables.Add(new ConsumableAnalisisModel
						{
							TypeConsumable = types[random.Next(types.Count)],
							Brand = brands[random.Next(brands.Count)],
							Model = models[random.Next(models.Count)],
							Quantity = random.Next(10, 500), // Количество от 10 до 500
							Mounth = month,
							Year = year
						});
					}
				}
			}

			var context = new MLContext();
			var dataView = context.Data.LoadFromEnumerable(consumables);

			var pipeline = context.Transforms.Conversion.MapValueToKey(outputColumnName: "TypeConsumableKey", inputColumnName: "TypeConsumable")
				.Append(context.Transforms.Conversion.MapValueToKey(outputColumnName: "BrandKey", inputColumnName: "Brand"))
				.Append(context.Transforms.Conversion.MapValueToKey(outputColumnName: "ModelKey", inputColumnName: "Model"))

				// Преобразуем ключевые столбцы в One-Hot кодирование
				.Append(context.Transforms.Categorical.OneHotEncoding(outputColumnName: "TypeConsumableEncoded", inputColumnName: "TypeConsumableKey"))
				.Append(context.Transforms.Categorical.OneHotEncoding(outputColumnName: "BrandEncoded", inputColumnName: "BrandKey"))
				.Append(context.Transforms.Categorical.OneHotEncoding(outputColumnName: "ModelEncoded", inputColumnName: "ModelKey"))

				// Преобразуем месяц и год в тип Single (float)
				.Append(context.Transforms.Conversion.ConvertType(outputColumnName: "MounthFloat", inputColumnName: "Mounth", outputKind: DataKind.Single))
				.Append(context.Transforms.Conversion.ConvertType(outputColumnName: "YearFloat", inputColumnName: "Year", outputKind: DataKind.Single))

				// Объединяем закодированные значения с другими признаками (месяц и год)
				.Append(context.Transforms.Concatenate("Features", "TypeConsumableEncoded", "BrandEncoded", "ModelEncoded", "MounthFloat", "YearFloat"))

				// Обучаем регрессионную модель с использованием FastTree
				.Append(context.Regression.Trainers.FastTree(labelColumnName: "Quantity", featureColumnName: "Features"));

			//Обучение модели по данным dataView
			var model = pipeline.Fit(dataView);


			var predictionEngine = context.Model.CreatePredictionEngine<ConsumableAnalisisModel, ConsumablePrediction>(model);



			var prediction = predictionEngine.Predict(input);

			return prediction.Quantity;
		}
	}
}
