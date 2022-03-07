using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Recipes.API.Data.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.API.Controllers
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
        private readonly PopulateIngredientsTableService populateIngredients;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, PopulateIngredientsTableService populateIngredients)
        {
            _logger = logger;
            this.populateIngredients = populateIngredients;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            populateIngredients.Populate();
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
