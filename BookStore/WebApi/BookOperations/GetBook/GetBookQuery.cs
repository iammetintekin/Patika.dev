using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBook
{
    public class GetBookQuery
    {
        public int Id { get; set; }
        private readonly BookStoreDbContext _db;
        private readonly IMapper _mapper;
        public GetBookQuery(BookStoreDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public BookViewModel Handle()
        {
            var IsExist = _db.Books.Where(x => x.Id == Id).SingleOrDefault();
            if (IsExist is null)
            {
                throw new InvalidOperationException("Kitap bulunamadı");
            }
            BookViewModel vm = _mapper.Map<BookViewModel>(IsExist);
            return vm;
        }
    }
    public class BookViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}
