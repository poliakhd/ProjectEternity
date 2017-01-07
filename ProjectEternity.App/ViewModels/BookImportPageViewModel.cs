using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using ProjectEternity.App.Managers.Interfaces;
using ProjectEternity.App.Models;

namespace ProjectEternity.App.ViewModels
{
    public enum ImportType
    {
        File,
        Folder
    }

    public class BookImportPageViewModel 
        : Screen
    {
        #region Private Members

        private readonly IBookManager _bookManager;
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;

        private bool _importingProgressVisibility = true;

        #endregion

        public ImportType ImportType { get; set; }
        public BindableCollection<Book> ImportedBooks { get; set; }

        public bool ImportingProgressVisibility
        {
            get { return _importingProgressVisibility; }
            set
            {
                _importingProgressVisibility = value;
                NotifyOfPropertyChange();
            }
        }

        public BookImportPageViewModel(IBookManager bookManager, INavigationService navigationService, IEventAggregator eventAggregator)
        {
            _bookManager = bookManager;
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
        }

        protected override async void OnViewReady(object view)
        {
            switch (ImportType)
            {
                case ImportType.File:
                    var importedBook = await _bookManager.ImportBook();
                    ImportedBooks.Add(importedBook);
                    break;
                case ImportType.Folder:
                    var importedBooks = await _bookManager.ImportBooks();
                    ImportedBooks.AddRange(importedBooks);
                    break;
            }

            NotifyOfPropertyChange(nameof(ImportedBooks));
            ImportingProgressVisibility = false;
        }
        private async void GoToLibrary()
        {
            await _eventAggregator.PublishOnUIThreadAsync(new Message<IEnumerable<Book>>(ImportedBooks.Where(x => x.IsImported)));
            _navigationService.GoBack();
        }
    }
}
