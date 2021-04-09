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
    public class BreedsController: ControllerBase
    {
        private readonly ILogger<BreedsController> _logger;
        private readonly IDataService _dataService;

        public BreedsController(ILogger<BreedsController> logger, IDataService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }

        [HttpGet("{id:guid?}")]
        public IActionResult GetBreeds([FromRoute]Guid? id=null,[FromQuery]bool useIndex=false,[FromQuery]bool useCache=false)
        {
            var breeds = _dataService.GetBreeds(useIndex: useIndex, useCache: useCache, id: id);
            return Ok(breeds);
        }

        [HttpGet]
        public IActionResult GetBreeds([FromQuery]bool useIndex=false,[FromQuery]bool useCache=false,[FromQuery]string name=null,[FromQuery]int? popularity=null,[FromQuery]string group=null,[FromQuery]Guid? groupId=null)
        {
            var breeds= _dataService.GetBreeds(useIndex: useIndex, useCache: useCache, name: name,popularity: popularity,group: group, groupId: groupId);
            return Ok(breeds);
        }
    }
}
