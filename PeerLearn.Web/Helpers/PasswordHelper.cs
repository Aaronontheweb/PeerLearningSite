using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace PeerLearn.Web.Helpers
{
    public class PasswordHelper
    {
        private MachineKeySection _machineKey;

        public void Initialize()
        {
            var cfg = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            _machineKey = (MachineKeySection)cfg.GetSection("system.web/machineKey");
        }

        public bool CheckPassword(string password, string dbpassword)
        {
            var pass1 = password;
            var pass2 = dbpassword;

            pass1 = EncodePassword(password);

            return pass1 == pass2;
        }

        //EncodePassword:Encrypts, Hashes, or leaves the password clear based on the PasswordFormat.
        public string EncodePassword(string password)
        {
            var encodedPassword = password;
            var hash = new HMACSHA1 { Key = HexToByte(_machineKey.ValidationKey) };
            encodedPassword = Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));
            return encodedPassword;
        }

        //   Converts a hexadecimal string to a byte array. Used to convert encryption key values from the configuration.    
        public byte[] HexToByte(string hexString)
        {
            var returnBytes = new byte[hexString.Length / 2];
            for (var i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
    }
}