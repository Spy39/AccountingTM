using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingTM.Domain.Models
{
    public class CommentsOnTheApplication : Entity
    {
        public int ApplicationId { get; set; }
        [ForeignKey(nameof(ApplicationId))]
        public Application? Application { get; set; }
        /// <summary>Дата</summary>
        public DateTime Date { get; set; } = DateTime.Now;
        /// <summary>Текст комментария</summary>
        public string Text { get; set; }
        /// <summary>Путь до файла</summary>
        public string PathToFile { get; set; }
    }
}
