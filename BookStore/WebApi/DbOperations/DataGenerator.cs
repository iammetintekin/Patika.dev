using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace WebApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context =
                new BookStoreDbContext
                (serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }
                context.Books.AddRange(
                    new Book
                    {
                        Title = "Lean Startup",
                        GenreId = 1,
                        PageCount = 200,
                        PublishDate = new DateTime(2001, 06, 12)
                    },
                    new Book
                    {
                        Title = "Herland",
                        GenreId = 2,
                        PageCount = 145,
                        PublishDate = new DateTime(2010, 03, 02)
                    },
                    new Book
                    {
                        Title = "Dune",
                        GenreId = 2,
                        PageCount = 540,
                        PublishDate = new DateTime(1997, 01, 01)
                    },
                    new Book
                    {
                        Title = "Marlon",
                        GenreId = 1,
                        PageCount = 196,
                        PublishDate = new DateTime(1999, 07, 23)

                    });

                context.SaveChanges();
            }

        }
    }
}
