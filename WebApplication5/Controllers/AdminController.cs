using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Models;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Admin/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Admin/Create
    [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("Id,Name")] Faculty faculty)
{
    if (ModelState.IsValid)
    {
        // Kiểm tra xem ID đã tồn tại trong bảng Faculty hay chưa
        var existingFaculty = await _context.Faculty.FindAsync(faculty.Id);

        if (existingFaculty == null)
        {
            // Nếu ID chưa tồn tại, thêm mới Faculty với ID cố định
            _context.Add(faculty);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        else
        {
            // Nếu ID đã tồn tại, có thể xử lý hoặc bỏ qua tùy thuộc vào yêu cầu của bạn
            // Ví dụ: thông báo lỗi, bỏ qua bản ghi, cập nhật thông tin, ...
        }
    }
    return View(faculty);
}



    public async Task<IActionResult> Index()
    {
        var faculties = await _context.Faculty.ToListAsync();
        return View(faculties);
    }

    public IActionResult Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var faculties = _context.Faculty.FirstOrDefault(x => x.Id == id);
        if (faculties == null)
        {
            return NotFound();
        }
        return View(faculties);
    }


    // GET: Admin/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var faculty = await _context.Faculty.FindAsync(id);
        if (faculty == null)
        {
            return NotFound();
        }

        return View(faculty);
    }

    // POST: Admin/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Faculty faculty)
    {
        if (id != faculty.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(faculty);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacultyExists(faculty.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(faculty);
    }

    private bool FacultyExists(int id)
    {
        return _context.Faculty.Any(e => e.Id == id);
    }

    // GET: Admin/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var faculty = await _context.Faculty
            .FirstOrDefaultAsync(m => m.Id == id);
        if (faculty == null)
        {
            return NotFound();
        }

        return View(faculty);
    }

    // POST: Admin/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var faculty = await _context.Faculty.FindAsync(id);
        _context.Faculty.Remove(faculty);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


}
