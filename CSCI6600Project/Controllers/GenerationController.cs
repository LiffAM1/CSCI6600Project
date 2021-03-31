using CSCI6600Project.DataGeneration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI6600Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenerationController : ControllerBase
    {
        private readonly ILogger<GenerationController> _logger;
        private readonly IDataService _generatorService;

        public GenerationController(ILogger<GenerationController> logger, IDataService generator)
        {
            _logger = logger;
            _generatorService = generator;
        }

        [HttpPost]
        public IActionResult GenerateOwners(int number)
        {
            GenerateOwners(number);
            return new OkResult();
        }

        [HttpPost]
        public IActionResult GenerateDogs(int number)
        {
            GenerateDogs(number);
            return new OkResult();
        }
    }
}
