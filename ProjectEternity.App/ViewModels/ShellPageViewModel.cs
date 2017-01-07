using System;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using ProjectEternity.App.Models.UI;
using ProjectEternity.App.Core;
using ProjectEternity.App.Providers.Interfaces;

namespace ProjectEternity.App.ViewModels
{
    public sealed class ShellPageViewModel 
        : Screen
    {
        #region Private Members

        private readonly WinRTContainer _container;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMenuProvider _menuProvider;

        private INavigationService _navigation;

        private bool _paneOpen;
        private Type _currentViewModel;

        #endregion

        public BindableCollection<MenuItem> MainMenuItems => _menuProvider.GetMainItems().ToBindableCollection();
        public BindableCollection<MenuItem> OptionsMenuItems => _menuProvider.GetOptionItems().ToBindableCollection();

        public ShellPageViewModel(WinRTContainer container, IEventAggregator eventAggregator, IMenuProvider menuProvider)
        {
            _container = container;
            _eventAggregator = eventAggregator;
            _menuProvider = menuProvider;

            Initialize();
            NotifyProperties();
        }

        private void Initialize()
        {
            _eventAggregator.Subscribe(this);
        }
        private void NotifyProperties()
        {
            NotifyOfPropertyChange(nameof(MainMenuItems));
            NotifyOfPropertyChange(nameof(OptionsMenuItems));
        }

        #region Navigation

        private void SetupNavigation(Frame frame)
        {
            _navigation = _container.RegisterNavigationService(frame);
            _navigation.BackRequested += (sender, args) =>
            {
                if (_navigation.CanGoBack)
                    _navigation.GoBack();
                else
                    TryClose();
            };
        }

        #endregion

        #region Pane

        public bool PaneOpen
        {
            get { return _paneOpen; }
            set
            {
                _paneOpen = value;
                NotifyOfPropertyChange();
            }
        }

        private void MainMenuItemClick(object sender, ItemClickEventArgs eventArgs)
        {
            var menuItem = eventArgs.ClickedItem as MenuItem;

            if (menuItem.Page == typeof(LibraryPageViewModel) &&
                _currentViewModel != typeof(LibraryPageViewModel))
            {
                _navigation.For<LibraryPageViewModel>()
                    .Navigate();

                _currentViewModel = typeof(LibraryPageViewModel);
            }

            DoPaneBehavior();
        }
        private void OptionMenuItemClick(object sender, ItemClickEventArgs eventArgs)
        {
            DoPaneBehavior();
        }

        private void DoPaneBehavior()
        {
            PaneOpen = PaneOpen && !PaneOpen;
        }

        #endregion
    }
}