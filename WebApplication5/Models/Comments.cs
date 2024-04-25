namespace WebApplication5.Models
{
    public class Comments
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreateAt { get; set; }
        public int ArticleId { get; set; }
    }
}
