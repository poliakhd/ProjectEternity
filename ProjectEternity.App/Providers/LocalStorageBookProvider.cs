using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Microsoft.Toolkit.Uwp;
using ProjectEternity.App.Core;
using ProjectEternity.App.Models;
using ProjectEternity.App.Providers.Interfaces;
using ProjectEternity.FictionBookLibrary;

namespace ProjectEternity.App.Providers
{
    public class LocalStorageBookProvider
        : IStorageBookProvider
    {
        #region Private Members

        private readonly IDbBookProvider _localDbBookProvider;

        #endregion

        public LocalStorageBookProvider(IDbBookProvider localDbBookProvider)
        {
            _localDbBookProvider = localDbBookProvider;
        }

        #region Importing books

        public async Task<Book> ImportBook()
        {
            var pickedFile = await GetBookAsPickedFile();

            if (pickedFile == null)
                return null;

            return await ImportingBookProcess(pickedFile);
        }

        public async Task<IEnumerable<Book>> ImportBooks()
        {
            var pickedFolder = await GetBooksAsPickedFolder();
            var pickedFoderFiles = await pickedFolder.GetFilesAsync();

            return await ImportingBooksProcess(pickedFoderFiles);
        }

        private async Task<Book> ImportingBookProcess(StorageFile pickedFile)
        {
            try
            {
                var fictionBook = await DeserializeAsFictionBook(pickedFile);

                var book = ToBook(fictionBook);
                var cover = GetCover(fictionBook);

                var bookPath = await SaveBookToFolder(pickedFile.Name, book.Id.ToString(), fictionBook, cover);
                await SaveBookToDb(book, bookPath);

                return book;
            }
            catch (Exception)
            {
                return new Book() {Title = "Not imported", Author = pickedFile.Name, BookPath = string.Empty, FolderPath = string.Empty, CoverPath = string.Empty, IsImported = false};
            }
            finally
            {
                Extensions.UnloadFromMemory();
            }
        }

        private async Task<IEnumerable<Book>> ImportingBooksProcess(IReadOnlyList<StorageFile> pickedFoderFiles)
        {
            var loadedBooks = new List<Book>();

            foreach (var pickedFile in pickedFoderFiles)
                loadedBooks.Add(await ImportingBookProcess(pickedFile));

            return loadedBooks;
        }

        private async Task<StorageFolder> GetBooksAsPickedFolder()
        {
            var folderPicker = new FolderPicker() { FileTypeFilter = { ".fb2" } };
            return await folderPicker.PickSingleFolderAsync();
        }

        private async Task<StorageFile> GetBookAsPickedFile()
        {
            var filePicker = new FileOpenPicker { FileTypeFilter = { ".fb2" } };
            return await filePicker.PickSingleFileAsync();
        }

        private async Task<FictionBook> DeserializeAsFictionBook(StorageFile pickedFile)
        {
            using (var book = await pickedFile.OpenStreamForReadAsync())
            {
                return book.Deserialize<FictionBook>();
            }
        }

        private Book ToBook(FictionBook fictionBook)
        {
            var title = fictionBook.Description.TitleInfo.BookTitle.Text;
            var author = fictionBook.Description.TitleInfo.Author[0].Items.Select(x => x.Text).Aggregate((current, next) => current + " " + next);

            var book = new Book
            {
                Id = Guid.NewGuid(),
                Author = author,
                Title = title,
                LastOpenedTime = DateTime.Now,
                IsImported = true,
                Type = BookType.FictionBook
            };

            return book;
        }

        private byte[] GetCover(FictionBook fictionBook)
        {
            return fictionBook.Binary.FirstOrDefault(x => x.Id == fictionBook.Description.TitleInfo.Coverpage[0].Href.Replace("#", "")).Value;
        }

        private async Task SaveBookToDb(Book book, string bookPath)
        {
            book.BookPath = bookPath;
            book.CoverPath = $"{bookPath}.png";
            book.FolderPath = book.Id.ToString();

            await _localDbBookProvider.SaveBook(book);
        }

        #endregion

        #region Saving books

        public async Task<string> SaveBookToFolder(string fileName, string directory, FictionBook book, byte[] cover)
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            localFolder = await localFolder.CreateFolderAsync("Books", CreationCollisionOption.OpenIfExists);
            localFolder = await localFolder.CreateFolderAsync(directory, CreationCollisionOption.OpenIfExists);

            
            await localFolder.WriteBytesToFileAsync(book.Serialize(), fileName);
            await localFolder.WriteBytesToFileAsync(cover, fileName + ".png");

            foreach (var binaryValue in book.Binary)
                if (binaryValue.ContentType == "image/jpeg")
                    await localFolder.WriteBytesToFileAsync(binaryValue.Value, binaryValue.Id + ".png");

            return $@"Books\{directory}\{fileName}";
        }

        #endregion

        #region Deleting books

        public async Task DeleteBook(Book book)
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            localFolder = await localFolder.GetFolderAsync("Books");
            localFolder = await localFolder.GetFolderAsync(book.FolderPath ?? book.Id.ToString());

            await localFolder.DeleteAsync(StorageDeleteOption.PermanentDelete);
        }
        public async Task DeleteBooks(IEnumerable<Book> booksToDelete)
        {
            foreach (var book in booksToDelete)
            {
                var localFolder = ApplicationData.Current.LocalFolder;
                localFolder = await localFolder.GetFolderAsync("Books");
                localFolder = await localFolder.GetFolderAsync(book.FolderPath ?? book.Id.ToString());

                await localFolder.DeleteAsync(StorageDeleteOption.PermanentDelete);
            }
        }

        #endregion

        #region Loading books

        public async Task<FictionBook> LoadFictionBook(Book bookModel)
        {
            var localFolder = ApplicationData.Current.LocalFolder;

            using (var book = await localFolder.OpenStreamForReadAsync(bookModel.BookPath))
            {
                return book.Deserialize<FictionBook>();
            }
        }
        public Task<string> LoadPlainBook(Book book)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}