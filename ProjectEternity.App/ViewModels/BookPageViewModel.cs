using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;
using Caliburn.Micro;
using ProjectEternity.App.Core;
using ProjectEternity.App.Models;
using ProjectEternity.App.Providers.Interfaces;

namespace ProjectEternity.App.ViewModels
{
    public class BookPageViewModel : Screen, IHandle<Message<Book>>
    {
        #region Private Members

        private readonly IBookLinesProvider _bookLinesProvider;
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigation;

        private FlipView _flip;
        private bool _resizeProgressVisibility;
        private IEnumerable<BookLine> _lines;

        #endregion

        public Book Book { get; set; } = new Book();
        public bool ResizeProgressVisibility
        {
            get { return _resizeProgressVisibility; }
            set
            {
                _resizeProgressVisibility = value; 
                NotifyOfPropertyChange();
            }
        }

        public BookPageViewModel(IBookLinesProvider bookLineProvider, IEventAggregator eventAggregator, INavigationService navigation)
        {
            _bookLinesProvider = bookLineProvider;
            _eventAggregator = eventAggregator;
            _navigation = navigation;
        }

        #region Overrides of Screen
        
        protected override void OnActivate()
        {
            _eventAggregator.Subscribe(this);
        }
        protected override void OnDeactivate(bool close)
        {
            _eventAggregator.Unsubscribe(this);

            if (_flip != null)
            {
                _flip.Items?.Clear();
                Extensions.UnloadFromMemory();
            }
        }

        #endregion

        #region Implementation of IHandle<BookToRead>

        public async void Handle(Message<Book> message)
        {
            _lines = await _bookLinesProvider.AsLines(message.Value);
            DisplayBook(_flip.RenderSize);
        }

        #endregion

        private void BookPageSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            if (_flip == null)
                _flip = sender as FlipView;

            DisplayBook(sizeChangedEventArgs.NewSize);
        }
        private async void DisplayBook(Size containerSize)
        {
            ResizeProgressVisibility = true;

            await Task.Delay(100);
            await Process(containerSize);

            Extensions.UnloadFromMemory();

            ResizeProgressVisibility = false;
        }

        private Task Process(Size containerSize)
        {
            if (_flip.Items != null && _flip.Items.Count == 0)
            {
                var layout = CreateLayout();
                AddParagraph(layout);
                AddLines(layout);
                AddPages(containerSize, layout);

                _lines = null;
            }
            else
            {
                foreach (var obj in _flip.Items.AsNotNull())
                    (obj as FrameworkElement).Measure(containerSize);

                while (((_flip.Items[_flip.Items.Count - 1] as FlipViewItem).Content as RichTextBlockOverflow).HasOverflowContent)
                {
                    var richTextBlockOverflow = (_flip.Items[_flip.Items.Count - 1] as FlipViewItem).Content as RichTextBlockOverflow;

                    var newRichTextBlockOverflow = new RichTextBlockOverflow();
                    richTextBlockOverflow.OverflowContentTarget = newRichTextBlockOverflow;
                    richTextBlockOverflow = newRichTextBlockOverflow;
                    _flip.Items.Add(new FlipViewItem() { Content = richTextBlockOverflow});

                    richTextBlockOverflow.Measure(containerSize);
                }

                // Удаление избыточных элементов RichTextBlockOverflow
                while (!((_flip.Items[_flip.Items.Count - 2] as FlipViewItem).Content as RichTextBlockOverflow).HasOverflowContent)
                {
                    _flip.Items.RemoveAt(_flip.Items.Count - 1);
                }
            }

            return Task.FromResult(true);
        }

        private static RichTextBlock CreateLayout()
        {
            return new RichTextBlock
            {
                FontSize = 15,
                Foreground = new SolidColorBrush(Colors.Black)
            };
        }
        private static void AddParagraph(RichTextBlock layout)
        {
            layout.Blocks.Add(new Paragraph { Margin = new Thickness(12) });
        }
        private void AddLines(RichTextBlock layout)
        {
            foreach (var line in _lines)
                layout.Blocks.Add(line.Value);
        }
        private void AddPages(Size containerSize, RichTextBlock layout)
        {
            if (_flip.Items != null)
            {
                _flip.Items.Add(new FlipViewItem { Content = layout });
                layout.Measure(containerSize);

                if (layout.HasOverflowContent)
                {
                    var richTextBlockOverflow = new RichTextBlockOverflow();
                    layout.OverflowContentTarget = richTextBlockOverflow;
                    _flip.Items.Add(new FlipViewItem() { Content = richTextBlockOverflow });
                    richTextBlockOverflow.Measure(containerSize);

                    while (richTextBlockOverflow.HasOverflowContent)
                    {
                        var newRichTextBlockOverflow = new RichTextBlockOverflow();
                        richTextBlockOverflow.OverflowContentTarget = newRichTextBlockOverflow;
                        richTextBlockOverflow = newRichTextBlockOverflow;
                        _flip.Items.Add(new FlipViewItem() { Content = richTextBlockOverflow });
                        richTextBlockOverflow.Measure(containerSize);
                    }
                }
            }
        }
    }
}