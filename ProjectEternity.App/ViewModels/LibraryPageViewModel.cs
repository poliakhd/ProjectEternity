using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using ProjectEternity.App.Core;
using ProjectEternity.App.Managers.Interfaces;
using ProjectEternity.App.Models;

namespace ProjectEternity.App.ViewModels
{
    public class LibraryPageViewModel
         : Screen, IHandle<Message<IEnumerable<Book>>>
    {
        #region Private Members

        private readonly IBookManager _bookManager;
        private readonly INavigationService _navigation;
        private readonly IEventAggregator _eventAggregator;

        private BindableCollection<Book> _recentBooks;
        private BindableCollection<Book> _allBooks;

        private bool _booksClickEnabled = true;

        private ListViewSelectionMode _booksSelectionMode = ListViewSelectionMode.None;

        #endregion

        public BindableCollection<Book> RecentBooks => _recentBooks;
        public BindableCollection<Book> AllBooks => _allBooks;

        public Book SelectedBook { get; set; }
        public IList<object> SelectedBooks { get; set; }

        public bool BooksClickEnabled
        {
            get { return _booksClickEnabled; }
            set
            {
                _booksClickEnabled = value;
                NotifyOfPropertyChange();
            }
        }
        public ListViewSelectionMode BooksSelectionMode
        {
            get { return _booksSelectionMode; }
            set
            {
                _booksSelectionMode = value;
                NotifyOfPropertyChange();
            }
        }

        public LibraryPageViewModel(IBookManager bookManager, INavigationService navigation, IEventAggregator eventAggregator)
        {
            _bookManager = bookManager;
            _navigation = navigation;
            _eventAggregator = eventAggregator;

            Initialize();

            UpdateLibraries();
        }

        private void Initialize()
        {
            _eventAggregator.Subscribe(this);
        }

        public async void UpdateLibraries()
        {
            _recentBooks = new BindableCollection<Book>(await _bookManager.GetBooks(7));
            _allBooks = new BindableCollection<Book>(await _bookManager.GetBooks());
        }

        public void AddBook()
        {
            _navigation
                .For<BookImportPageViewModel>()
                .WithParam(x => x.ImportType, ImportType.File)
                .Navigate();

            Extensions.UnloadFromMemory();
        }

        public void AddBooks()
        {
            _navigation
                .For<BookImportPageViewModel>()
                .WithParam(x => x.ImportType, ImportType.Folder)
                .Navigate();

            Extensions.UnloadFromMemory();
        }

        public async void DeleteBooks()
        {
            var selectedBooks = SelectedBooks?.Cast<Book>();
            if (selectedBooks?.Count() > 1)
            {
                await _bookManager.DeleteBooks(selectedBooks);

                _allBooks.RemoveRange(selectedBooks);
                _recentBooks.RemoveRange(selectedBooks);
            }
            else
            {
                await _bookManager.DeleteBook(SelectedBook);

                _allBooks.Remove(SelectedBook);
                _recentBooks.Remove(SelectedBook);
            }

            BooksSelectionMode = ListViewSelectionMode.None;
            BooksClickEnabled = true;

            NotifyOfPropertyChange(nameof(RecentBooks));
            NotifyOfPropertyChange(nameof(AllBooks));
        }
        public void SelectionMode()
        {
            BooksClickEnabled = false;

            BooksClickEnabled = BooksSelectionMode != ListViewSelectionMode.Multiple;
            BooksSelectionMode = BooksSelectionMode == ListViewSelectionMode.None ? ListViewSelectionMode.Multiple : ListViewSelectionMode.None;

            NotifyOfPropertyChange(nameof(BooksSelectionMode));
        }

        public async void OpenBook(ItemClickEventArgs eventArgs)
        {
            if(BooksSelectionMode == ListViewSelectionMode.Multiple)
                return;

            var book = eventArgs.ClickedItem as Book;

            _navigation
                .For<BookPageViewModel>()
                .Navigate();

            await _eventAggregator.PublishOnUIThreadAsync(new Message<Book>(book));

            Extensions.UnloadFromMemory();
        }

        public void Handle(Message<IEnumerable<Book>> message)
        {
            _recentBooks.AddRange(message.Value);
            _allBooks.AddRange(message.Value);

            NotifyOfPropertyChange(nameof(RecentBooks));
            NotifyOfPropertyChange(nameof(AllBooks));
        }
    }
}