using BookShop.Data.Services;
using BookShop.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookShop.Controllers
{
    public class AuthorsController : ControllerBase
    {
        private AuthorsService _authorsService;

        public AuthorsController(AuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpGet("get-all-authors")]
        public IActionResult GetAllBooks()
        {
            var _allBooks = _authorsService.GetAllAuthors();
            return Ok(_allBooks);
        }

        [HttpPost("add-author")]
        public IActionResult AddAuthor([FromBody] AuthorVM author)
        {
            _authorsService.AddAuthor(author);
            return Ok();
        }

        [HttpGet("get-author-with-books-by-id/{id}")]
        public IActionResult GetAuthorWithBooks(int id)
        {
            var _author = _authorsService.GetAuthorWithBooks(id);

            if (_author != null)
            {
                return Ok(_author);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("get-author-by-id/{id}")]
        public IActionResult GetAuthorById(int id)
        {
            var _author = _authorsService.GetAuthorById(id);
            if (_author != null)
            {
                return Ok(_author);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("update-author-by-id/{id}")]
        public IActionResult UpdateAuthorById(int id, [FromBody] AuthorVM author)
        {
            var _author = _authorsService.UpdateAuthorById(id, author);
            return Ok(_author);
        }

        [HttpDelete("delete-author-by-id/{id}")]
        public IActionResult DeleteAuthorById(int id)
        {
            try
            {
                _authorsService.DeleteAuthorById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
