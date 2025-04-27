using DataTable.Data;
using DataTable.Interface;
using DataTable.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataTable.Controllers
{
    public class StudentController : Controller
    {
        private readonly IGenericService<Student> _genericService;

        public StudentController(IGenericService<Student> genericService)
        {
            _genericService = genericService;
        }

        public IActionResult Index()
        {
            // সব Students নিয়ে আসবো
            var students = _genericService.GetAll();

            return View(students); // View এ পাঠাবো
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                var result = _genericService.Add(student);
                TempData["Message"] = result;
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
