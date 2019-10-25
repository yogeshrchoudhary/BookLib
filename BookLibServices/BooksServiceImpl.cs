using Microsoft.Extensions.Configuration;
using OpenLibraryApiClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookLibServices
{
    internal class BooksServiceImpl: IBooksService
    {
        private IBookStore _bookStore;
        private IConfiguration _configuration;

        public BooksServiceImpl(IBookStore bookStore, IConfiguration configuration)
        {
            _bookStore = bookStore;
            _configuration = configuration;
        }

        public IEnumerable<BookDto> GetAll()
        {
            var result = _bookStore.GetAllBooks().Result;
            return result.Select(b => new BookDto {
                Id = IsbnToUlong(b.Isbn),
                Name = b.Name });
        }

        public BookDto GetSingle(ulong isbn)
        {
            try
            {
                var result = _bookStore.GetBooks(new[] { isbn }).Result.FirstOrDefault();
                return new BookDto { Id = IsbnToUlong(result.Isbn), Name = result.Name };
            }
            catch(IsbnNotFoundException e)
            {
                throw new ArgumentOutOfRangeException(e.Message, e);
            }
        }

        private ulong IsbnToUlong(string isbn)
        {
            return ulong.Parse(isbn.Replace("ISBN:", string.Empty));
        }
    }
}
