using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectEternity.App.Formatters.Interfaces;
using ProjectEternity.App.Models;
using ProjectEternity.App.Providers.Interfaces;

namespace ProjectEternity.App.Providers
{
    public class BookLinesProvider : IBookLinesProvider
    {
        private readonly IFictionBookFormatter _fictionBookFormatter;

        public BookLinesProvider(IFictionBookFormatter fictionBookFormatter)
        {
            _fictionBookFormatter = fictionBookFormatter;
        }

        #region Implementation of IBookLineProvider

        public async Task<IEnumerable<BookLine>> AsLines(Book book)
        {
            switch (book.Type)
            {
                case BookType.FictionBook:
                    return await _fictionBookFormatter.AsLines(book);
                default:
                    return null;
            }
        }

        #endregion
    }
}