using ModelsLibrary;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DALibrary
{
    public class UserDAL
    {
        SqlConnection conn;
        public UserDAL()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);
        }

        public string Login(int userId, string password)
        {
            string userName = null;
            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = conn;
            cmdLogin.CommandText = "proc_Login";
            cmdLogin.CommandType = CommandType.StoredProcedure;
            cmdLogin.Parameters.Add("@Id", SqlDbType.VarChar, 20);
            cmdLogin.Parameters.Add("@Password", SqlDbType.VarChar, 20);
            cmdLogin.Parameters.Add("@UserName", SqlDbType.VarChar, 20);
            cmdLogin.Parameters[0].Value = userId;
            cmdLogin.Parameters[1].Value = password;
            cmdLogin.Parameters[2].Direction = ParameterDirection.Output;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            cmdLogin.ExecuteScalar();
            userName = cmdLogin.Parameters[2].Value.ToString();
            conn.Close();
            return userName;
        }
        public User Register(string userName, int userAge)
        {
            User user = new User();
            SqlCommand cmdRegister = new SqlCommand();
            cmdRegister.Connection = conn;
            cmdRegister.CommandText = "proc_RegisterUser";
            cmdRegister.CommandType = CommandType.StoredProcedure;
            cmdRegister.Parameters.Add("@Name", SqlDbType.VarChar, 20);
            cmdRegister.Parameters.Add("@Age", SqlDbType.Int);
            cmdRegister.Parameters.Add("@Id", SqlDbType.Int);
            cmdRegister.Parameters.Add("@Password", SqlDbType.VarChar, 30);
            cmdRegister.Parameters[0].Value = userName;
            cmdRegister.Parameters[1].Value = userAge;
            cmdRegister.Parameters[2].Direction = ParameterDirection.Output;
            cmdRegister.Parameters[3].Direction = ParameterDirection.Output;
            user.Name = userName;
            user.Age = userAge;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            cmdRegister.ExecuteScalar();
            user.Id = Convert.ToInt32(cmdRegister.Parameters[2].Value);
            user.Password = cmdRegister.Parameters[3].Value.ToString();
            conn.Close();
            return user;
        }
    }
}