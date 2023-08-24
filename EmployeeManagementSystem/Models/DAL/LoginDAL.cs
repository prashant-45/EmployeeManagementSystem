using System.Data.SqlClient;

namespace EmployeeManagementSystem.Models.DAL
{
    public class LoginDAL
    {
        static IConfiguration _configuration;
        
        public LoginDAL(IConfiguration configuration)
        {
            _configuration= configuration;
        }

        public bool CheckUserAuth(int UID, string pass) 
        {
            string con = _configuration.GetConnectionString("defaultConnection");
            SqlConnection cn = new SqlConnection(con);
            string sql = "select count(*) as rn from Emp_mst where emp_id='"+UID+"' and password = '"+pass+"'";
            SqlCommand cmd = new SqlCommand(sql,cn);
            cmd.CommandType=System.Data.CommandType.Text;
            cn.Open();
            using (SqlDataReader dr= cmd.ExecuteReader()) 
            {
                if (dr.Read()) 
                {
                    int rowcount = Convert.ToInt32(dr["rn"]);
                    if (rowcount>0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
