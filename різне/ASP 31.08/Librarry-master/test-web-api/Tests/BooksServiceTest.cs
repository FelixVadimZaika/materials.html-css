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
    class BooksServiceTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                    .UseInMemoryDatabase(databaseName: "WepAPI-9")
                    .Options;

        AppDbContext _context;
        BooksService _booksService;
        int onPage = 6;

        [OneTimeSetUp]
        public void Setup()
        {
            _context = new AppDbContext(dbContextOptions);
            _context.Database.EnsureCreated();

            SeedDatabase();

            _booksService = new BooksService(_context);
        }

        [Test, Order(11)]
        public void AddBookWithAuthours_Test()
        {
            string title = "Book 11";
            _booksService.AddBookWithAuthors(
               new BookVM()
               {
                   Title = title,
                   Description = "Description 11",
                   IsRead = true,
                   DateRead = System.DateTime.Now,
                   Rate = 11,
                   Genre = "Genre 11",
                   CoverUrl = "CoverUrl 11",
                   PublisherId = 11,
                   AuthorIds = new List<int>() { 1, 2, 3 }
               });

            Assert.That(_context.Books.FirstOrDefault(b => b.Title == title) != null);
        }

        [Test, Order(12)]
        public void GetAllBooks_Test()
        {
            var result = _booksService.GetAllBooks();

            Assert.That(result, Is.EqualTo(_context.Books.ToList()));
        }

        [Test, Order(13)]
        public void GetBookById_Test() // error
        {
            int id = 11;
            var result = _booksService.GetBookById(id); // вертає null

            Assert.That(result, Is.EqualTo(_context.Books.FirstOrDefault(b => b.Id == id)));
        }

        [Test, Order(14)]
        public void UpdateBookById_Test()
        {
            int id = 1;
            string title = "Book 10_";
            _booksService.UpdateBookById(id,
                new BookVM()
                {
                    Title = title
                });

            Assert.That(title, Is.EqualTo(_context.Books.FirstOrDefault(b => b.Id == id).Title));
        }

        [Test, Order(15)]
        public void DeleteBookById_Test()
        {
            int id = 11;
            _booksService.DeleteBookById(id);

            Assert.That(_context.Books.FirstOrDefault(b => b.Id == id), Is.EqualTo(null));
        }

        public void ClenUp()
        {
            _context.Database.EnsureDeleted();
        }

        private void SeedDatabase()
        {
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
                },
                new Book()
                {
                    Id = 4,
                    Title = "Book 4",
                    Description = "Description 4",
                    IsRead = true,
                    DateRead = System.DateTime.Now,
                    Rate = 4,
                    Genre = "Genre 4",
                    CoverUrl = "CoverUrl 4",
                    DateAdded = System.DateTime.Now,
                    PublisherId = 4
                },
                new Book()
                {
                    Id = 5,
                    Title = "Book 5",
                    Description = "Description 5",
                    IsRead = true,
                    DateRead = System.DateTime.Now,
                    Rate = 5,
                    Genre = "Genre 5",
                    CoverUrl = "CoverUrl 5",
                    DateAdded = System.DateTime.Now,
                    PublisherId = 5
                },
                new Book()
                {
                    Id = 6,
                    Title = "Book 6",
                    Description = "Description 6",
                    IsRead = true,
                    DateRead = System.DateTime.Now,
                    Rate = 6,
                    Genre = "Genre 6",
                    CoverUrl = "CoverUrl 6",
                    DateAdded = System.DateTime.Now,
                    PublisherId = 6
                },
                new Book()
                {
                    Id = 7,
                    Title = "Book 7",
                    Description = "Description 7",
                    IsRead = true,
                    DateRead = System.DateTime.Now,
                    Rate = 7,
                    Genre = "Genre 7",
                    CoverUrl = "CoverUrl 7",
                    DateAdded = System.DateTime.Now,
                    PublisherId = 7
                },
                new Book()
                {
                    Id = 8,
                    Title = "Book 8",
                    Description = "Description 8",
                    IsRead = true,
                    DateRead = System.DateTime.Now,
                    Rate = 8,
                    Genre = "Genre 8",
                    CoverUrl = "CoverUrl 8",
                    DateAdded = System.DateTime.Now,
                    PublisherId = 8
                },
                new Book()
                {
                    Id = 9,
                    Title = "Book 9",
                    Description = "Description 9",
                    IsRead = true,
                    DateRead = System.DateTime.Now,
                    Rate = 9,
                    Genre = "Genre 9",
                    CoverUrl = "CoverUrl 9",
                    DateAdded = System.DateTime.Now,
                    PublisherId = 9
                },
                new Book()
                {
                    Id = 10,
                    Title = "Book 10",
                    Description = "Description 10",
                    IsRead = true,
                    DateRead = System.DateTime.Now,
                    Rate = 10,
                    Genre = "Genre 10",
                    CoverUrl = "CoverUrl 10",
                    DateAdded = System.DateTime.Now,
                    PublisherId = 10
                }
            };
            var publishers = new List<Publisher>
            {
                new Publisher()
                {
                    Id = 11,
                    Name = "Publisher 11"
                }
            };
            _context.SaveChanges();
        }
    }
}
