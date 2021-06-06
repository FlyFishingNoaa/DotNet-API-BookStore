using AutoMapper;
using BookStore_Api.Contracts;
using BookStore_Api.Data;
using BookStore_Api.Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace BookStore_Api.Controllers
{

    /// <summary>
    /// Flys BookStore Authors Endpoint that connects to the DataBase
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class AuthorsController : ControllerBase
    {


        private readonly IAuthorRepository _authorRepository;
        private readonly ILoggerServices _logger;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorRepository authorRepository, ILoggerServices logger, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _logger = logger;
            _mapper = mapper;
        }



        


        /// <summary>
        /// Get all Authors in Flys Book Store
        /// </summary>
        /// <returns>List of Authors</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAuthors()
        {

            try
            {
                _logger.LogInfo("Trying to get all of Flys Book store Authors");
                var authors = await _authorRepository.FindAll();
                var response = _mapper.Map<IList<AuthorDTO>>(authors);
                _logger.LogInfo("Bingo got flys BookStore Authors with no issues");
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogIError($"{e.Message } - {e.StackTrace }");
                return StatusCode(500, "Opps The Fly Get Authors Call did not work as expected");
            }
        }


        /// <summary>
        /// Get Author in Flys book store by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ruturns Author from Flys book store</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAuthor(int id)
        {

            try
            {
                _logger.LogInfo($"Trying to get all of Flys Book store Author by ID: {id}");
                var author = await _authorRepository.FindByID(id);
                if (author == null)
                {
                    _logger.LogWarn($"Author with id:{id} was not found");
                    return NotFound();
                }
                var response = _mapper.Map<AuthorDTO>(author);
                _logger.LogInfo("Bingo got flys BookStore Author by ID with no issues");
                return Ok(response);
            }
            catch (Exception e)
            {
               return InternalError($"{e.Message } - {e.StackTrace }");             
                
            }
        }



       
        /// <summary>
        /// Creates a new Author in Flys Book Store - Administrator
        /// </summary>
        /// <param name="authorDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
         //////// [Authorize]
        ////[AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        ////[ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] AuthorCreateDTO authorDTO)
        {
            try
            {
                if (authorDTO == null)
                {
                    _logger.LogWarn($"Empty Request was submitted");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"Bad Data Dude");
                    return BadRequest(ModelState);
                }
                var author = _mapper.Map<Author>(authorDTO);
                var isSuccess = await _authorRepository.Create(author);

                if (!isSuccess)
                {
                    _logger.LogWarn($"Bad Data Dude");
                    return InternalError($"Did not write to Flys Book store ");
                }

                return Created("Create", new { author });
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message } - {e.StackTrace }");
            }


        }




        /// <summary>
        /// Updates Author info in Flys Book Store
        /// </summary>
        /// <param name="id"></param>
        /// <param name="authorDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator, Customer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] AuthorUpdateDTO authorDTO)
        {
            try
            {
                if (id < 1 || authorDTO == null || id != authorDTO.Id)
                {
                    _logger.LogWarn($"Empty Request was submitted");
                    return BadRequest(ModelState);
                }


               // var isExists = await _authorRepository.FindByID(id);
                var isExists = await _authorRepository.isExists(id);



                if (!isExists)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"Bad Data Dude");
                    return BadRequest(ModelState);
                }
                var author = _mapper.Map<Author>(authorDTO);
               // var isSuccess = await _authorRepository.Save(author);
                var isSuccess = await _authorRepository.Update(author);

                if (!isSuccess)
                {
                    _logger.LogWarn($"Bad Data Dude");
                    return InternalError($"Did not Update to Flys Book store ");
                }

                return NoContent();
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message } - {e.StackTrace }");
            }


        }





        /// <summary>
        /// Deletes Author info in Flys Book Store
        /// </summary>
        /// <param name="id"></param>
        /// <param name="authorDTO"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator, Customer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> Delete(int id, [FromBody] AuthorDeleteDTO authorDTO)
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 1)
                {
                    _logger.LogWarn($"Empty Request was submitted");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"Bad Data Dude");
                    return BadRequest(ModelState);
                }

                var author = await _authorRepository.FindByID(id);
                if (author == null)
                {
                    return NotFound();
                }

                var isSuccess = await _authorRepository.Delete(author);

                if (!isSuccess)
                {
                    _logger.LogWarn($"Bad Data Dude");
                    return InternalError($"Did not Delete from Flys Book store ");
                }

                return NoContent();
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message } - {e.StackTrace }");
            }


        }






        private ObjectResult InternalError(string message)
        {
            _logger.LogIError(message);
            return StatusCode(500, "Ohh Noes Something went wrong with Flys Book Store");

        }





    }
}
