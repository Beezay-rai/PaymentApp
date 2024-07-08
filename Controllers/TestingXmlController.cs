using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentApp.Model;
using Swashbuckle.AspNetCore.Filters;
using System.Xml.Serialization;

namespace PaymentApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class TestingXmlController : ControllerBase
    {



        [HttpPost]
        [Produces("text/xml")]
        [Consumes("text/xml")]
        public IActionResult XmlObject([FromBody] TestXmlModel model)
        {
            return Ok(model);
        }
        


        [HttpPost]
        public IActionResult JsonObject([FromBody] JsonModel model)
        {
            return Ok(model);
        }


        [HttpGet]

        public IActionResult GetJsonObj()
        {
            return Ok(new JsonModel());
        }

        public class JsonModel
        {
            public int MyProperty { get; set; }
        }




        [XmlRoot("TestingRoot")]
        public class TestXmlModel
        {
            [XmlElement("Id")]
            public string Id { get; set; }
            [XmlElement("Name")]
            public string[] Name { get; set; }
        }
    }

}
