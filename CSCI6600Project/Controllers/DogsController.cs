using CSCI6600Project.DataGeneration;
using CSCI6600Project.Models;
using CSCI6600Project.Models.Base;
using CSCI6600Project.Models.Context;
using CSCI6600Project.Responses;
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
        private readonly IDataService _dataService;

        public DogsController(ILogger<DogsController> logger, IDataService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }

        [HttpGet("{id:guid?}")]
        public IActionResult GetDogs([FromRoute]Guid? id=null,[FromQuery]bool useIndex=false,[FromQuery]bool useCache=false)
        {
            var dogs = _dataService.GetDogs(useIndex: useIndex, useCache: useCache, id: id);
            return Ok(dogs.Select(d => new DogResponse(d)));
        }

        [HttpGet]
        public IActionResult GetDogs([FromQuery]bool useIndex=false,[FromQuery]bool useCache=false,[FromQuery]string breed=null,[FromQuery]Guid? breedId=null,[FromQuery]string name=null,[FromQuery]string ownerFirstName=null,[FromQuery]string ownerLastName=null,[FromQuery]Guid? ownerId=null,[FromQuery]int? popularity=null)
        {
            var dogs = _dataService.GetDogs(useIndex: useIndex, useCache: useCache,breed: breed,breedId: breedId,name: name,ownerId: ownerId,ownerFirstName: ownerFirstName,ownerLastName: ownerLastName,popularity: popularity);
            return Ok(dogs.Select(d => new DogResponse(d)));
        }
    }
}
