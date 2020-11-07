using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using playing_with_secrets.data;
using System.Threading.Tasks;

namespace playing_with_secrets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherByZipCodeRepository weatherByZipCodeRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherByZipCodeRepository repository)
        {
            _logger = logger;
            weatherByZipCodeRepository = repository;
        }

        [HttpGet]
        public Task<WeatherByZipCode> Get(string zipcode)
        {
            var weather = weatherByZipCodeRepository.GetByZipCode(zipcode);
            _logger.LogInformation("{weather}", weather);
            return weather;
        }
    }
}
