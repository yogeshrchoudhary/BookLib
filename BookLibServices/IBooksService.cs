using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibServices
{
    public interface IBooksService
    {
        IEnumerable<BookDto> GetAll();
        BookDto GetSingle(ulong isbn);
    }
}
