using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace UserServiceDemo
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        List<User> GetList(string email);

        [OperationContract]
        void CreateUser(string fname, string lname, string password, string email);

        [OperationContract]
        void DeleteUser(string email, string password);

        [OperationContract]
        void UpdateUser(string fname, string lname, string password, string email, string oldemail, string oldpassword);

        [OperationContract]
        bool AuthUser(string email, string password);
    }

    [DataContract]
    public class User
    {
        [DataMember]
        public string fName { get; set; }
        [DataMember]
        public string lName { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public Guid GUID { get; set; }
    }
}
