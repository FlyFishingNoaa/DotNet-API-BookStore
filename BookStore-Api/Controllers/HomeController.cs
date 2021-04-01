using BookStore_Api.Contracts;
using BookStore_Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore_Api.Controllers
{ 
    /// <summary>
    /// This is Flys First Controller
    /// </summary>
    
    [Route("api/[controller]")]
    [ApiController]
    //[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class HomeController : ControllerBase
    {



        private readonly ILoggerServices _logger;

        public HomeController(ILoggerServices logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// Gets Flys ID values
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<HomeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            _logger.LogDebug("Got Flys Home Controller Value");
            return "value";
        }



        /// <summary>
        ///  Gets Flys Values
        /// </summary>
        /// <returns></returns>
        // GET: api/<HomeController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogInfo("Accessed Flys Home Controller");
            return new string[] { "value1", "value2" };
        }

       

        /// <summary>
        ///  Posts Flys values
        /// </summary>
        /// <param name="value"></param>
        // POST api/<HomeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            _logger.LogDebug("Got Flys Home Controller Error Message");
        }
        /// <summary>
        /// Puts Flys Values
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>

        // PUT api/<HomeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// Deletes the Fly
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/<HomeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _logger.LogInfo("Accessed Flys Home Controller and got a Warning");
        }


       


    }
}
