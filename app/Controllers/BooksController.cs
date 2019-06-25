using App.Models;
using App.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult<List<Book>> Get() =>
            _bookService.Get();

        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Book> Get(string id)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public ActionResult<Book> Create(BookPayload book)
        {
            return Ok(_bookService.Create(Mapper.Map<Book>(book)));
        }

        [HttpPut("{id:length(24)}")]    
        public IActionResult Update(string id, BookPayload bookIn)
        {

            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookService.Update(id, Mapper.Map<Book>(bookIn));

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookService.Remove(book.Id);

            return NoContent();
        }
    }
}