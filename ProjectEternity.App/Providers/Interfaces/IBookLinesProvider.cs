using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectEternity.App.Models;

namespace ProjectEternity.App.Providers.Interfaces
{
    public interface IBookLinesProvider
    {
        Task<IEnumerable<BookLine>> AsLines(Book book);
    }
}
