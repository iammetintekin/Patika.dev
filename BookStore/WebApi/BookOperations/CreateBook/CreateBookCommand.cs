using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly BookStoreDbContext _db;

        public CreateBookCommand(BookStoreDbContext db)
        {
            _db = db;
        }

        public void Handle()
        {
            var IsExist = _db.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (IsExist is not null)
            {
                throw new InvalidOperationException("Kitap zaten mevcut");
            }

            IsExist = new Book();
            IsExist.Title = Model.Title;
            IsExist.PublishDate = Model.PublishDate;
            IsExist.PageCount = Model.PageCount;
            IsExist.GenreId = Model.GenreId;

            _db.Books.Add(IsExist);
            _db.SaveChanges();
        }
        public class CreateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}
