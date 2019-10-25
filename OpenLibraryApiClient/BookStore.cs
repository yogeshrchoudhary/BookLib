using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OpenLibraryApiClient
{
    internal class BookStore : IBookStore
    {
        private string _hostUrl;
        private IConfiguration _configuration;

        public BookStore(IConfiguration configuration)
        {
            _configuration = configuration;
            _hostUrl = _configuration["BookLibraryUrl"];
        }

        #region IBookStore implementation
        public async Task<IEnumerable<BookItem>> GetBooks(IEnumerable<ulong> isbnNumbers)
        {
            InMemoryBookRepository.ValidateIsbnNumbers(isbnNumbers);
            string requestUri = $"/api/books?bibkeys={CombineIsbnNumbers(isbnNumbers)}&format=json";
            return await SendHttpRequest(requestUri);
        }

        public async Task<IEnumerable<BookItem>> GetAllBooks()
        {
            var inMemoryBookIds = InMemoryBookRepository.GetAllBookIds();
            return await SendHttpRequest(
                $"/api/books?bibkeys={CombineIsbnNumbers(inMemoryBookIds)}&format=json"
                );
        }
        #endregion

        #region Helper methods
        private class OpenLibItem
        {
            public string bib_key { get; set; }
            public string preview { get; set; }
            public string thumbnail_url { get; set; }
            public string preview_url { get; set; }
            public string info_url { get; set; }
        }

        private static string CombineIsbnNumbers(IEnumerable<ulong> isbnNumbers)
        {
            return string.Join<string>(",", isbnNumbers.Select((i) => $"ISBN:{i}"));
        }

        private async Task<IEnumerable<BookItem>> SendHttpRequest(string booksRequestUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_hostUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(booksRequestUrl);

                var result = await response.Content.ReadAsStringAsync();
                var books = JsonConvert.DeserializeObject<Dictionary<string, OpenLibItem>>(result);
                return books.Select(b => new BookItem
                {
                    Isbn = b.Key,
                    Name = new Uri(b.Value.info_url).Segments.Last().Replace('_', ' ')
                });
            }
        }
        #endregion
    }
}
