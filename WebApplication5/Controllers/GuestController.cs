using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class GuestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GuestController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }
        [HttpPost]
        public IActionResult RedirectToFacultyPage(string selectedFaculty)
        {
            switch (selectedFaculty)
            {
                case "IT":
                    return RedirectToAction("ITPublish", "Guest");
                case "Business":
                    return RedirectToAction("BusinessPublish", "Guest");
                case "GraphicDesign":
                    return RedirectToAction("GraphicDesignPublish", "Guest");
                case "Doctor":
                    return RedirectToAction("DoctorPublish", "Guest");
                case "Music":
                    return RedirectToAction("MusicPublish", "Guest");
                default:
                    return RedirectToAction("Guest", "Home");
            }
        }

        public IActionResult ITPublish()
        {
            var articles = _context.Articles.Include(a => a.Faculty).Where(a => a.FacultyId == 1).ToList(); // Filter articles for IT faculty (FacultyId == 1)
            var viewModel = new ArticleListViewModel
            {
                Articles = articles
            };
            return View(viewModel);
        }

        public IActionResult BusinessPublish()
        {
            var articles = _context.Articles.Include(a => a.Faculty).Where(a => a.FacultyId == 2).ToList(); // Filter articles for Business faculty (FacultyId == 2)
            var viewModel = new ArticleListViewModel
            {
                Articles = articles
            };
            return View(viewModel);
        }

        public IActionResult GraphicDesignPublish()
        {
            var articles = _context.Articles.Include(a => a.Faculty).Where(a => a.FacultyId == 3).ToList(); // Filter articles for Design faculty (FacultyId == 3)
            var viewModel = new ArticleListViewModel
            {
                Articles = articles
            };
            return View(viewModel);
        }

        public IActionResult DoctorPublish()
        {
            var articles = _context.Articles.Include(a => a.Faculty).Where(a => a.FacultyId == 4).ToList(); // Filter articles for Design faculty (FacultyId == 3)
            var viewModel = new ArticleListViewModel
            {
                Articles = articles
            };
            return View(viewModel);
        }

        public IActionResult MusicPublish()
        {
            var articles = _context.Articles.Include(a => a.Faculty).Where(a => a.FacultyId == 5).ToList(); // Filter articles for Design faculty (FacultyId == 3)
            var viewModel = new ArticleListViewModel
            {
                Articles = articles
            };
            return View(viewModel);
        }

        public IActionResult GetContributionStatistics()
        {
            var contributionStatistics = new ContributionStatisticsViewModel
            {
                NumberOfContributionsByFaculty = _context.Articles.GroupBy(a => a.FacultyId)
                    .Select(group => new FacultyContribution
                    {
                        FacultyId = group.Key,
                        NumberOfContributions = group.Count()
                    }).ToList(),
                TotalContributorsByFaculty = _context.Articles.GroupBy(a => a.FacultyId)
                    .Select(group => new FacultyContribution
                    {
                        FacultyId = group.Key,
                        NumberOfContributors = group.Select(a => a.StudentName).Distinct().Count()
                    }).ToList()
            };

            // Tính toán tỷ lệ phần trăm của mỗi khoa
            int totalContributions = contributionStatistics.NumberOfContributionsByFaculty.Sum(item => item.NumberOfContributions);
            contributionStatistics.PercentageOfContributionsByFaculty = contributionStatistics.NumberOfContributionsByFaculty
                .Select(item => new FacultyContribution
                {
                    FacultyId = item.FacultyId,
                    Percentage = totalContributions != 0 ? (double)item.NumberOfContributions / totalContributions * 100 : 0
                }).ToList();

            return View("GetContributionStatistics", contributionStatistics);
        }

    }
}
