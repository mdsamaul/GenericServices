using DataTable.Interface;
using DataTable.Models;
using DataTable.Service; // এখানে তোমার IGenericService namespace দিতে হবে
using Microsoft.AspNetCore.Mvc;

namespace DataTable.Controllers
{
    public class TeacherController : Controller
    {
        private readonly IGenericService<Teacher> _genericService;

        public TeacherController(IGenericService<Teacher> genericService)
        {
            _genericService = genericService;
        }

        // GET: Teacher
        public IActionResult Index()
        {
            var teachers = _genericService.GetAll();
            return View(teachers);
        }

        // GET: Teacher/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teacher/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _genericService.Add(teacher);
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        // GET: Teacher/Edit/5
        public IActionResult Edit(int id)
        {
            var teacher = _genericService.Get(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // POST: Teacher/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _genericService.Update(id, teacher);
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        // GET: Teacher/Delete/5
        public IActionResult Delete(int id)
        {
            var teacher = _genericService.Get(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // POST: Teacher/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var teacher = _genericService.Get(id);
            if (teacher != null)
            {
                _genericService.Delete(id);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Teacher/Details/5
        public IActionResult Details(int id)
        {
            var teacher = _genericService.Get(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }
    }
}
