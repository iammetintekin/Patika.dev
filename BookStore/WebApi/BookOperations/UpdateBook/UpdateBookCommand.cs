using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        public UpdateBookModel Model { get; set; }
        private readonly BookStoreDbContext _db;

        public UpdateBookCommand(BookStoreDbContext db)
        {
            _db = db;
        }

        public void Handle()
        {
            var IsExist = _db.Books.SingleOrDefault(x => x.Id == Model.Id);
            if (IsExist is null)
            {
                throw new InvalidOperationException("Kitap bulunamadı");
            }
            //default demek o veriye dokunulduysa yani sıfır değilse bookdan gelen kategory ıd kullan
            //eğer değiiştiyse kendi kategori ıd kullan
            IsExist.GenreId = Model.GenreId != default ? Model.GenreId : IsExist.GenreId;
            IsExist.PublishDate = Model.PublishDate != default ? Model.PublishDate : IsExist.PublishDate;
            IsExist.PageCount = Model.PageCount != default ? Model.PageCount : IsExist.PageCount;
            IsExist.Title = Model.Title != default ? Model.Title : IsExist.Title;
            _db.SaveChanges();
        }
        public class UpdateBookModel
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}
