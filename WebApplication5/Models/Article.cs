
using WebApplication5.Models;

namespace WebApplication5.Models
{
    public class Article
    {
        public Guid Id { get; set; }
        public string StudentName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int FacultyId { get; set; }
        public DateTime UploadDate { get; set; }
        public bool AgreedToTerms { get; set; }
        public string ImageFile { get; set; }
        public string DocumentFile { get; set; }
        public Faculty Faculty { get; set; }
        public int AcademicYear { get; set; }

        public bool IsPublished { get; set; }

    }
}