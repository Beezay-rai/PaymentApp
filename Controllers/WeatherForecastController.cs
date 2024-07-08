using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PaymentApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            var random = new Random();
            var prob = random.Next(0, 3);
            if (prob == 0)
            {
                return Ok(Success());
            }
            else if (prob == 1)
            {
                Task.Delay(60000).GetAwaiter().GetResult();
                return Ok(Success());
            }
            else if (prob == 2)
            {
                Task.Delay(60000).GetAwaiter().GetResult();
                return Ok(Fail());
            }
            else
            {
                Task.Delay(60000).GetAwaiter().GetResult();
                return Ok(Unknown());
            }
        }

        private object Success()
        {
            return new { Code = 200 };
        }

        private object Fail()
        {
            return new { Code = 400 };
        }
        private object Unknown()
        {
            return new { Code = 500 };
        }
    }
}
