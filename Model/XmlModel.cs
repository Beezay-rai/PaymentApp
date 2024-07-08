using Swashbuckle.AspNetCore.Filters;
using System.Xml.Serialization;

namespace PaymentApp.Model
{


    /// <summary>
    /// For Testing XMl Model in Swagger
    /// </summary>
    [XmlRoot("TestingRoort")]
    public class XmlModel
    {
        /// <summary>
        /// Unique ID.
        /// </summary>
        /// <example>b521fb69-d6fc-4c20-83bf-46a3f391eb52</example>
        public string Id { get; set; }
        [XmlElement("testinNAme")]
     
        public string[] Name { get; set; }
    }
    /// <summary>
    /// For Testing XMl Model in Swagger
    /// </summary>
    public class XmlModelProvide : IExamplesProvider<XmlModel>
    {
       

        public XmlModel GetExamples()
        {
            return new XmlModel()
            {
                Id="1",
                Name=new string[2] { "a","b"}
            };
        }
    }



}
