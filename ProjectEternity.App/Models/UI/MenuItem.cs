using System;
using Windows.UI.Xaml.Controls;

namespace ProjectEternity.App.Models.UI
{
    public class MenuItem
    {
        public Symbol Icon { get; set; }
        public string Name { get; set; }
        public Type Page { get; set; }
    }
}