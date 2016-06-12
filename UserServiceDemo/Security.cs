using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServiceDemo
{
    public static class Security
    {
        public static string hash(string input)
        {
            //hash input string using SHA1
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var hashInput = Encoding.ASCII.GetBytes(input);
            var hash = sha1.ComputeHash(hashInput);

            //build string from hash results
            StringBuilder passString = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                passString.Append(hash[i].ToString("X2"));
            }

            //return string of hash
            return passString.ToString();
        }
    }
}
