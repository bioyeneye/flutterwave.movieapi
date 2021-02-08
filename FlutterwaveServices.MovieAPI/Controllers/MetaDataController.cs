using FlutterwaveServices.MovieAPI.Model;
using FlutterwaveServices.MovieAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlutterwaveServices.MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetaDataController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        public MoviceService MoviceService { get; set; }
        public MetaDataController(MoviceService moviceService)
        {
            MoviceService = moviceService;
        }

        // GET api/<MetaDataController>/5
        [HttpGet("{movieId}")]
        [ProducesResponseType(typeof(IEnumerable<MovieMetaDataModel>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<MovieMetaDataModel>> Get(int movieId)
        {
            return Ok(MoviceService.GetMovieMetaDataModelsByMovieId(movieId));
        }

        // POST api/<MetaDataController>
        [HttpPost]
        public void Post([FromBody] MovieMetaDataModel model)
        {
            MoviceService.CreateMovieMetaDataModel(model);
        }
    }
}
