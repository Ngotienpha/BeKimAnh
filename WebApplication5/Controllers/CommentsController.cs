using Microsoft.AspNetCore.Mvc;
using WebApplication5.Data;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Create(int articleId, Comments comment)
        {
            if (ModelState.IsValid)
            {
                // Lưu ArticleId cho Comment
                comment.ArticleId = articleId;

                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Article", new { id = articleId }); // Điều hướng đến trang chi tiết bài viết
            }
            return View(comment);
        }
    }
}
