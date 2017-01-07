using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectEternity.App.Managers.Interfaces;
using ProjectEternity.App.Models;
using ProjectEternity.App.Providers.Interfaces;
using ProjectEternity.FictionBookLibrary;

namespace ProjectEternity.App.Managers
{
    public class BookManager : IBookManager
    {
        #region Private Members

        private readonly IDbBookProvider _dbBookProvider;
        private readonly IStorageBookProvider _storageBookProvider;

        #endregion

        public BookManager(IDbBookProvider dbBookProvider, IStorageBookProvider storageBookProvider)
        {
            _dbBookProvider = dbBookProvider;
            _storageBookProvider = storageBookProvider;
        }

        #region Implementation of IBookManager

        public async Task<Book> ImportBook()
        {
            return await _storageBookProvider.ImportBook();
        }
        public async Task<IEnumerable<Book>> ImportBooks()
        {
            return await _storageBookProvider.ImportBooks();
        }

        public async Task<Book> GetBook(Guid bookId)
        {
            return await _dbBookProvider.GetBook(bookId);
        }
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _dbBookProvider.GetBooks();
        }
        public async Task<IEnumerable<Book>> GetBooks(int days)
        {
            return await _dbBookProvider.GetBooks(days);
        }

        public async Task DeleteBook(Book book)
        {
            await _storageBookProvider.DeleteBook(book);
            await _dbBookProvider.DeleteBook(book);
        }
        public async Task DeleteBooks(IEnumerable<Book> books)
        {
            await _storageBookProvider.DeleteBooks(books);
            await _dbBookProvider.DeleteBooks(books);
        }

        public async Task<FictionBook> LoadFictionBook(Book book)
        {
            return await _storageBookProvider.LoadFictionBook(book);
        }

        #endregion
    }
}