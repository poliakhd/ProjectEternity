using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Globalization;
using Windows.System.UserProfile;
using Windows.UI.Core;
using Caliburn.Micro;
using Microsoft.EntityFrameworkCore;
using ProjectEternity.App.Core;
using ProjectEternity.App.Formatters;
using ProjectEternity.App.Formatters.Interfaces;
using ProjectEternity.App.Managers;
using ProjectEternity.App.Managers.Interfaces;
using ProjectEternity.App.Providers;
using ProjectEternity.App.Providers.Interfaces;
using ProjectEternity.App.ViewModels;

namespace ProjectEternity.App
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App
    {
        #region Private Members

        private WinRTContainer _container;
        private IEventAggregator _eventAggregator;

        #endregion

        public App()
        {
            InitializeComponent();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        #region Overrides of CaliburnApplication

        protected override void Configure()
        {
            ApplicationLanguages.PrimaryLanguageOverride = GlobalizationPreferences.Languages[0];

            #region Migrations

            using (var db = new AppDbContext())
            {
                db.Database.Migrate();
            }

            #endregion

            _container = new WinRTContainer();
            _container.RegisterWinRTServices();

            _container
                .Singleton<ShellPageViewModel>()
                .Singleton<LibraryPageViewModel>()
                .PerRequest<BookPageViewModel>()
                .PerRequest<BookImportPageViewModel>()
                .Singleton<AppDbContext>()
                .Singleton<IMenuProvider, ShellMenuProvider>()
                .Singleton<IDbBookProvider, AppDbBookProvider>()
                .Singleton<IStorageBookProvider, LocalStorageBookProvider>()
                .Singleton<IBookLinesProvider, BookLinesProvider>()
                .Singleton<IFictionBookFormatter, FictionBookFormatter>()
                .Singleton<IBookManager, BookManager>();
//#if DEBUG
//                .Singleton<IInAppPurchase, SimulatorInAppPurchase>();
//#else
//                .Singleton<IInAppPurchase, MarketInAppPurchase>();
//#endif

            _eventAggregator = _container.GetInstance<IEventAggregator>();

            var navigation = SystemNavigationManager.GetForCurrentView();
            navigation.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            navigation.BackRequested += (o, e) =>
            {
                e.Handled = true;

                var n = IoC.Get<INavigationService>();
                if(n.CanGoBack) n.GoBack();
            };

        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (args.PreviousExecutionState == ApplicationExecutionState.Running)
                return;

            DisplayRootViewFor<ShellPageViewModel>();

            if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
            {
                //_eventAggregator.PublishOnUIThread(new ResumeMessage());
            }
        }

        protected override void OnSuspending(object sender, SuspendingEventArgs e)
        {
            //_eventAggregator.PublishOnUIThread(new SuspendMessage());
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        #endregion

        #region Overrides of Application

        protected override void OnFileActivated(FileActivatedEventArgs args)
        {
            if (args.PreviousExecutionState == ApplicationExecutionState.Running)
                return;

            //DisplayRootViewFor<ShellPageViewModel>();

            if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
            {
                //_eventAggregator.PublishOnUIThread(new ResumeMessage());
            }
        }

        #endregion
    }
}
