
namespace WebApplication5.Models
{
    public class ArticleViewModel
    {
        public string StudentName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int FacultyId { get; set; }
        public DateTime UploadDate { get; set; }
        public bool AgreedToTerms { get; set; }
        public string ImageFile { get; set; }
        public string DocumentFile { get; set; }
    }
}
