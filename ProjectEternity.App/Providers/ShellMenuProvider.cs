using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using ProjectEternity.App.Models.UI;
using ProjectEternity.App.Providers.Interfaces;
using ProjectEternity.App.ViewModels;

namespace ProjectEternity.App.Providers
{
    public class ShellMenuProvider 
        : IMenuProvider
    {
        #region Implementation of IMenuProvider

        public IEnumerable<MenuItem> GetMainItems()
        {
            yield return new MenuItem() {Icon = Symbol.Library, Name = "Library", Page = typeof(LibraryPageViewModel)};
        }

        public IEnumerable<MenuItem> GetOptionItems()
        {
            yield return new MenuItem() {Icon = Symbol.Setting, Name = "Settings", Page = typeof(ShellPageViewModel)};

            //var symbol = Symbol.Library;
        }

        #endregion
    }
}