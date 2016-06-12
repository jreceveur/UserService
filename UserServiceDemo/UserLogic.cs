using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Runtime.Serialization;

namespace UserServiceDemo
{

    public static class UserLogic
    {
        //method to update user
        public static void UpdateUser(string fname, string lname, string password, string email, string oldemail, string oldpassword)
        {
            //create database connection
            DatabaseManager db = new DatabaseManager();

            //pull user information
            List<User> userInfo = GetList(oldemail);

            //authenticate user password against stored password
            if (userInfo.FirstOrDefault().password == Security.hash(oldpassword + userInfo.FirstOrDefault().GUID.ToString()))
            {
                //hash new password
                string newPassword = Security.hash(password + userInfo.FirstOrDefault().ToString());

                //update account
                db.UpdateUser(fname, lname, email, newPassword, oldemail);
            }

        }

        //method to insert new user
        public static void CreateUser(string fname, string lname, string password, string email)
        {
            //create database connection
            DatabaseManager db = new DatabaseManager();

            //create new GUID
            Guid userGuid = Guid.NewGuid();

            //hash password and unique ID to create unique hash for each user
            string newPassword = Security.hash(password + userGuid.ToString());

            //put user information into table
            db.InsertNewUser(fname, lname, email, newPassword, userGuid);
        }
        //method to delete user
        public static void DeleteUser(string email, string password)
        {
            //create database connection
            DatabaseManager db = new DatabaseManager();

            //pull user information
            List<User> userInfo = GetList(email);

            //authenticate user password against stored password
            if (userInfo.FirstOrDefault().password == Security.hash(password+userInfo.FirstOrDefault().GUID.ToString()))
            {
                //delete account
                db.DeleteUser(email);
            }
        }

        //method to list users
        public static List<User> GetList(string email)
        {
            //create database connection
            DatabaseManager db = new DatabaseManager();

            //create user list store
            List<User> userList = new List<User>();

            //populate list by email
            userList = db.GetUserData(email);

            return userList;
        }

        //method to authenticate users
        public static bool AuthUser(string email, string password)
        {
            //create database connection
            DatabaseManager db = new DatabaseManager();

            //pull user information
            List<User> userInfo = GetList(email);

            //authenticate user password against stored password
            if (userInfo.FirstOrDefault().password == Security.hash(password + userInfo.FirstOrDefault().GUID.ToString()))
            {
                db.UpdateDate(email);
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}

