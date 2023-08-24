using System.Data;

namespace EmployeeManagementSystem.EmpRepository
{
    public interface IEmpStatus
    {
        public  DataTable GetStatus();
        public DataTable GetBand();
        public DataTable GetAllEmployee();
    }
}
