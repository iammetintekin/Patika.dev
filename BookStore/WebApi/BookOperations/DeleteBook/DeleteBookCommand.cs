using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        public int Id { get; set; }
        private readonly BookStoreDbContext _db;

        public DeleteBookCommand(BookStoreDbContext db)
        {
            _db = db;
        }
        public void Handle()
        {
            var IsExist = _db.Books.SingleOrDefault(x => x.Id == Id);
            if (IsExist is null)
            {
                throw new InvalidOperationException("Kitap bulunamadı");
            }
            _db.Books.Remove(IsExist);
            _db.SaveChanges();
        }
    }
}
