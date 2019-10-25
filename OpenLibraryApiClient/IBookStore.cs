using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenLibraryApiClient
{
    public interface IBookStore
    {
        Task<IEnumerable<BookItem>> GetBooks(IEnumerable<ulong> isbnNumbers);
        Task<IEnumerable<BookItem>> GetAllBooks();
    }
}