using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Xml.Serialization;
using PaymentApp.Areas.Admin.Models;
using Abp.Web.Models;
using Swashbuckle.AspNetCore.Annotations;
using PaymentApp.Model;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;

namespace PaymentApp.Controllers
{
    /// <summary>
    /// For Testing Purpose
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Produces("application/xml", "application/json")]
    [Consumes("application/xml", "application/json")]
    public class TestingController : ControllerBase
    {

        RSACryptoServiceProvider rSA;
      

        private RSAParameters _privatekey;
        private RSAParameters _publickey;

        /// <summary>
        /// For Testing Purpose
        /// </summary>
        public TestingController()
        {
            rSA = new RSACryptoServiceProvider();

            _privatekey = rSA.ExportParameters(true);
            _publickey = rSA.ExportParameters(false);
        }



        [HttpPost]
        [SwaggerRequestExample(typeof(XmlModel),typeof(XmlModelProvide))]

        public IActionResult testingXmlObj([FromBody] XmlModel model)
        {
            return Ok(model);
        }
     


        [HttpGet]
        public object testingalo(string json)
        {

            Utility.Utility.GetPublicKey(_publickey);

            var cipher = Utility.Utility.Encrypt(json, _publickey);
            var decrypted = Utility.Utility.Decrypt(cipher, rSA, _privatekey);

            return new
            {
                PublicKey = _publickey,
                cipher = cipher,
                decrypted = decrypted,
            };


        }

       
      

        [HttpPost]
      
        public IActionResult CreatingNewUserForXmlJSON(BalanceModel model)
        {
            return Ok(model);
        }
        [HttpGet]
        public IActionResult GetItems([FromQuery] int[] ids)
        {
            // Your logic here
            return Ok();
        }



        #region Multiplicity
        public class OneToMany
        {
            /// <summary>
            /// <example>123</example>
            /// </summary>
            public int Id { get; set; }
            [MinLength(1)]
            [MaxLength(3)]
            public List<object> ObjectsList { get; set; }

            [SwaggerSchema("This is my prop")]
            [MaxLength(100)]
            public string MyProperty { get; set; }
        }

        [HttpPost]
        public IActionResult OneToManyTest(OneToMany obj)
        {
            return Ok(obj);
        }


        public class NoneToSome
        {
            public int Id { get; set; }
            public object? ObjectVal { get; set; }
        }
        [HttpPost]
        public IActionResult NoneToSomeTest(NoneToSome obj)
        {
            return Ok(obj);
        }







        #endregion
    }
}
