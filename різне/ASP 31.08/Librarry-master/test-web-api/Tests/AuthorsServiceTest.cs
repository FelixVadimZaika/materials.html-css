using BookShop.Data;
using BookShop.Data.Models;
using BookShop.Data.Services;
using BookShop.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace test_web_api.Tests
{
    class AuthorsServiceTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                    .UseInMemoryDatabase(databaseName: "WepAPI-9")
                    .Options;

        AppDbContext _context;
        AuthorsService _authorsService;
        int onPage = 6;

        [OneTimeSetUp]
        public void Setup()
        {
            _context = new AppDbContext(dbContextOptions);
            _context.Database.EnsureCreated();

            SeedDatabase();

            _authorsService = new AuthorsService(_context);
        }

        [Test, Order(16)]
        public void AddAuthor()
        {
            string name = "Author 4";
            _authorsService.AddAuthor(
                new AuthorVM()
                {
                    FullName = name
                });

            Assert.That(_context.Authors.FirstOrDefault(a => a.FullName == name) != null);
        }

        [Test, Order(17)]
        public void GetAuthorById_Test()
        {
            int id = 4;
            var result = _authorsService.GetAuthorById(id);

            Assert.That(result.FullName, Is.EqualTo(_context.Authors.FirstOrDefault(a => a.Id == id).FullName));
        }

        [Test, Order(18)]
        public void GetAllAuthors_Test()
        {
            var result = _authorsService.GetAllAuthors();

            Assert.That(result, Is.EqualTo(_context.Authors.ToList()));
        }

        [Test, Order(19)]
        public void GetAuthorWithBooks_Test()
        {
            int id = 2;
            var result = _authorsService.GetAuthorWithBooks(id);

            Assert.That(result.FullName,
                Is.EqualTo(_context.Authors.FirstOrDefault(a => a.Id == id).FullName));
        }

        [Test, Order(20)]
        public void UpdateAuthorById_Test()
        {
            int id = 1;
            string name = "Author 1_";
            _authorsService.UpdateAuthorById(id,
                new AuthorVM()
                {
                    FullName = name
                });

            Assert.That(name,
                Is.EqualTo(_context.Authors.FirstOrDefault(a => a.Id == id).FullName));
        }

        [Test, Order(21)]
        public void DeleteAuthorById_Test()
        {
            int id = 1;
            _authorsService.DeleteAuthorById(id);

            Assert.That(_context.Authors.FirstOrDefault(a => a.Id == id), Is.EqualTo(null));
        }

        private void SeedDatabase()
        {
            var authors = new List<Author>
            {
                new Author()
                {
                    Id = 1,
                    FullName = "Author 1"
                },
                new Author()
                {
                    Id = 2,
                    FullName = "Author 2"
                },
                new Author()
                {
                    Id = 3,
                    FullName = "Author 3"
                }
            };
            var books = new List<Book>
            {
                new Book()
                {
                    Id = 1,
                    Title = "Book 1",
                    Description = "Description 1",
                    IsRead = true,
                    DateRead = System.DateTime.Now,
                    Rate = 1,
                    Genre = "Genre 1",
                    CoverUrl = "CoverUrl 1",
                    DateAdded = System.DateTime.Now,
                    PublisherId = 1
                },
                new Book()
                {
                    Id = 2,
                    Title = "Book 2",
                    Description = "Description 2",
                    IsRead = true,
                    DateRead = System.DateTime.Now,
                    Rate = 2,
                    Genre = "Genre 2",
                    CoverUrl = "CoverUrl 2",
                    DateAdded = System.DateTime.Now,
                    PublisherId = 2
                },
                new Book()
                {
                    Id = 3,
                    Title = "Book 3",
                    Description = "Description 3",
                    IsRead = true,
                    DateRead = System.DateTime.Now,
                    Rate = 3,
                    Genre = "Genre 3",
                    CoverUrl = "CoverUrl 3",
                    DateAdded = System.DateTime.Now,
                    PublisherId = 3
                }
            };
            var book_authors = new List<Book_Author>
            {
                new Book_Author()
                {
                    Id = 1,
                    BookId = 1,
                    AuthorId = 1
                },
                new Book_Author()
                {
                    Id = 2,
                    BookId = 2,
                    AuthorId = 2
                },
                new Book_Author()
                {
                    Id = 3,
                    BookId = 1,
                    AuthorId = 3
                }
            };
            _context.Authors.AddRange(authors);
            _context.Books.AddRange(books);
            _context.Books_Authors.AddRange(book_authors);
            _context.SaveChanges();
        }
    }
}
