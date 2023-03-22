using BookShop.Data;
using BookShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using my_books.Data.Services;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace test_web_api.Tests
{
    class PublishersServiceTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                    .UseInMemoryDatabase(databaseName: "WepAPI-9")
                    .Options;

        AppDbContext _context;
        PublishersService _publishersService;
        int onPage = 6;

        [OneTimeSetUp]
        public void Setup()
        {
            _context = new AppDbContext(dbContextOptions);
            _context.Database.EnsureCreated();

            SeedDatabase();

            _publishersService = new PublishersService(_context);
        }

        [Test, Order(1)]
        public void GetAllPublishers_WithNoSort_WithNoSearch_WithNoPageNumber_Test()
        {
            var result = _publishersService.GetAllPublishers("", "", null);

            Assert.That(result.Count, Is.EqualTo(onPage));
        }

        [Test, Order(2)]
        public void GetAllPublishers_WithSort_WithNoSearch_WithNoPageNumber_Test()
        {
            var result = _publishersService.GetAllPublishers("name_desc", "", null);

            Assert.That(result.Count, Is.EqualTo(onPage));
            Assert.That(result.FirstOrDefault().Name, Is.EqualTo("Publisher 9"));
        }

        [Test, Order(3)]
        public void GetAllPublishers_WithNoSort_WithSearch_WithNoPageNumber_Test()
        {
            int countOfPublishers = _context.Publishers.Count();
            string rndSearch = "Publisher 1";
            var result = _publishersService.GetAllPublishers("", rndSearch, null);

            Assert.That(result.FirstOrDefault().Name, Is.EqualTo(rndSearch));
        }

        [Test, Order(4)]
        public void GetAllPublishers_WithNoSort_WithNoSearch_WithPageNumber_Test()
        {
            var result = _publishersService.GetAllPublishers("", "", 2);

            Assert.That(result.Count, Is.EqualTo(4));
        }

        [Test, Order(5)]
        public void GetAllPublishers_WithSort_WithNoSearch_WithPageNumber()
        {
            var result = _publishersService.GetAllPublishers("name_desc", "", 2);

            Assert.That(result.Count, Is.EqualTo(4));
            Assert.That(result.FirstOrDefault().Name, Is.EqualTo("Publisher 3"));
        }

        [Test, Order(6)]
        public void GetPublisherById()
        {
            int rndId = 3;
            var result = _publishersService.GetPublisherById(rndId);

            Assert.That(result, Is.EqualTo(_context.Publishers.First(p => p.Id == rndId)));
        }

        //[Test, Order(7)]
        //public void GetPublisherData()
        //{
        //int rndId = 3;
        //var result = _publishersService.GetPublisherData(rndId);

        //Assert.That(result == _context.Publishers.First(p => p.Id == rndId));
        //}

        [Test, Order(8)]
        public void AddPublisher()
        {
            string name = "Publisher 11";
            var result = _publishersService.AddPublisher(
                new BookShop.Data.ViewModels.PublisherVM()
                {
                    Name = name
                });

            Assert.That(result, Is.EqualTo(_context.Publishers.FirstOrDefault(p => p.Name == name)));
        }

        [Test, Order(9)]
        public void DeletePublisherById()
        {
            int id = 11;
            _publishersService.DeletePublisherById(id);

            Assert.That(_context.Publishers.FirstOrDefault(p => p.Id == id), Is.EqualTo(null));
        }

        [Test, Order(10)]
        public void UpdatePublisherById()
        {
            int id = 10;
            string name = "Publisher 11";
            _publishersService.UpdatePublisherById(id,
                new BookShop.Data.ViewModels.PublisherVM()
                {
                    Name = name
                });

            Assert.That(name,
                Is.EqualTo(_context.Publishers.FirstOrDefault(p => p.Id == id).Name));
        }

        [OneTimeTearDown]
        public void ClenUp()
        {
            _context.Database.EnsureDeleted();
        }

        private void SeedDatabase()
        {
            var publishers = new List<Publisher>
            {
                new Publisher()
                {
                    Id = 1,
                    Name = "Publisher 1"
                },
                new Publisher()
                {
                    Id = 2,
                    Name = "Publisher 2"
                },
                new Publisher()
                {
                    Id = 3,
                    Name = "Publisher 3"
                },
                new Publisher()
                {
                    Id = 4,
                    Name = "Publisher 4"
                },
                new Publisher()
                {
                    Id = 5,
                    Name = "Publisher 5"
                },
                new Publisher()
                {
                    Id = 6,
                    Name = "Publisher 6"
                },
                new Publisher()
                {
                    Id = 7,
                    Name = "Publisher 7"
                },
                new Publisher()
                {
                    Id = 8,
                    Name = "Publisher 8"
                },
                new Publisher()
                {
                    Id = 9,
                    Name = "Publisher 9"
                },
                new Publisher()
                {
                    Id = 10,
                    Name = "Publisher 10"
                },
            };
            _context.Publishers.AddRange(publishers);
            _context.SaveChanges();
        }
    }
}
