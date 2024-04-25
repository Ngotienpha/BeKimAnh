using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;
using WebApplication5.Data;


namespace WebApplication5.Controllers
{
    public class MarketingCoordinatorController : Controller
    {

        private readonly ApplicationDbContext _context;

        public MarketingCoordinatorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string faculty, int academicYear)
        {
            var articles = _context.Articles.Include(a => a.Faculty).Where(a => a.FacultyId == 1 && a.AcademicYear == academicYear).ToList(); // Lấy danh sách bài báo cho Faculty IT
            var viewModel = new ArticleListViewModel { Articles = articles };
            return View(viewModel);
        }

        public IActionResult Index1()
        {
            var articles = _context.Articles.Include(a => a.Faculty).Where(a => a.FacultyId == 2).ToList(); // Lấy danh sách bài báo cho Faculty Business
            var viewModel = new ArticleListViewModel { Articles = articles };

            return View(viewModel);
        }

        public IActionResult Index2()
        {
            var articles = _context.Articles.Include(a => a.Faculty).Where(a => a.FacultyId == 3).ToList(); // Lấy danh sách bài báo cho Faculty Graphic Design
            var viewModel = new ArticleListViewModel { Articles = articles };

            return View(viewModel);
        }
        public IActionResult Index3()
        {
            var articles = _context.Articles.Include(a => a.Faculty).Where(a => a.FacultyId == 4).ToList(); // Lấy danh sách bài báo cho Faculty Graphic Design
            var viewModel = new ArticleListViewModel { Articles = articles };

            return View(viewModel);
        }
        public IActionResult Index4()
        {
            var articles = _context.Articles.Include(a => a.Faculty).Where(a => a.FacultyId == 5).ToList(); // Lấy danh sách bài báo cho Faculty Graphic Design
            var viewModel = new ArticleListViewModel { Articles = articles };

            return View(viewModel);
        }

        public IActionResult PublishArticle(Guid articleId)
        {
            var article = _context.Articles.Find(articleId);
            if (article != null)
            {
                article.IsPublished = true;
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "MarketingCoordinator");
        }

        public IActionResult PublishArticle1(Guid articleId)
        {
            var article = _context.Articles.Find(articleId);
            if (article != null)
            {
                article.IsPublished = true;
                _context.SaveChanges();
            }
            return RedirectToAction("Index1", "MarketingCoordinator");
        }

        public IActionResult PublishArticle2(Guid articleId)
        {
            var article = _context.Articles.Find(articleId);
            if (article != null)
            {
                article.IsPublished = true;
                _context.SaveChanges();
            }
            return RedirectToAction("Index2", "MarketingCoordinator");
        }

    }
}