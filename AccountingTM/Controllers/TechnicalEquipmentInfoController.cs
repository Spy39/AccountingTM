using Accounting.Data;
using AccountingTM.Domain;
using AccountingTM.Domain.Models.Tables;
using AccountingTM.Dto.Common;
using AccountingTM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingTM.Controllers
{
    [Authorize]
    //Информация о ТС
    public class TechnicalEquipmentInfoController : Controller
    {
        private readonly DataContext _context;

        public TechnicalEquipmentInfoController(DataContext context)
        {
            _context = context;
        }

        //Редактирование основной информации ТС
        [HttpGet]
        public IActionResult Get(int id)
        {
            var entity = _context.TechnicalEquipment
                        .Include(x => x.Type)
                        .Include(x => x.Brand)
                        .Include(x => x.Model)
                        .FirstOrDefault(x => x.Id == id);

            if (entity == null)
            {
                throw new Exception($"ТС с id = {id} не найдено");
            }

            return Ok(entity);
        }

        [HttpPost]
        public IActionResult Update([FromBody] TechnicalEquipment input)
        {
            // Находим объект в БД (теперь без AsNoTracking)
            var entity = _context.TechnicalEquipment
                        .AsNoTracking()
                        .FirstOrDefault(x => x.Id == input.Id);


            if (entity == null)
            {
                throw new Exception($"ТС с id = {input.Id} не найдено");
            }

            // Теперь переносим нужные поля из input 
            // (то, что пользователь реально редактирует)
            entity.TypeId = input.TypeId;
            entity.BrandId = input.BrandId;
            entity.ModelId = input.ModelId;
            entity.State = input.State;

            // Сохраняем
            _context.TechnicalEquipment.Update(input);
            _context.SaveChanges();


            return Ok();
        }


        //Редактирование дополнительной информации ТС
        [HttpGet]
        public IActionResult GetAditional(int id)
        {
            var entity = _context.TechnicalEquipment.Find(id);
            if (entity == null)
            {
                throw new Exception($"ТС с id = {id} не найдено");
            }

            return Ok(entity);
        }

        [HttpPost]
        public IActionResult UpdateAditional([FromBody] TechnicalEquipment input)
        {
            if (input == null)
                return BadRequest("Ошибка: входные данные пустые.");

            if (input.Id == 0)
                return BadRequest("Ошибка: ID технического средства не передан.");

            var entity = _context.TechnicalEquipment.Find(input.Id);

            if (entity == null)
            {
                Console.WriteLine($"Ошибка: ТС с id = {input.Id} не найдено");
                return NotFound($"ТС с id = {input.Id} не найдено");
            }

            // Копируем нужные поля
            entity.SerialNumber = input.SerialNumber;
            entity.InventoryNumber = input.InventoryNumber;
            entity.EmployeeId = input.EmployeeId;
            entity.LocationId = input.LocationId;

            // Проверка формата дат перед сохранением
            if (DateTime.TryParse(input.Date?.ToString(), out var date)) entity.Date = date;
            if (DateTime.TryParse(input.DateStart?.ToString(), out var dateStart)) entity.DateStart = dateStart;
            if (DateTime.TryParse(input.DateEnd?.ToString(), out var dateEnd)) entity.DateEnd = dateEnd;
            if (DateTime.TryParse(input.DateGarant?.ToString(), out var dateGarant)) entity.DateGarant = dateGarant;

            _context.SaveChanges();

            return Ok();
        }



        //Характеристики технического средства
        [HttpGet]
        public IActionResult GetAllCharacteristic([FromQuery] SearchPagedRequestDto input, int technicalEquipmentId)
        {
            IQueryable<Characteristic> query = _context.Characteristics.Include(x => x.Unit).Include(x => x.Indicator).Where(x => x.TechnicalEquipmentId == technicalEquipmentId);

            var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return Ok(new PagedResultDto<Characteristic>(query.Count(), entities));
        }

        [HttpPost]
        public IActionResult CreateCharacteristic([FromBody] Characteristic input)
        {
            _context.Characteristics.Add(input);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteCharacteristic(int id)
        {
            var entity = _context.Characteristics.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Characteristics.Remove(entity);
            _context.SaveChanges();
            return Ok();
        }


        //Консервация
        [HttpGet]
        public IActionResult GetAllConservation([FromQuery] SearchPagedRequestDto input, int technicalEquipmentId)
        {
            IQueryable<Conservation> query = _context.Conservations.Include(x => x.Employee).Where(x => x.TechnicalEquipmentId == technicalEquipmentId);

            var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return Ok(new PagedResultDto<Conservation>(query.Count(), entities));
        }

        [HttpPost]
        public IActionResult CreateConservation([FromBody] Conservation input)
        {
            input.Date += TimeSpan.FromHours(3);
            _context.Conservations.Add(input);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteConservation(int id)
        {
            var entity = _context.Conservations.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Conservations.Remove(entity);
            _context.SaveChanges();
            return Ok();
        }


        //Прием и передача изделия
        [HttpGet]
        public IActionResult GetAllReceptionAndTransmission([FromQuery] SearchPagedRequestDto input, int technicalEquipmentId)
        {
            IQueryable<ReceptionAndTransmission> query = _context.ReceptionAndTransmissions.Where(x => x.TechnicalEquipmentId == technicalEquipmentId); ;

            var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return Ok(new PagedResultDto<ReceptionAndTransmission>(query.Count(), entities));
        }

        [HttpPost]
        public IActionResult CreateReceptionAndTransmission([FromBody] ReceptionAndTransmission input)
        {
            input.Date += TimeSpan.FromHours(3);
            _context.ReceptionAndTransmissions.Add(input);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteReceptionAndTransmission(int id)
        {
            var entity = _context.ReceptionAndTransmissions.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.ReceptionAndTransmissions.Remove(entity);
            _context.SaveChanges();
            return Ok();
        }


        //Ремонт
        [HttpGet]
        public IActionResult GetAllRepair([FromQuery] SearchPagedRequestDto input, int technicalEquipmentId)
        {
            IQueryable<Repair> query = _context.Repairs.Where(x => x.TechnicalEquipmentId == technicalEquipmentId); ;

            var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return Ok(new PagedResultDto<Repair>(query.Count(), entities));
        }

        [HttpPost]
        public IActionResult CreateRepair([FromBody] Repair input)
        {
            input.Date += TimeSpan.FromHours(3);
            _context.Repairs.Add(input);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteRepair(int id)
        {
            var entity = _context.Repairs.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Repairs.Remove(entity);
            _context.SaveChanges();
            return Ok();
        }


        //Хранение
        [HttpGet]
        public IActionResult GetAllStorage([FromQuery] SearchPagedRequestDto input, int technicalEquipmentId)
        {
            IQueryable<Storage> query = _context.Storages.Where(x => x.TechnicalEquipmentId == technicalEquipmentId); ;

            var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return Ok(new PagedResultDto<Storage>(query.Count(), entities));
        }

        [HttpPost]
        public IActionResult CreateStorage([FromBody] Storage input)
        {
            input.Acceptance += TimeSpan.FromHours(3);
            input.Removal += TimeSpan.FromHours(3);
            _context.Storages.Add(input);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteStorage(int id)
        {
            var entity = _context.Storages.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Storages.Remove(entity);
            _context.SaveChanges();
            return Ok();
        }
    }
}