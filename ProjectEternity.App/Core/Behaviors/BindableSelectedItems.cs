using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Microsoft.Xaml.Interactivity;

namespace ProjectEternity.App.Core.Behaviors
{
    public class BindableSelectedItems 
        : Behavior<AdaptiveGridView>
    {
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems", typeof(IList<object>), typeof(BindableSelectedItems),
                new PropertyMetadata(null, null));
        
        public IList<object> SelectedItems
        {
            get { return (IList<object>)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        #region Overrides of Behavior<AdaptiveGridView>

        protected override void OnAttached()
        {
            AssociatedObject.SelectionChanged += AssociatedObjectOnSelectionChanged;
            AssociatedObject.RightTapped += AssociatedObjectOnRightTapped;
        }
        protected override void OnDetaching()
        {
            AssociatedObject.SelectionChanged -= AssociatedObjectOnSelectionChanged;
            AssociatedObject.RightTapped -= AssociatedObjectOnRightTapped;
        }

        private void AssociatedObjectOnSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            var adaptiveGridView = sender as AdaptiveGridView;

            if (adaptiveGridView != null)
                SelectedItems = adaptiveGridView.SelectedItems;
        }
        private void AssociatedObjectOnRightTapped(object sender, RightTappedRoutedEventArgs rightTappedRoutedEventArgs)
        {
            if(AssociatedObject.SelectionMode == ListViewSelectionMode.None)
            {
                var frameworkElement = rightTappedRoutedEventArgs.OriginalSource as FrameworkElement;

                if (frameworkElement != null)
                    AssociatedObject.SelectedItem = frameworkElement.DataContext;
            }
        }

        #endregion
    }
}