using CSCI6600Project.DataGeneration;
using CSCI6600Project.Models;
using CSCI6600Project.Models.NonIndex;
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
    public class BreedsController : ControllerBase
    {
        private readonly ILogger<BreedsController> _logger;
        // private readonly IDataService _dataService;

        public BreedsController(ILogger<BreedsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id:guid?}")]
        public IActionResult GetBreeds([FromQuery]bool useIndex=false,[FromQuery]bool useCache=false)
        {
            var breeds = new List<DogBreed>();
            // IDataService.GetBreeds(id: id);
            return Ok(breeds);
        }

        [HttpGet]
        public IActionResult GetBreeds([FromQuery]bool useIndex=false,[FromQuery]bool useCache=false,[FromQuery]string name=null,[FromQuery]int? popularity=null,[FromQuery]string description=null)
        {
            var breeds = new List<DogBreed>();
            // IDataService.GetBreeds(name: name,popularity: popularity,description: description);
            return Ok(breeds);
        }
    }
}
