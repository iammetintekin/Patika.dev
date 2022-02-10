using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _db;
        public GetBooksQuery(BookStoreDbContext db)
        {
            _db = db;
        }
        public List<BooksViewModel> Handle()
        {
            var books = _db.Books.OrderBy(x=>x.Id).ToList<Book>();
            List<BooksViewModel> vm = new List<BooksViewModel>();
            foreach (var book in books)
            {
                vm.Add(new BooksViewModel
                {
                    Title = book.Title,
                    PageCount = book.PageCount,
                    PublishDate = book.PublishDate.ToString("dd/MM/yyyy"),
                    Genre = ((GenreEnum)book.GenreId).ToString()
                });
            }
            return vm;
        }

    }

    public class BooksViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}
