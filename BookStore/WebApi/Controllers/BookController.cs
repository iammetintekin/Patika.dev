using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBook;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DbOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _db;
        public BookController(BookStoreDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            GetBooksQuery query = new GetBooksQuery(_db);
            var result = query.Handle();
            return Ok(result);
        }

        
        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            GetBookQuery query = new GetBookQuery(_db);
           
            try
            {
                query.Id = Id;
                var result = query.Handle();
                return Ok(result);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreateBookModel book)
        {
            CreateBookCommand create = new CreateBookCommand(_db);
            try
            {
                create.Model = book;
                create.Handle();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

          
        }

        [HttpPut]
        public IActionResult GetById([FromBody] UpdateBookModel book)
        {
            UpdateBookCommand update = new UpdateBookCommand(_db);
            try
            {
                update.Model = book;
                update.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();


        }
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            DeleteBookCommand delete = new DeleteBookCommand(_db);
            try
            {
                delete.Id = Id;
                delete.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            

            return Ok();
        }

        // [HttpGet]
        // public Book GetByQuery([FromQuery]string Id)
        // { 
        //     var book = BookList.Where
        //     (
        //         s=>s.Id==Convert.ToInt32(Id)
        //     ).FirstOrDefault();
        //     return book;
        // }
    }
}