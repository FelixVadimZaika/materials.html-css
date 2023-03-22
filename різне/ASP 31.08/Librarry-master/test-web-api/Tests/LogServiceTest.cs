using BookShop.Data;
using BookShop.Data.Models;
using BookShop.Data.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_web_api.Tests
{
    class LogServiceTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                      .UseInMemoryDatabase(databaseName: "WepAPI-9")
                      .Options;

        AppDbContext _context;
        LogsService _logsService;
        int onPage = 6;

        [OneTimeSetUp]
        public void Setup()
        {
            _context = new AppDbContext(dbContextOptions);
            _context.Database.EnsureCreated();

            SeedDatabase();

            _logsService = new LogsService(_context);
        }

        [Test, Order(22)]
        public void GetAllLogsFromDB_Test()
        {
            var result =_logsService.GetAllLogsFromDB();

            Assert.That(result, Is.EqualTo(_context.Logs.ToList()));
        }

        private void SeedDatabase()
        {
            var logs = new List<Log>
            {
               new Log()
               {
                   Id = 1,
                   Message = "Message 1",
                   MessageTemplate = "Message template 1",
                   Level = "Level 1",
                   TimeStamp = DateTime.Now,
                   Exception = "Exception 1",
                   Properties = "Properties 1",
                   LogEvent = "Log event 1"
               },
               new Log()
               {
                   Id = 2,
                   Message = "Message 2",
                   MessageTemplate = "Message template 2",
                   Level = "Level 2",
                   TimeStamp = DateTime.Now,
                   Exception = "Exception 2",
                   Properties = "Properties 2",
                   LogEvent = "Log event 2"
               }
            };
            _context.Logs.AddRange(logs);
            _context.SaveChanges();
        }
    }
}
