using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Serialization;

namespace PaymentApp.Utility
{
    public static class Utility
    {

        public static string Encrypt(string plaintext,RSAParameters _publickey)
        {
            RSACryptoServiceProvider rSA = new RSACryptoServiceProvider();
            rSA.ImportParameters(_publickey);


            var data =Encoding.Unicode.GetBytes(plaintext);
            var cyphertext = rSA.Encrypt(data, false);
            return Convert.ToBase64String(cyphertext);

        }

        public static string Decrypt(string cyphertext, RSACryptoServiceProvider rSA, RSAParameters _privatekey)
        {
            var databytes = Convert.FromBase64String(cyphertext);
            rSA.ImportParameters(_privatekey);
            var plaintext = rSA.Decrypt(databytes, false);
            return Encoding.Unicode.GetString(plaintext );
        }

        public static string GetPublicKey(RSAParameters _publickey)
        {
            var sw = new StringWriter();
            var xs = new XmlSerializer(typeof(RSAParameters));
            xs.Serialize(sw, _publickey);
            return sw.ToString();
        }

        public static void SignIn(string a, RSACryptoServiceProvider rSA)
        {
          
        }

    }
}
