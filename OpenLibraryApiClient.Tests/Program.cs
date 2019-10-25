using Microsoft.Extensions.Configuration;
using Moq;
using System;

namespace OpenLibraryApiClient.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("In main!");

            var mockConfig = new Mock<IConfiguration>();
            mockConfig.As<IConfiguration>().Setup(m => m["BookLibraryUrl"]).Returns("http://openlibrary.org");

            BookStore bookStore = new BookStore(mockConfig.Object);
            var result = bookStore.GetBooks(new[] { 9780439139595UL, 9780545010221UL, 9780545010222UL }).Result;
            foreach (var item in result)
                Console.WriteLine($"{item.Isbn} >> {item.Name}");
        }

    }
}
