using AutoMapper;
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
        private readonly IMapper _mapper;
        public GetBooksQuery(BookStoreDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public List<BooksViewModel> Handle()
        {
            var books = _db.Books.OrderBy(x=>x.Id).ToList<Book>();
            List<BooksViewModel> vm = _mapper.Map<List<BooksViewModel>>(books);
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
