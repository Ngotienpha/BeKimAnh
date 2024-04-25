using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Enum;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class ViewStatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ViewStatusController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ViewStatus viewStatus)
        {
            if (ModelState.IsValid)
            {
                _context.ViewStatus.Add(viewStatus);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewStatus);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var viewStatus = _context.ViewStatus.FirstOrDefault(x => x.Id == id);
            if (viewStatus == null)
            {
                return NotFound();
            }
            return View(viewStatus);
        }
        public IActionResult Index()
        {
            return View(_context.ViewStatus.ToList());
        }
    }
}
