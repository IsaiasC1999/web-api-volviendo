using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using wep_api_token.Filtros;

namespace wep_api_token.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration config;

        public WeatherForecastController(ILogger<WeatherForecastController> logger ,IConfiguration config)
        {
            _logger = logger;
            this.config = config;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        [ServiceFilter(typeof(FilterConsole))]
        [ServiceFilter(typeof(FiltrosRecursos))]
        
        public IEnumerable<WeatherForecast> Get()
        {
            Console.WriteLine("Inicio de ejecucion del endPoinr");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        //En el funcionamiento de este end-point mediante inyeccion de depencias ya puede leer el Iconfiguracion
        [HttpGet("getName")]
        public string GetConfig()
        {
            return config["MyName"];
        }
    }
}