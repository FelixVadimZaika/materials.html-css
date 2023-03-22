using BookShop.Data.Models;
using BookShop.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookShop.Data.Services
{
    public class AuthorsService
    {
        private AppDbContext _context;
        public AuthorsService(AppDbContext context)
        {
            _context = context;
        }

        public void AddAuthor(AuthorVM author)
        {
            var _author = new Author()
            {
                FullName = author.FullName
            };
            _context.Authors.Add(_author);
            _context.SaveChanges();
        }

        public AuthorVM GetAuthorById(int authorId)
        {
            Author author = _context.Authors.FirstOrDefault(b => b.Id == authorId);
            return new AuthorVM()
            {
                FullName = author.FullName
            };
        }

        public List<Author> GetAllAuthors() => _context.Authors.ToList();

        public AuthorWithBooksVM GetAuthorWithBooks(int authorId)
        {
            var _author = _context.Authors.Where(n => n.Id == authorId)
                .Select(n => new AuthorWithBooksVM()
                {
                    FullName = n.FullName,
                    BookTitles = n.Book_Authors.Select(n => n.Book.Title).ToList()
                }).FirstOrDefault();

            return _author;
        }

        public Author UpdateAuthorById(int id, AuthorVM author)
        {
            var _author = _context.Authors.FirstOrDefault(n => n.Id == id);
            if (_author != null)
            {
                _author.FullName = author.FullName;

                _context.SaveChanges();
            }

            return _author;
        }

        public void DeleteAuthorById(int authorId)
        {
            var _author = _context.Authors.FirstOrDefault(n => n.Id == authorId);
            if (_author != null)
            {
                _context.Authors.Remove(_author);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"Author with id: {authorId} not found");
            }
        }
    }
}
