using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBooks;
using WebApi.DbOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

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
        public Book GetById(int Id)
        {
            var book = _db.Books.Where(s=>s.Id==Id).FirstOrDefault();
            return book;
        }
        [HttpPost]
        public IActionResult Add([FromBody] CreateBookModel book)
        {
            CreateBookCommand create = new CreateBookCommand(_db);
            try
            {
                create.Model = book;
                create.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

            return Ok();
        }
        [HttpPut("{Id}")]
        public IActionResult GetById(int Id,[FromBody] Book book)
        {
            var IsExist = _db.Books.SingleOrDefault(x=>x.Id==Id);
            if(IsExist is null)
            {
                return BadRequest();
            }

            IsExist.GenreId = book.GenreId != default ? book.GenreId:IsExist.GenreId;
            //default demek o veriye dokunulduysa yani sıfır değilse bookdan gelen kategory ıd kullan
            //eğer değiiştiyse kendi kategori ıd kullan
            IsExist.PublishDate = book.PublishDate != default ? book.PublishDate:IsExist.PublishDate;
            IsExist.PageCount = book.PageCount != default ? book.PageCount:IsExist.PageCount;
            IsExist.Title = book.Title != default ? book.Title:IsExist.Title;
            _db.SaveChanges();
            return Content($"{IsExist.Title} güncellendi");

        }
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var IsExist = _db.Books.SingleOrDefault(x=>x.Id==Id);
            if(IsExist is null)
            {
                return BadRequest();
            }
            _db.Books.Remove(IsExist);
            _db.SaveChanges();

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