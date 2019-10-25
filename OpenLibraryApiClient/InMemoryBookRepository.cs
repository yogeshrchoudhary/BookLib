using System.Collections.Generic;
using System.Linq;

namespace OpenLibraryApiClient
{
    internal class InMemoryBookRepository
    {
        private static readonly IEnumerable<ulong> _localInMemoryBookIsbns = new[] {
                9780132350884UL,
                9780321146533UL,
                9781617294549UL,
                9780596007126UL,
                9780201485677UL,
                9780321125217UL,
                9780439139595UL,
                9780545010221UL,
                9780439784542UL,
                9780747545118UL,
                9780747538493UL
            };

        internal static IEnumerable<ulong> GetAllBookIds()
        {
            return _localInMemoryBookIsbns;
        }

        internal static void ValidateIsbnNumbers(IEnumerable<ulong> isbnNumbers)
        {
            foreach (var isbn in isbnNumbers)
            {
                if (!_localInMemoryBookIsbns.Contains(isbn))
                {
                    throw new IsbnNotFoundException($"'ISBN:{isbn}' NOT FOUND!!!");
                }
            }
        }
    }
}