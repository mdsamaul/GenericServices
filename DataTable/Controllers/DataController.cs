using DataTable.Interface;
using DataTable.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataTable.Controllers
{
    public class DataController : Controller
    {
        private readonly IDataService _dataService;

        public DataController(IDataService dataService)
        {
            _dataService = dataService;
        }

        // Display the main view
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // Generic method to get data for any entity type
        [HttpGet]
        public async Task<IActionResult> GetDataAll<T>() where T : class
        {
            var data = await _dataService.GetDataAsync<T>();
            return Json(data);
        }

        // Method to get data based on table name
        [HttpGet]
        public async Task<IActionResult> GetData(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                return BadRequest("Table name cannot be empty");
            }

            object data = null;

            // Determine which entity to retrieve data for
            switch (tableName)
            {
                case "Employees":
                    data = await _dataService.GetDataAsync<Employee>();
                    break;
                case "Departments":
                    data = await _dataService.GetDataAsync<Department>();
                    break;
                case "Projects":
                    data = await _dataService.GetDataAsync<Project>();
                    break;
                case "Salaries":
                    data = await _dataService.GetDataAsync<Salary>();
                    break;
                case "EmployeeProjects":
                    data = await _dataService.GetDataAsync<EmployeeProject>();
                    break;
                default:
                    return NotFound($"Table '{tableName}' not supported");
            }

            return Json(data);
        }
    }
}