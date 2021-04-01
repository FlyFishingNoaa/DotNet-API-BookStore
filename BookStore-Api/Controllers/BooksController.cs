using AutoMapper;
using BookStore_Api.Contracts;
using BookStore_Api.Data;
using BookStore_Api.Data.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_Api.Controllers
{

    /// <summary>
    /// Flys Books Control 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly iBookRepository _bookRepository;
        private readonly ILoggerServices _logger;
        private readonly IMapper _mapper;

        public BooksController(iBookRepository BookRepository, ILoggerServices logger, IMapper mapper)
        {
            _bookRepository = BookRepository;
            _logger = logger;
            _mapper = mapper;
        }





        /// <summary>
        /// Gets a Book in Flys Book Store by ID
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBook(int id)
        {
            var location = GetControllerActionNames();
            try
            {
                _logger.LogInfo("Trying to get all of Flys Book store Authors");
                var book = await _bookRepository.FindByID(id);
                var response = _mapper.Map<BookDTO>(book);
                _logger.LogInfo("Bingo got flys Book with no issues");
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogIError($"{location} {e.Message } - {e.StackTrace }");
                return StatusCode(500, "Opps The Fly Get Authors Call did not work as expected");
            }
        }




        /// <summary>
        /// Get all Books in Flys Book Store
        /// </summary>
        /// <returns>List of Books</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBooks()
        {

            var location = GetControllerActionNames();


            try
            {
                _logger.LogInfo("Trying to get all of Flys Book store Authors");
                var books = await _bookRepository.FindAll();
                var response = _mapper.Map<IList<BookDTO>>(books);
                _logger.LogInfo("Bingo got flys BookStore Authors with no issues");
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogIError($"{location} {e.Message } - {e.StackTrace }");
                return StatusCode(500, "Opps The Fly Get Authors Call did not work as expected");
            }
        }






        /// <summary>
        /// Creates a new Author in Flys Book Store
        /// </summary>
        /// <param name="bookDTO"></param>        
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] BookCreateDTO bookDTO)
        {

            var location = GetControllerActionNames();
            try
            {
                if (bookDTO == null)
                {
                    _logger.LogWarn($"Empty Request was submitted");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"Bad Data Dude");
                    return BadRequest(ModelState);
                }
                var book = _mapper.Map<Book>(bookDTO);
                var isSuccess = await _bookRepository.Create(book);

                if (!isSuccess)
                {
                    _logger.LogWarn($"Bad Data Dude");
                    return InternalError($"Did not write to Flys Book store ");
                }


                //todo: add logging 
                return Created("Create", new {book});
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
        /// <param name="bookDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] BookUpdateDTO bookDTO)
        {
            try
            {
                if (id < 1 || bookDTO == null || id != bookDTO.Id)
                {
                    _logger.LogWarn($"Empty Request was submitted");
                    return BadRequest(ModelState);
                }
                var isExists = await _bookRepository.isExists(id);
                if (!isExists)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"Bad Data Dude");
                    return BadRequest(ModelState);
                }
                var book = _mapper.Map<Book>(bookDTO);
                var isSuccess = await _bookRepository.Update(book);

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
       /// Delete the book 
       /// </summary>
       /// <param name="id"></param>       
       /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

                var book = await _bookRepository.FindByID(id);
                if (book == null)
                {
                    return NotFound();
                }


                //ToDo: add isExists logic 
                //Todo: Refactor Errors and messages 
                var isSuccess = await _bookRepository.Delete(book);

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







        /// <summary>
        /// Creates a new Author in Flys Book Store
        /// </summary>
        /// <param name="bookDTO"></param>        
        /// <returns></returns>
        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> Update([FromBody] BookUpdateDTO bookDTO)
        //{

        //    var location = GetControllerActionNames();
        //    try
        //    {
        //        if (bookDTO == null)
        //        {
        //            _logger.LogWarn($"Empty Request was submitted");
        //            return BadRequest(ModelState);
        //        }
        //        if (!ModelState.IsValid)
        //        {
        //            _logger.LogWarn($"Bad Data Dude");
        //            return BadRequest(ModelState);
        //        }
        //        var book = _mapper.Map<Book>(bookDTO);
        //        var isSuccess = await _bookRepository.Update(book);

        //        if (!isSuccess)
        //        {
        //            _logger.LogWarn($"Bad Data Dude");
        //            return InternalError($"Did not write to Flys Book store ");
        //        }


        //        //todo: add logging 
        //        return Created("Create", new { book });
        //    }
        //    catch (Exception e)
        //    {
        //        return InternalError($"{e.Message } - {e.StackTrace }");
        //    }


        //}

























        

    private string GetControllerActionNames()
        {

            var controller = ControllerContext.ActionDescriptor.ControllerName;
            var action = ControllerContext.ActionDescriptor.ActionName;

            return $"{controller} - {action}";
        }



        private ObjectResult InternalError(string message)
        {
            _logger.LogIError(message);
            return StatusCode(500, "Ohh Noes Something went wrong with Flys Book Store");

        }


    }
}
