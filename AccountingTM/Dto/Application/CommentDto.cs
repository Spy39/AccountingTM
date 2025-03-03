namespace AccountingTM.Dto.Application
{
    public class CommentDto
    {
        public int ApplicationId { get; set; }
        public string Text { get; set; }
        public string PathToFile { get; set; } // Если файл прикрепляется
        public string Author { get; set; } // 🔹 Добавлено поле автора комментария
    }
}
