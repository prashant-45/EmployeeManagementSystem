using EmployeeManagementSystem.EmpRepository;
using EmployeeManagementSystem.Models.DAL;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        static IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration= configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult  CheckUserAuth(int Uid, string password) 
        {
            LoginDAL ldal = new LoginDAL(_configuration);
            bool check= ldal.CheckUserAuth(Uid, password);
            if (check)
            {
                return Json(new {success=true ,redirecturl=Url.Action("Index", "Dashboard") });
            }
            else 
            {
                TempData["errormsg"] = "user id or password wrong..!";
            return Json(new {success=false, errormsg= Url.Action("Index","Login") });
            }
        }

        public IActionResult CheckNewUser(int id)
        {
            EmpStatus empStatus = new EmpStatus(_configuration);
            int uid = empStatus.VarifyUser(id);
            if (uid == 1)
            {
                return Json(new { success = true });
            }
            return Json("");
        }

        public IActionResult GenPassword(int Uid, int Npass, int Cnpass)
        {
            EmpStatus empStatus = new EmpStatus(_configuration);
            int check = empStatus.genPassword(Uid, Npass);
            if (check == 1)
            {
                return Json("password created...");
            }
            return Json("something went wrong...");
        }
    }
}
