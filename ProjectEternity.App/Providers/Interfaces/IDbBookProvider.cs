using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectEternity.App.Models;

namespace ProjectEternity.App.Providers.Interfaces
{
    public interface IDbBookProvider
    {
        Task<Book> GetBook(Guid bookId);
        Task<IEnumerable<Book>> GetBooks();
        Task<IEnumerable<Book>> GetBooks(int days);

        Task DeleteBook(Book book);
        Task DeleteBooks(IEnumerable<Book> books);

        Task SaveBook(Book book);
    }
}