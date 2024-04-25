using WebApplication5.Models;

namespace WebApplication5.Models
{
    public class ArticleUpdateViewModel
    {
        public Guid Id { get; set; }
        public string StudentName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
