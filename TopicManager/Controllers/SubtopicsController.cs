using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TopicManager.Data;
using TopicManager.Models;

namespace TopicManager.Controllers
{
    public class SubtopicsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubtopicsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Subtopics/Index?subjectId=1
        public async Task<IActionResult> Index(int subjectId)
        {
            var subject = await _context.Subjects
                                        .Include(s => s.Subtopics)
                                        .FirstOrDefaultAsync(s => s.Id == subjectId);

            if (subject == null) return NotFound();

            ViewBag.SubjectName = subject.Name;
            ViewBag.SubjectId = subjectId;
            return View(subject.Subtopics);
        }

        // GET: Subtopics/Create?subjectId=1
        public IActionResult Create(int subjectId)
        {
            ViewBag.SubjectId = subjectId;
            return View();
        }

        // POST: Subtopics/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,SubjectId")] Subtopic subtopic)
        {
            if (ModelState.IsValid)
            {
                _context.Subtopics.Add(subtopic);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { subjectId = subtopic.SubjectId });
            }
            ViewBag.SubjectId = subtopic.SubjectId;
            return View(subtopic);
        }

        // GET: Subtopics/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var subtopic = await _context.Subtopics.FindAsync(id);
            if (subtopic == null) return NotFound();

            ViewBag.SubjectId = subtopic.SubjectId;
            return View(subtopic);
        }

        // POST: Subtopics/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,SubjectId")] Subtopic subtopic)
        {
            if (id != subtopic.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subtopic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubtopicExists(subtopic.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction("Index", new { subjectId = subtopic.SubjectId });
            }
            ViewBag.SubjectId = subtopic.SubjectId;
            return View(subtopic);
        }

        // GET: Subtopics/Delete/{id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var subtopic = await _context.Subtopics.FindAsync(id);
            if (subtopic == null) return NotFound();

            return View(subtopic);
        }

        // POST: Subtopics/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subtopic = await _context.Subtopics.FindAsync(id);
            if (subtopic != null)
            {
                _context.Subtopics.Remove(subtopic);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", new { subjectId = subtopic.SubjectId });
        }

        private bool SubtopicExists(int id)
        {
            return _context.Subtopics.Any(e => e.Id == id);
        }
    }
}

