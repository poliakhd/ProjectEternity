using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectEternity.App.Models;
using ProjectEternity.FictionBookLibrary;

namespace ProjectEternity.App.Providers.Interfaces
{
    public interface IStorageBookProvider
    {
        Task<IEnumerable<Book>> ImportBooks();
        Task<Book> ImportBook();

        Task<string> SaveBookToFolder(string fileName, string directory, FictionBook book, byte[] cover);

        Task DeleteBook(Book book);
        Task DeleteBooks(IEnumerable<Book> books);

        Task<FictionBook> LoadFictionBook(Book book);
        Task<string> LoadPlainBook(Book book);
    }
}