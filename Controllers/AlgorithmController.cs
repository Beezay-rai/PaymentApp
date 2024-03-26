using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PaymentApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AlgorithmController : ControllerBase
    {

        RSACryptoServiceProvider rSA;
        private RSAParameters _privatekey;
        private RSAParameters _publickey;

        public AlgorithmController()
        {
           rSA = new RSACryptoServiceProvider();
            _privatekey = rSA.ExportParameters(true);
            _publickey = rSA.ExportParameters(false);
        }


        [HttpGet]
        public object TestAlgorithm(string json)
        {
            RSACryptoServiceProvider my = new RSACryptoServiceProvider();
            my.ImportParameters(_publickey);
            var cipher = my.Encrypt(Encoding.UTF8.GetBytes(json), false);

            var ecnrypttext =Convert.ToBase64String(cipher);
            rSA.ImportParameters(_privatekey);


            var decript = rSA.Decrypt(cipher, false);
            return new
            {
                a = ecnrypttext,
                b = Encoding.UTF8.GetString(decript)
            };




        }

        [HttpGet]
        public string Decrypt(string json)
        {
            rSA.ImportParameters(_privatekey);
            var cipher = Convert.FromBase64String(json);

            var decript = rSA.Decrypt(cipher, false);
            return Encoding.UTF8.GetString(decript);
        }
    }
}
