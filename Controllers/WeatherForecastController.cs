using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace LearnIMemoryCache.Controllers
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
        private readonly IMemoryCache _memoryCache;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("SetData")]
        public ActionResult SetData()
        {
            string key = "token";
            string value = "token123";
            _memoryCache.Set(key, value, DateTimeOffset.Now.AddMinutes(5));
            return Ok("Sukses");
        }

        [HttpGet("GetData")]
        public ActionResult GetData()
        {
            string key = "token";
            var tmp = _memoryCache.Get(key);
            return Ok(tmp);
        }

        [HttpGet("DeleteData")]
        public ActionResult DeleteData()
        {
            string key = "token";
            _memoryCache.Remove(key);
            return Ok("Sukses");
        }
    }
}
