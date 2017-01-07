using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectEternity.App.Models;
using ProjectEternity.FictionBookLibrary;

namespace ProjectEternity.App.Managers.Interfaces
{
    public interface IBookManager
    {
        Task<Book> ImportBook();
        Task<IEnumerable<Book>> ImportBooks();

        Task<Book> GetBook(Guid bookId);
        Task<IEnumerable<Book>> GetBooks();
        Task<IEnumerable<Book>> GetBooks(int days);

        Task DeleteBook(Book book);
        Task DeleteBooks(IEnumerable<Book> books);


        Task<FictionBook> LoadFictionBook(Book book);
    }
}