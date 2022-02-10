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
        public int Id { get; set; }
        private readonly BookStoreDbContext _db;

        public UpdateBookCommand(BookStoreDbContext db)
        {
            _db = db;
        }

        public void Handle()
        {
            var IsExist = _db.Books.SingleOrDefault(x => x.Id == Id);
            if (IsExist is null)
            {
                throw new InvalidOperationException("Güncellenecek kitap bulunamadı");
            }
            //default demek o veriye dokunulduysa yani sıfır değilse bookdan gelen kategory ıd kullan
            //eğer değiiştiyse kendi kategori ıd kullan
            IsExist.GenreId = Model.GenreId != default ? Model.GenreId : IsExist.GenreId;
            IsExist.Title = Model.Title != default ? Model.Title : IsExist.Title;
            _db.SaveChanges();
        }
        public class UpdateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
        }
    }
}
