using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace UserServiceDemo
{
    
    public class UserService : IUserService
    {
        
        [WebInvoke(Method = "GET",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    BodyStyle = WebMessageBodyStyle.WrappedRequest,
                    UriTemplate = "/List?email={email}")]
        //method to list users

        public List<User> GetList(string email)
        {
            //empty result set
            List<User> resultSet = new List<User>();

            //TODO: Check if null, set to "ALL"

            // TODO: Check if CSV, convert to array
            // TODO: loop through array for each email
            // TODO: add user to list<user>

            //query table by email
            resultSet = UserLogic.GetList(email);
            
            //return results     
            return resultSet;
        }

        //method to update user
        //assumption is made that all information will be passed into service
        [WebInvoke(Method = "GET",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    BodyStyle = WebMessageBodyStyle.WrappedRequest,
                    UriTemplate = "/Update?fname={fname}&lname={lname}&password={password}&email={email}&oldemail={oldemail}&oldpassword={oldpassword}")]

        public void UpdateUser(string fname, string lname, string password, string email, string oldemail, string oldpassword)
        {
            UserLogic.UpdateUser(fname, lname, password, email, oldemail, oldpassword);

            //TODO: devise override methods to accept less than all of input, rewrite stored proc to check for updates based on which variables are being passed in
        }

        //method to insert new user
        [WebInvoke(Method = "GET",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    BodyStyle = WebMessageBodyStyle.WrappedRequest,
                    UriTemplate = "/Create?fname={fname}&lname={lname}&password={password}&email={email}")]

        public void CreateUser(string fname, string lname, string password, string email)
        {
            UserLogic.CreateUser(fname, lname, password, email);
        }

        //method to delete user
        [WebInvoke(Method = "GET",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    BodyStyle = WebMessageBodyStyle.WrappedRequest,
                    UriTemplate = "/Delete?email={email}&password={password}")]

        public void DeleteUser(string email, string password)
        {
            UserLogic.DeleteUser(email, password);
        }


        //method to authenticate users
        [WebInvoke(Method = "GET",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    BodyStyle = WebMessageBodyStyle.WrappedRequest,
                    UriTemplate = "/Auth?email={email}&password={password}")]

        public bool AuthUser(string email, string password)
        {
            return UserLogic.AuthUser(email, password);
        }
    }
}

