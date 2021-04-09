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
    public class OwnersController: ControllerBase
    {
        private readonly ILogger<OwnersController> _logger;
        private readonly IDataService _dataService;

        public OwnersController(ILogger<OwnersController> logger, IDataService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }

        [HttpGet("{id:guid?}")]
        public IActionResult GetOwners([FromRoute]Guid? id=null,[FromQuery]bool useIndex=false,[FromQuery]bool useCache=false)
        {
            var owners = _dataService.GetOwners(useIndex: useIndex, useCache: useCache, id: id);
            return Ok(owners);
        }

        [HttpGet]
        public IActionResult GetOwners([FromQuery]bool useIndex=false,[FromQuery]bool useCache=false,[FromQuery]string firstName=null,[FromQuery]string lastName=null,[FromQuery]string dog=null,[FromQuery]Guid? dogId=null,[FromQuery]string breed=null)
        {
            var owners = _dataService.GetOwners(useIndex: useIndex, useCache: useCache, firstName: firstName,lastName: lastName,dog: dog, dogId: dogId,breed: breed);
            return Ok(owners);
        }
    }
}
