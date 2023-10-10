using EmployeeManagementSystem.EmpRepository;
using EmployeeManagementSystem.Models.EmpModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using System.Data;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeeController : Controller 
    {
        static IConfiguration _configuration;
        public EmployeeController(IConfiguration configuration )
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            EmployeeJoining();
            return View();
        }

        public IActionResult EmployeeJoining()
        {
            EmpStatus empStatus=new EmpStatus(_configuration);
            ViewBag.Status= GetStatusName();
            ViewBag.Band= GetBandName();

            return View();
        }
        [HttpPost]
        public IActionResult EmployeeJoining([FromBody]EmpDetails emp)
        {
            if (ModelState.IsValid) 
            {
                string msg = "";
                EmpStatus empStatus = new EmpStatus(_configuration);


                bool check = empStatus.createEmp(emp);
                if (check) 
                {
                    msg = "employee created..!";
                }
                else
                {
                    msg = "something went wrong..!";
                }
                return  Json(new { success = true, msg });
            }
            return Json("error occured");
        }

        public SelectList GetBandName()
        {
            EmpStatus empStatus = new EmpStatus(_configuration);
            List<SelectListItem> list = new List<SelectListItem>();
            DataTable dt = empStatus.GetBand();
            list.Add(new SelectListItem { Text = "--select--", Value = "" });
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new SelectListItem { Text = Convert.ToString(dr["Band"]), Value = Convert.ToString(dr["Band"]) });
                }
            }
            return new SelectList(list, "Value", "Text");
        }
        public SelectList GetStatusName()
        {
            EmpStatus empStatus = new EmpStatus(_configuration);
            List<SelectListItem> list = new List<SelectListItem>();
            DataTable dt = empStatus.GetStatus();
            list.Add(new SelectListItem { Value = "", Text = "--select--" });
            if (dt.Rows.Count > 0)
            {
                 foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new SelectListItem { Text = Convert.ToString(dr["EmpStatus"]), Value = Convert.ToString(dr["EmpStatus"]) });
                }
            }
            return new SelectList(list, "Value", "Text");
        }

        

        public IActionResult EmployeeList()
        {
            return View();
        }
        public IActionResult GetEmployeeList()
        {
            EmpStatus empStatus = new EmpStatus(_configuration);
            List<EmpDetails> list= empStatus.GetEmployee();
            return Json(list);
        }

        public IActionResult GetSingleEmp(int id) 
        {
            EmpStatus empStatus = new EmpStatus(_configuration);
            ViewBag.Band = GetBandName();
            EmpDetails data= empStatus.GetSingleEmployee(id);
            return Json(data);
        }
        public IActionResult BindBand()
        {
            var asdf= GetBandName();
            return Json(asdf);
        }
        public IActionResult BindStatus()
        {
            var asdf = GetStatusName();
            return Json(asdf);
        }
        public IActionResult DeleteEmp(int[] item)
        {
            EmpStatus empStatus= new EmpStatus(_configuration);
            int asdf = empStatus.DeleteEmployee(item);
            if (asdf==1) 
            {
                return Json("Delete successfully...");
            }
            return Json("Error occur");
        }
    }
}
