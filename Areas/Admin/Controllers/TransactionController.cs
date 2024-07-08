using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using PaymentApp.Areas.Admin.Interfaces;
using PaymentApp.Areas.Admin.Models;
using PaymentApp.Model;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
namespace PaymentApp.Areas.Admin.Controllers
{
    [Route("api/Admin/[controller]/[action]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransaction _transaction;
        RSACryptoServiceProvider rSA;

        private RSAParameters _privatekey;
        private RSAParameters _publickey;
        public TransactionController(ITransaction transaction)
        {
            rSA = new RSACryptoServiceProvider();

            _privatekey = rSA.ExportParameters(true);
            _publickey = rSA.ExportParameters(false);
            _transaction = transaction;
        }

        #region Learning
        [HttpGet]
        public async Task<IActionResult> TotalAmount([FromBody] string username = "Bijay")
        {
            var data = await _transaction.CheckBalance(username);

            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> WithdrawAmountAsync(int amount, string Username)
        {

            return Ok(await _transaction.WithdrawAmount(amount, Username));
        }
        [HttpPost]
        public async Task<IActionResult> DepositAmountAsync([FromHeader] TransactionModel model)
        {

            var randomnumb = new Random().Next(0, 6);
            var response = new ResponseModel();
            switch (randomnumb)
            {
                case 0:
                    response = await _transaction.DeposiAmount(model);
                    break;
                case 1:
                    response = await _transaction.SetTransactionFailure(model);
                    break;
                case 2:
                    Task.Delay(10000).GetAwaiter().GetResult();
                    response = await _transaction.SetTransactionFailure(model);
                    break;
                case 3:
                    Task.Delay(10000).GetAwaiter().GetResult();
                    response = await _transaction.DeposiAmount(model);
                    break;
                case 4:
                    return BadRequest();
                case 5:
                    throw new Exception("Something went wrong");
                default:
                    break;
            }
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> CreateNewBalance([FromBody] BalanceModel tocreatenewacc)
        {
            return Ok(await _transaction.CreateNewBalance(tocreatenewacc));
        }

        [HttpGet]
        [Produces("application/xml")]
        public async Task<IActionResult> GetAllTransaction()
        {

            return Ok(await _transaction.GetAllTransaction());
        }
        #endregion


        [HttpGet]
        public string RsaSignin(string json)
        {
            var test = rSA.SignData(Encoding.UTF8.GetBytes(json), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            var verify = rSA.VerifyData(Encoding.UTF8.GetBytes(json), HashAlgorithmName.SHA256, test);


            return Convert.ToBase64String(test);
        }

        [HttpGet]

        public bool verify(string json)
        {
            var test = rSA.VerifyData(Encoding.UTF8.GetBytes(json), HashAlgorithmName.SHA256, Encoding.UTF8.GetBytes(json));
            return test;
        }






        [HttpPost]
        public async Task<IActionResult> CheckStatus([FromBody] int trackingid = 50)
        {
            var response = new ResponseModel();
            var randomnumb = new Random().Next(0, 4);
            switch (randomnumb)
            {
                case 0:
                    response = await _transaction.CheckStatus(trackingid.ToString());
                    break;
                case 1:
                    response = await _transaction.CheckStatus(trackingid.ToString());

                    break;
                case 2:
                    response = await _transaction.CheckStatus(trackingid.ToString());

                    break;
                case 3:
                    response = await _transaction.CheckStatus(trackingid.ToString());

                    break;
                default:
                    break;
            }
            return Ok(response);
        }
    }
}
