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
    public class OwnersController : ControllerBase
    {
        private readonly ILogger<OwnersController> _logger;
        // private readonly IDataService _dataService;

        public OwnersController(ILogger<OwnersController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id:guid?}")]
        public IActionResult GetOwners([FromQuery]bool useIndex=false,[FromQuery]bool useCache=false)
        {
            var owners = new List<DogOwner>();
            // IDataService.GetOwners(useIndex: useIndex, useCache: useCache,id: id);
            return Ok(owners);
        }

        [HttpGet]
        public IActionResult GetOwners([FromQuery]bool useIndex=false,[FromQuery]bool useCache=false,[FromQuery]string name=null,[FromQuery]string country=null,[FromQuery]Guid? dogId=null,[FromQuery]string breed=null)
        {
            var owners = new List<DogOwner>();
            // IDataService.GetOwners(useIndex: useIndex, useCache: useCache, useIndex: useIndex, useCache: useCache, name: name,country: country,dogId: dogId,breed: breed);
            return Ok(owners);
        }
    }
}
