using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectEternity.App.Core;
using ProjectEternity.App.Models;
using ProjectEternity.App.Providers.Interfaces;

namespace ProjectEternity.App.Providers
{
    public class AppDbBookProvider 
        : IDbBookProvider
    {
        #region Private Members

        private readonly AppDbContext _dbContext;

        #endregion

        public AppDbBookProvider(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        #region Getting books

        public async Task<Book> GetBook(Guid bookId)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == bookId);
        }

        public Task<IEnumerable<Book>> GetBooks()
        {
            return Task.FromResult(_dbContext.Books.AsEnumerable());
        }

        public Task<IEnumerable<Book>> GetBooks(int days)
        {
            return
                Task.FromResult(
                    _dbContext.Books.Where(x => x.LastOpenedTime >= DateTime.Now.Add(TimeSpan.FromDays(-days)))
                        .OrderByDescending(x => x.LastOpenedTime)
                        .AsEnumerable());
        }

        #endregion

        #region Deleting books

        public async Task DeleteBook(Book book)
        {
            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteBooks(IEnumerable<Book> books)
        {
            _dbContext.Books.RemoveRange(books);
            await _dbContext.SaveChangesAsync();
        }

        #endregion

        #region Saving books

        public async Task SaveBook(Book book)
        {
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();
        }

        #endregion
    }
}