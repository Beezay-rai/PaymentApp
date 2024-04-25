using Microsoft.AspNetCore.Mvc;
using PaymentApp.Areas.Admin.Interfaces;

namespace PaymentApp.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DapperController : ControllerBase
    {
        private readonly IMyDapper _myDapper;

        public DapperController(IMyDapper myDapper)
        {
            _myDapper = myDapper;
        }

        [HttpGet]
        public string Test()
        {
            _myDapper.ExecutestoredProcedure();
            return "Working";
        }

    }
}
