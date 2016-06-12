using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace UserServiceDemo
{
    class DatabaseManager
    {
        #region member variables
        private string m_SQLServerAddress;
        private string m_SQLDatabaseName;
        private string m_SQLUsername;
        private string m_SQLPassword;
        private SqlConnection m_Connection;

        string m_uspInsertNewUser = "dbo.uspInsertNewUser";
        string m_uspDeleteUser = "dbo.uspDeleteUser";
        string m_uspListUser = "dbo.uspListUser";
        string m_uspUpdateUser = "dbo.uspUpdateUser";
        string m_uspUpdateLoginDate = "dbo.uspUpdateLoginDate";
        #endregion

        #region constructor

        //connect to database using app config
        public DatabaseManager()
        {
            m_SQLServerAddress = System.Configuration.ConfigurationManager.AppSettings.Get("SQLServerAddress");
            m_SQLDatabaseName = System.Configuration.ConfigurationManager.AppSettings.Get("SQLDatabaseName");
            m_SQLUsername = System.Configuration.ConfigurationManager.AppSettings.Get("SQLUserName");
            m_SQLPassword = System.Configuration.ConfigurationManager.AppSettings.Get("SQLPassword");

            m_Connection = new SqlConnection("user id=" + m_SQLUsername + ";password=" + m_SQLPassword + ";server=" + m_SQLServerAddress + ";database=" + m_SQLDatabaseName+ ";Encrypt=YES;TrustServerCertificate=true" + ";Encrypt=YES;TrustServerCertificate=true");
        }

        //connect to database using input

        public DatabaseManager(string SQLServerAddress,string SQLDatabaseName,string SQLUsername,string SQLPassword)
        {
            m_SQLServerAddress = SQLServerAddress;
            m_SQLDatabaseName = SQLDatabaseName;
            m_SQLUsername = SQLUsername;
            m_SQLPassword = SQLPassword;

            m_Connection = new SqlConnection("user id=" + m_SQLUsername + ";password=" + m_SQLPassword + ";server=" + m_SQLServerAddress + ";database=" + m_SQLDatabaseName + ";Encrypt=YES;TrustServerCertificate=true" + ";Encrypt=YES;TrustServerCertificate=true");
        }
        #endregion

        #region Stored Procs
        //insert a new user into table
        public void InsertNewUser(string fname, string lname, string email, string password, Guid guid)
        {
            //open connection
            try
            {
                m_Connection.Open();
            }
            catch
            {
                throw new Exception("An Error has occured: Cannot open database for use.\nLocation: " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name.ToString() + "." + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

            //define stored proc and input
            SqlCommand cmd = new SqlCommand(m_uspInsertNewUser, m_Connection);
            cmd.Parameters.Add(new SqlParameter("@fname", fname));
            cmd.Parameters.Add(new SqlParameter("@lname", lname));
            cmd.Parameters.Add(new SqlParameter("@email", email));
            cmd.Parameters.Add(new SqlParameter("@password", password));
            cmd.Parameters.Add(new SqlParameter("@guid", guid));
            cmd.CommandType = CommandType.StoredProcedure;

            //execute stored proc
            try
            {
                cmd.ExecuteNonQuery();
            }
            finally
            {
                cmd.Connection.Close();
            }
        }


        //delete user from table
        public void DeleteUser(string email)
        {
            //open connection
            try
            {
                m_Connection.Open();
            }
            catch
            {
                throw new Exception("An Error has occured: Cannot open database for use.\nLocation: " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name.ToString() + "." + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

            //define stored proc and input
            SqlCommand cmd = new SqlCommand(m_uspDeleteUser, m_Connection);
            cmd.Parameters.Add(new SqlParameter("@email", email));
            cmd.CommandType = CommandType.StoredProcedure;

            //execute stored proc
            try
            {
                cmd.ExecuteNonQuery();
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        //update user information in table
        public void UpdateUser(string fname, string lname, string email, string password, string oldemail)
        {
            //open connection
            try
            {
                m_Connection.Open();
            }
            catch
            {
                throw new Exception("An Error has occured: Cannot open database for use.\nLocation: " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name.ToString() + "." + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

            //define stored proc and input
            SqlCommand cmd = new SqlCommand(m_uspUpdateUser, m_Connection);
            cmd.Parameters.Add(new SqlParameter("@fname", fname));
            cmd.Parameters.Add(new SqlParameter("@lname", lname));
            cmd.Parameters.Add(new SqlParameter("@email", email));
            cmd.Parameters.Add(new SqlParameter("@password", password));
            cmd.Parameters.Add(new SqlParameter("@oldemail", oldemail));
            cmd.CommandType = CommandType.StoredProcedure;

            //execute stored proc
            try
            {
                cmd.ExecuteNonQuery();
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<User> GetUserData(string email)
        {
            //open connection
            try
            {
                m_Connection.Open();
            }
            catch
            {
                throw new Exception("An Error has occured: Cannot open database for use.\nLocation: " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name.ToString() + "." + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            //TODO: if email is null, select all users

            //define stored proc and input
            SqlCommand cmd = new SqlCommand(m_uspListUser, m_Connection);
            cmd.Parameters.Add(new SqlParameter("@email", email));
            cmd.CommandType = CommandType.StoredProcedure;

            //create user list to store results
            List<User> userList = new List<User>();
            //read results
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while(reader.Read())
            {
                //store results in user buffer
                User tempUser = new User();
                tempUser.fName = (string)reader["USER_FNAME"];
                tempUser.lName = (string)reader["USER_LNAME"];
                tempUser.email = (string)reader["USER_EMAIL"];
                tempUser.password = (string)reader["USER_PASSWORD"];
                tempUser.GUID = (Guid)reader["USER_GUID"];
                tempUser.LastLogin = (string)reader["USER_LAST_LOGIN"];

                //add user buffer to user list
                userList.Add(tempUser);

            }
            reader.NextResult();

            //close connection and return list
            reader.Close();
            return userList;
        }

        //update date on user login
        public void UpdateDate(string email)
        {
            //open connection
            try
            {
                m_Connection.Open();
            }
            catch
            {
                throw new Exception("An Error has occured: Cannot open database for use.\nLocation: " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name.ToString() + "." + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

            //define stored proc and input
            SqlCommand cmd = new SqlCommand(m_uspUpdateLoginDate, m_Connection);

            cmd.Parameters.Add(new SqlParameter("@email", email));
            cmd.CommandType = CommandType.StoredProcedure;

            //execute stored proc
            try
            {
                cmd.ExecuteNonQuery();
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
        #endregion
    }
}
