using Microsoft.EntityFrameworkCore;
using ProjectEternity.App.Models;

namespace ProjectEternity.App.Core
{
    public class AppDbContext
        : DbContext
    {
        public DbSet<Book> Books { get; set; }

        #region Overrides of DbContext

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Books.db");
        }

        #endregion
    }
}