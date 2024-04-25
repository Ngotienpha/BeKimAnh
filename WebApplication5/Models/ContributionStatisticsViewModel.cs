namespace WebApplication5.Models
{
    public class ContributionStatisticsViewModel
    {
        public List<FacultyContribution> NumberOfContributionsByFaculty { get; set; }
        public List<FacultyContribution> PercentageOfContributionsByFaculty { get; set; }
        public List<FacultyContribution> TotalContributorsByFaculty { get; set; }
    }

    public class FacultyContribution
    {
        public int FacultyId { get; set; }
        public int NumberOfContributions { get; set; }
        public double Percentage { get; set; }
        public int NumberOfContributors { get; set; }
    }
}
