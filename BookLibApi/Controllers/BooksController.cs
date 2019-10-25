using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace BookLibApi.Controllers
{
    using BookLibServices;

    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IBooksService _booksService;
        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        public ActionResult<IEnumerable<BookDto>> Get()
        {
            return new ActionResult<IEnumerable<BookDto>>(_booksService.GetAll());
        }

        [HttpGet("{isbn}")]
        public ActionResult<BookDto> Get(ulong isbn)
        {
            try
            {
                return new ActionResult<BookDto>(_booksService.GetSingle(isbn));
            }
            catch (ArgumentOutOfRangeException a)
            {
                return NotFound(a);
            }
        }
    }
}