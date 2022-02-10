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
        public GetBookQuery(BookStoreDbContext db)
        {
            _db = db;
        }

        public BookViewModel Handle()
        {
            var IsExist = _db.Books.SingleOrDefault(x => x.Id == Id);
            if (IsExist is null)
            {
                throw new InvalidOperationException("Kitap bulunamadı");
            }
            BookViewModel vm = new BookViewModel();
            vm.Genre = ((GenreEnum)IsExist.GenreId).ToString();
            vm.Title = IsExist.Title;
            vm.PageCount = IsExist.PageCount;
            vm.PublishDate = IsExist.PublishDate.ToString("dd/MM/yyyy");
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
