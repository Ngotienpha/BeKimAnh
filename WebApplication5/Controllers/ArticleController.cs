using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ArticleController(ApplicationDbContext applicationDbContext, IWebHostEnvironment webHostEnvironment)
        {
            this.applicationDbContext = applicationDbContext;
            this._webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var article = await applicationDbContext.Articles.ToListAsync();
            return View(article);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var article = applicationDbContext.Articles.FirstOrDefault(x => x.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ArticleViewModel addArticleRequest, IFormFile imageFile, IFormFile documentFile)
        {
            if (!addArticleRequest.AgreedToTerms)
            {
                ModelState.AddModelError("AgreedToTerms", "You must agree to the terms and conditions.");
                return View(addArticleRequest);
            }

            var article = new Article()
            {
                Id = Guid.NewGuid(),
                StudentName = addArticleRequest.StudentName,
                Title = addArticleRequest.Title,
                Description = addArticleRequest.Description,
                FacultyId = addArticleRequest.FacultyId,
                UploadDate = addArticleRequest.UploadDate,
                AgreedToTerms = addArticleRequest.AgreedToTerms,
                IsPublished = false,

            };

            if (imageFile != null && imageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Articles/Images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                    article.ImageFile = uniqueFileName;
                }
            }

            // Lưu trữ tài liệu nếu có
            if (documentFile != null && documentFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Articles/Documents");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + documentFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await documentFile.CopyToAsync(fileStream);
                    article.DocumentFile = uniqueFileName;
                }
            }

            await applicationDbContext.Articles.AddAsync(article);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction("StudentPage","Home");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var article = await applicationDbContext.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            var editViewModel = new ArticleUpdateViewModel
            {
                Id = article.Id,
                StudentName = article.StudentName,
                Title = article.Title,
                Description = article.Description,
                UploadDate = article.UploadDate
            };

            return View(editViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ArticleUpdateViewModel editArticleRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(editArticleRequest);
            }

            var article = await applicationDbContext.Articles.FindAsync(editArticleRequest.Id);
            if (article == null)
            {
                return NotFound();
            }

            article.StudentName = editArticleRequest.StudentName;
            article.Title = editArticleRequest.Title;
            article.Description = editArticleRequest.Description;
            article.UploadDate = editArticleRequest.UploadDate;

            applicationDbContext.Articles.Update(article);
            await applicationDbContext.SaveChangesAsync();

            return RedirectToAction("Index", "MarketingCoordinator");
        }

        [HttpGet]
        public async Task<IActionResult> Edit1(Guid id)
        {
            var article = await applicationDbContext.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            var editViewModel = new ArticleUpdateViewModel
            {
                Id = article.Id,
                StudentName = article.StudentName,
                Title = article.Title,
                Description = article.Description,
                UploadDate = article.UploadDate
            };

            return View(editViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit1(ArticleUpdateViewModel editArticleRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(editArticleRequest);
            }

            var article = await applicationDbContext.Articles.FindAsync(editArticleRequest.Id);
            if (article == null)
            {
                return NotFound();
            }

            article.StudentName = editArticleRequest.StudentName;
            article.Title = editArticleRequest.Title;
            article.Description = editArticleRequest.Description;
            article.UploadDate = editArticleRequest.UploadDate;

            applicationDbContext.Articles.Update(article);
            await applicationDbContext.SaveChangesAsync();

            return RedirectToAction("Index1", "MarketingCoordinator");
        }

        [HttpGet]
        public async Task<IActionResult> Edit2(Guid id)
        {
            var article = await applicationDbContext.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            var editViewModel = new ArticleUpdateViewModel
            {
                Id = article.Id,
                StudentName = article.StudentName,
                Title = article.Title,
                Description = article.Description,
                UploadDate = article.UploadDate
            };

            return View(editViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit2(ArticleUpdateViewModel editArticleRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(editArticleRequest);
            }

            var article = await applicationDbContext.Articles.FindAsync(editArticleRequest.Id);
            if (article == null)
            {
                return NotFound();
            }

            article.StudentName = editArticleRequest.StudentName;
            article.Title = editArticleRequest.Title;
            article.Description = editArticleRequest.Description;
            article.UploadDate = editArticleRequest.UploadDate;

            applicationDbContext.Articles.Update(article);
            await applicationDbContext.SaveChangesAsync();

            return RedirectToAction("Index2", "MarketingCoordinator");
        }
        [HttpGet]
        public async Task<IActionResult> Edit3(Guid id)
        {
            var article = await applicationDbContext.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            var editViewModel = new ArticleUpdateViewModel
            {
                Id = article.Id,
                StudentName = article.StudentName,
                Title = article.Title,
                Description = article.Description,
                UploadDate = article.UploadDate
            };

            return View(editViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit3(ArticleUpdateViewModel editArticleRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(editArticleRequest);
            }

            var article = await applicationDbContext.Articles.FindAsync(editArticleRequest.Id);
            if (article == null)
            {
                return NotFound();
            }

            article.StudentName = editArticleRequest.StudentName;
            article.Title = editArticleRequest.Title;
            article.Description = editArticleRequest.Description;
            article.UploadDate = editArticleRequest.UploadDate;

            applicationDbContext.Articles.Update(article);
            await applicationDbContext.SaveChangesAsync();

            return RedirectToAction("Index1", "MarketingCoordinator");
        }
        [HttpGet]
        public async Task<IActionResult> Edit4(Guid id)
        {
            var article = await applicationDbContext.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            var editViewModel = new ArticleUpdateViewModel
            {
                Id = article.Id,
                StudentName = article.StudentName,
                Title = article.Title,
                Description = article.Description,
                UploadDate = article.UploadDate
            };

            return View(editViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit4(ArticleUpdateViewModel editArticleRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(editArticleRequest);
            }

            var article = await applicationDbContext.Articles.FindAsync(editArticleRequest.Id);
            if (article == null)
            {
                return NotFound();
            }

            article.StudentName = editArticleRequest.StudentName;
            article.Title = editArticleRequest.Title;
            article.Description = editArticleRequest.Description;
            article.UploadDate = editArticleRequest.UploadDate;

            applicationDbContext.Articles.Update(article);
            await applicationDbContext.SaveChangesAsync();

            return RedirectToAction("Index1", "MarketingCoordinator");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmDelete(Guid id)
        {
            var article = await applicationDbContext.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var article = await applicationDbContext.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            applicationDbContext.Articles.Remove(article);
            await applicationDbContext.SaveChangesAsync();

            return RedirectToAction("Index", "MarketingCoordinator");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmDelete1(Guid id)
        {
            var article = await applicationDbContext.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmDelete2(Guid id)
        {
            var article = await applicationDbContext.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }


        [HttpPost]
        public async Task<IActionResult> Delete1(Guid id)
        {
            var article = await applicationDbContext.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            applicationDbContext.Articles.Remove(article);
            await applicationDbContext.SaveChangesAsync();

            return RedirectToAction("Index1", "MarketingCoordinator");
        }

        [HttpPost]
        public async Task<IActionResult> Delete2(Guid id)
        {
            var article = await applicationDbContext.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            applicationDbContext.Articles.Remove(article);
            await applicationDbContext.SaveChangesAsync();

            return RedirectToAction("Index2", "MarketingCoordinator");
        }
        [HttpPost]
        public async Task<IActionResult> Delete3(Guid id)
        {
            var article = await applicationDbContext.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            applicationDbContext.Articles.Remove(article);
            await applicationDbContext.SaveChangesAsync();

            return RedirectToAction("Index1", "MarketingCoordinator");
        }
        [HttpPost]
        public async Task<IActionResult> Delete4(Guid id)
        {
            var article = await applicationDbContext.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            applicationDbContext.Articles.Remove(article);
            await applicationDbContext.SaveChangesAsync();

            return RedirectToAction("Index1", "MarketingCoordinator");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmDelete3(Guid id)
        {
            var article = await applicationDbContext.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmDelete4(Guid id)
        {
            var article = await applicationDbContext.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }



    }
}
