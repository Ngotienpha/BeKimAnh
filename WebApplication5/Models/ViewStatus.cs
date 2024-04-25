using WebApplication5.Enum;

namespace WebApplication5.Models
{
    public class ViewStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public string Feedback { get; set; }
    }
}
