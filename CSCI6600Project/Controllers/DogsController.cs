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
    public class DogsController : ControllerBase
    {
        private readonly ILogger<DogsController> _logger;
        // private readonly IDataService _dataService;

        public DogsController(ILogger<DogsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id:guid?}")]
        public IActionResult GetDogs([FromQuery]bool useIndex=false,[FromQuery]bool useCache=false)
        {
            var dogs = new List<Dog>();
            // IDataService.GetDogs(useIndex: useIndex, useCache: useCache,id:id);
            return Ok(dogs);
        }

        [HttpGet]
        public IActionResult GetDogs([FromQuery]bool useIndex=false,[FromQuery]bool useCache=false,[FromQuery]string breed=null,[FromQuery]string name=null,[FromQuery]Guid? ownerId=null,[FromQuery]int? popularity=null)
        {
            var dogs = new List<Dog>();
            // IDataService.GetDogs(useIndex: useIndex, useCache: useCache,breed: breed,name: name,ownerId: ownerId,popularity: popularity);
            return Ok(dogs);
        }
    }
}
