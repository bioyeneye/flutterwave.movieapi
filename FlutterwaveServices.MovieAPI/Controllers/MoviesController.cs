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
    public class MoviesController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        public MoviceService MoviceService { get; set; }
        public MoviesController(MoviceService moviceService)
        {
            MoviceService = moviceService;
        }

        /// <summary>
        /// Get movie stats
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<MoviesStatsReturnModel>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<MoviesStatsReturnModel>> Stats()
        {
            return Ok(MoviceService.GetMoviesStatsModelsForStatus());
        }
    }
}
