using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
namespace WebApplication5.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ManagerController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult DownloadArticle(Guid articleId)
        {
            var article = _context.Articles
                .Include(a => a.Faculty)
                .FirstOrDefault(a => a.Id == articleId);

            if (article != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                    {
                        // Add article information as a text file to the ZIP archive
                        var entry = archive.CreateEntry("Article_Info.txt");
                        using (var writer = new StreamWriter(entry.Open()))
                        {
                            writer.WriteLine($"Id: {article.Id}");
                            writer.WriteLine($"Student Name: {article.StudentName}");
                            writer.WriteLine($"Title: {article.Title}");
                            writer.WriteLine($"Description: {article.Description}");
                            writer.WriteLine($"Upload Date: {article.UploadDate}");
                            writer.WriteLine($"Faculty: {article.Faculty?.Name}");
                            // Add other fields as needed
                        }

                        // Add the document file to the ZIP archive
                        if (!string.IsNullOrEmpty(article.DocumentFile))
                        {
                            var documentPath = Path.Combine(_hostEnvironment.WebRootPath, "Articles", "Documents", article.DocumentFile);
                            if (System.IO.File.Exists(documentPath))
                            {
                                var docEntry = archive.CreateEntry($"Document_{article.DocumentFile}");
                                using (var docStream = docEntry.Open())
                                {
                                    using (var fileStream = System.IO.File.OpenRead(documentPath))
                                    {
                                        fileStream.CopyTo(docStream);
                                    }
                                }
                            }
                        }

                        // Add the image file to the ZIP archive
                        if (!string.IsNullOrEmpty(article.ImageFile))
                        {
                            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Articles", "Images", article.ImageFile);
                            if (System.IO.File.Exists(imagePath))
                            {
                                var imgEntry = archive.CreateEntry($"Image_{article.ImageFile}");
                                using (var imgStream = imgEntry.Open())
                                {
                                    using (var fileStream = System.IO.File.OpenRead(imagePath))
                                    {
                                        fileStream.CopyTo(imgStream);
                                    }
                                }
                            }
                        }
                    } // Ensure the archive is closed before accessing memoryStream
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    // Define the MIME type for the ZIP file
                    var mimeType = "application/zip";
                    // Return the ZIP file as a file download
                    return File(memoryStream.ToArray(), mimeType, $"Article_{article.Id}.zip");
                } // Ensure the memoryStream is closed before returning

            }

            // Handle error or redirection if article not found
            return RedirectToAction("Error");
        }


        [HttpPost]
        public IActionResult RedirectToFacultyPage(string selectedFaculty)
        {
            switch (selectedFaculty)
            {
                case "IT":
                    return RedirectToAction("IT", "Manager");
                case "Business":
                    return RedirectToAction("Business", "Manager");
                case "GraphicDesign":
                    return RedirectToAction("GraphicDesign", "Manager");
                case "Doctor":
                    return RedirectToAction("Doctor", "Manager");
                case "Music":
                    return RedirectToAction("Music", "Manager");
                default:
                    return RedirectToAction("ManagerPage", "Home");
            }
        }

        public IActionResult IT()
        {
            var articles = _context.Articles.Include(a => a.Faculty).Where(a => a.FacultyId == 1).ToList(); // Filter articles for IT faculty (FacultyId == 1)
            var viewModel = new ArticleListViewModel
            {
                Articles = articles
            };
            return View(viewModel);
        }

        public IActionResult Business()
        {
            var articles = _context.Articles.Include(a => a.Faculty).Where(a => a.FacultyId == 2).ToList(); // Filter articles for Business faculty (FacultyId == 2)
            var viewModel = new ArticleListViewModel
            {
                Articles = articles
            };
            return View(viewModel);
        }

        public IActionResult GraphicDesign()
        {
            var articles = _context.Articles.Include(a => a.Faculty).Where(a => a.FacultyId == 3).ToList(); // Filter articles for Design faculty (FacultyId == 3)
            var viewModel = new ArticleListViewModel
            {
                Articles = articles
            };
            return View(viewModel);
        }

        public IActionResult Doctor()
        {
            var articles = _context.Articles.Include(a => a.Faculty).Where(a => a.FacultyId == 4).ToList(); // Filter articles for Design faculty (FacultyId == 3)
            var viewModel = new ArticleListViewModel
            {
                Articles = articles
            };
            return View(viewModel);
        }

        public IActionResult Music()
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
