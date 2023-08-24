using EmployeeManagementSystem.Models.EmpModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeManagementSystem.EmpRepository
{
    public class EmpStatus : IEmpStatus
    {
        static IConfiguration _configuration;
        public EmpStatus()
        {
            
        }
        public EmpStatus(IConfiguration configuration)
        {
            _configuration= configuration;
        }  
        public  DataTable GetStatus()
        {
            DataTable dt = new DataTable();
            string con=_configuration.GetConnectionString("defaultConnection");
            SqlConnection sqlConnection = new SqlConnection(con);
            string sql = "select EmpStatus from Emp_Status";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            cmd.CommandType=System.Data.CommandType.Text;
            sqlConnection.Open();
            //SqlDataReader reader = cmd.ExecuteReader();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            sqlConnection.Close();
            da.Fill(dt);
            return dt;
        }
        

        public DataTable GetBand()
        {
            DataTable dt = new DataTable();
            string connection = _configuration.GetConnectionString("defaultConnection");
            string str = "select Band from Emp_Band";
            SqlConnection con = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType= System.Data.CommandType.Text;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Close();
            da.Fill(dt);
            return dt;
        }

        public DataTable GetAllEmployee() 
        {
            DataTable dt = new DataTable();
            string connection = _configuration.GetConnectionString("defaultConnection");
            SqlConnection con = new SqlConnection(connection);
            string str = "select emp_id,emp_status, REPLACE(CONVERT(VARCHAR(15), Dob,106) , ' ','-') As Dob,name,Sex,P_Address,P_Zip,P_State,P_Country,MobileNo,Email_Id from Emp_mst";
            SqlCommand cmd = new SqlCommand(str, con);
            con.Open() ;
            SqlDataAdapter da = new SqlDataAdapter(cmd) ;
            con.Close() ;
            da.Fill(dt);
            return dt;
        }
        public List<EmpDetails> GetEmployee()
        {
            List<EmpDetails> list = new List<EmpDetails>();
            DataTable dt = GetAllEmployee();
            if (dt.Rows.Count>0) 
            {
                foreach (DataRow dr in dt.Rows)
                {
                    EmpDetails emp = new EmpDetails();
                    emp.Id =Convert.ToInt32(dr["Emp_id"]);
                    emp.Name = dr["name"].ToString();
                    emp.Status = dr["emp_status"].ToString();
                    emp.Dob = Convert.ToString(dr["Dob"]);
                    emp.Sex = dr["Sex"].ToString();
                    emp.Address = dr["P_Address"].ToString();
                    emp.ZipCode = Convert.ToInt32(dr["P_Zip"]);
                    emp.State = Convert.ToString(dr["P_State"]);
                    emp.Country = dr["P_Country"].ToString();
                    emp.MobileNo =Convert.ToInt32(dr["MobileNo"]);
                    emp.Email =  dr["Email_Id"].ToString();
                    list.Add(emp);  
                }
            }
            return list;

        
        }

        public bool createEmp(EmpDetails emp) 
        {
            bool check = false;
            string connection = _configuration.GetConnectionString("defaultConnection");
            SqlConnection con = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand("spCreateEmp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name",emp.Name);
            cmd.Parameters.AddWithValue("@emp_status", emp.Status);
            cmd.Parameters.AddWithValue("@dob", emp.Dob);
            cmd.Parameters.AddWithValue("@sex", emp.Sex);
            cmd.Parameters.AddWithValue("@pAddress", emp.Address);
            cmd.Parameters.AddWithValue("@State", emp.State);
            cmd.Parameters.AddWithValue("@country", emp.Country);
            cmd.Parameters.AddWithValue("@mobile", emp.MobileNo);
            cmd.Parameters.AddWithValue("@ZipCode",emp.ZipCode);
            cmd.Parameters.AddWithValue("@email", emp.Email);
            cmd.Parameters.AddWithValue("@RHead", emp.RHead);
            cmd.Parameters.AddWithValue("@Role", emp.Role);
            con.Open();
            int i= cmd.ExecuteNonQuery();
            if (i > 0)
            {
                check = true;
            }
            return check;
        }
        public int VarifyUser(int id) 
        {
            int uid = 0;
            string connection = _configuration.GetConnectionString("defaultConnection");
            SqlConnection con = new SqlConnection(connection);
            string str = "select count(*) from emp_mst where emp_id = "+id+" and Password is null ";
            SqlCommand cmd = new SqlCommand(str,con);
            cmd.CommandType= CommandType.Text;
            con.Open();
            SqlDataReader dr= cmd.ExecuteReader();
            if (dr.Read()) 
            {
                uid = Convert.ToInt32(dr[0]);
            }
            return uid;
        }

        public int genPassword(int uid, int npass) 
        {
            string connection = _configuration.GetConnectionString("defaultConnection");
            SqlConnection con = new SqlConnection(connection);
            string sql = "update emp_mst set Password= "+npass+" where emp_id ="+uid+"   ";
            SqlCommand cmd = new SqlCommand(sql,con);
            cmd.CommandType= CommandType.Text;
            con.Open();
            int i = cmd.ExecuteNonQuery();
            return i;
        }
    }
}
