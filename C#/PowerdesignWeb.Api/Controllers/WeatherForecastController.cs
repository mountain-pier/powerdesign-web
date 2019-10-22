using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PowerdesignWeb.Api.Models;
using PowerdesignWeb.Api.Services;

namespace PowerdesignWeb.Api.Controllers
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
        private readonly PdmService _pdmService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, PdmService pdmService)
        {
            _logger = logger;
            _pdmService = pdmService;
        }
        [HttpGet]
        public PdmModels Get()
        {
            string filePath = $"{AppContext.BaseDirectory}表单流程.pdm";
            var pmdModel = _pdmService.ReadFromFile(filePath);
            return pmdModel;
        }
    }
}
