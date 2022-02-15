using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
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
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            GetBooksQuery query = new GetBooksQuery(_db, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        
        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            GetBookQuery query = new GetBookQuery(_db, _mapper);
            try
            {
                query.Id = Id;
                GetBookValidator validate = new GetBookValidator();
                validate.ValidateAndThrow(query);

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
            CreateBookCommand create = new CreateBookCommand(_db,_mapper);
            try
            {
                create.Model = book;
                CreateBookValidator validator = new CreateBookValidator();
                validator.ValidateAndThrow(create);
              //  ValidationResult result = validator.Validate(create);
                create.Handle();
                return Ok();
                //if (!result.IsValid)
                //{
                //    string errormessages = "";
                //    foreach (var item in result.Errors)
                //    {
                //        errormessages += $"Özellik {item.PropertyName} Error Mesajý : {item.ErrorMessage} \n";
                //    }
                //    return BadRequest(errormessages);
                //}
                //else
                //{
                //    create.Handle();
                //    return Ok();
                //}


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

          
        }

        [HttpPut("{Id}")]
        public IActionResult Update(int Id,[FromBody] UpdateBookModel book)
        {
            UpdateBookCommand update = new UpdateBookCommand(_db);
            try
            {
                update.Id = Id;
                update.Model = book;

                UpdateBookValidator validate = new UpdateBookValidator();
                validate.ValidateAndThrow(update);

                
                update.Handle();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            


        }
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            DeleteBookCommand delete = new DeleteBookCommand(_db);
            try
            {
                delete.Id = Id;
                DeleteBookValidator validator = new DeleteBookValidator();
                validator.ValidateAndThrow(delete);
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