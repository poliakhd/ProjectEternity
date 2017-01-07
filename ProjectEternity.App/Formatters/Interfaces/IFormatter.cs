using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectEternity.App.Models;

namespace ProjectEternity.App.Formatters.Interfaces
{
    public interface IFormatter
    {
        Task<IEnumerable<BookLine>> AsLines(Book book);
    }
}