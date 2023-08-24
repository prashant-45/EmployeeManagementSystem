using EmployeeManagementSystem.Models.EmpModel;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetEmpDetails( ) 
        {
            EmpDetails empDetails = new EmpDetails() 
            {
            Id = 1,
            Name= "Test",
            };   

            return Json(empDetails);
        }
    }
}
