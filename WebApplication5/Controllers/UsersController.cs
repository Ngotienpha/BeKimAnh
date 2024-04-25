using Microsoft.AspNetCore.Mvc;
using WebApplication5.Data;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext _context;
        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Users.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Users users)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(users);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(users);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var users = _context.Users.FirstOrDefault(x => x.UserId == id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = _context.Users.Find(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }
        [HttpPost]
        public IActionResult Edit(int id, Users users)
        {
            if (id != users.UserId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.Users.Update(users);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(users);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var users = _context.Users.Find(id);
            if (users == null)
            {
                return NotFound();
            }
            _context.Users.Remove(users);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = _context.Users.Find(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }
    }
}
