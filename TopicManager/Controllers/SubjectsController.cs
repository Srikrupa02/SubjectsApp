using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TopicManager.Data;
using TopicManager.Models;
namespace TopicManager.Controllers
{
    //public class SubjectsController : Controller
    //{
    //    public IActionResult Index()
    //    {
    //        return View();
    //    }
    //}
        public class SubjectsController : Controller
        {
            private readonly ApplicationDbContext _context;

            public SubjectsController(ApplicationDbContext context)
            {
                _context = context;
            }

            // GET: Subjects
            public async Task<IActionResult> Index()
            {
                return View(await _context.Subjects.Include(s => s.Subtopics).ToListAsync());
            }

            
            // GET: Subjects/Create
            public IActionResult Create()
            {
                return View();
            }

        // POST: Subjects/Create
        [HttpPost]
        public async Task<IActionResult> Create(Subject subject)
        {
            if (ModelState.IsValid)
            {
                _context.Subjects.Add(subject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        // GET: Subjects/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
            {
                if (id == null) return NotFound();

                var subject = await _context.Subjects.FindAsync(id);
                if (subject == null) return NotFound();

                return View(subject);
            }

            // POST: Subjects/Edit/{id}
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Subject subject)
            {
                if (id != subject.Id) return NotFound();

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(subject);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!SubjectExists(subject.Id)) return NotFound();
                        else throw;
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(subject);
            }

            // GET: Subjects/Delete/{id}
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null) return NotFound();

                var subject = await _context.Subjects.FindAsync(id);
                if (subject == null) return NotFound();

                return View(subject);
            }

            // POST: Subjects/Delete/{id}
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var subject = await _context.Subjects.FindAsync(id);
                if (subject != null)
                {
                    _context.Subjects.Remove(subject);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects
                .Include(s => s.Subtopics)  // Include subtopics
                .FirstOrDefaultAsync(m => m.Id == id);

            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        //[HttpGet("GetSubjectsWithSubtopics")]
        //public async Task<IActionResult> GetSubjectsWithSubtopics()
        //{
        //    var subjects = await _context.Subjects
        //        .Include(s => s.Subtopics)
        //        .ToListAsync();

        //    return Ok(subjects);
        //}
        private bool SubjectExists(int id)
            {
                return _context.Subjects.Any(e => e.Id == id);
            }
        }
    }
