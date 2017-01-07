using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using ProjectEternity.App.Core;
using ProjectEternity.App.Formatters.Interfaces;
using ProjectEternity.App.Managers.Interfaces;
using ProjectEternity.App.Models;
using ProjectEternity.FictionBookLibrary.Models.Base;
using ProjectEternity.FictionBookLibrary.Models.Formatings;
using ProjectEternity.FictionBookLibrary.Models.Values;
using LinkType = ProjectEternity.FictionBookLibrary.Models.Formatings.LinkType;

namespace ProjectEternity.App.Formatters
{
    public class FictionBookFormatter 
        : IFictionBookFormatter
    {
        private readonly IBookManager _bookManager;

        public FictionBookFormatter(IBookManager bookManager)
        {
            _bookManager = bookManager;
        }

        public async Task<IEnumerable<BookLine>> AsLines(Book book)
        {
            var fictionBook = await _bookManager.LoadFictionBook(book);
            var lines = new List<BookLine>();

            foreach (var fictionBookBody in fictionBook.Body)
            {
                foreach (var section in fictionBookBody.Section)
                {
                    foreach (var item in section.Title.Items.AsNotNull())
                    {
                        var paragraph = new Paragraph();
                        paragraph.Inlines.Add(ProcessBaseItems(item));

                        lines.Add(new BookLine() { Value = paragraph });
                    }

                    foreach (var item in section.Items.AsNotNull())
                    {
                        if (item is PType)
                            lines.Add(new BookLine() { Value = ProcessPType(item as PType) });
                        else if (item is ImageValue)
                            lines.Add(new BookLine() { Value = ProcessImageType(item as ImageValue, book), Type = LineType.Image});
                        else if (item is BaseFormatingStyle)
                            lines.Add(new BookLine() { Value = new Paragraph() });
                    }
                }
            }

            return lines;
        }

        private Paragraph ProcessImageType(ImageValue item, Book fictionBook)
        {
            var paragraph = new Paragraph();

            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Books\\" + fictionBook.FolderPath + "\\" + item.Href.Replace("#", "") + ".png");


            var container = new InlineUIContainer();
            container.Child = new Image() { Source = new BitmapImage(new Uri(path)), Stretch = Stretch.Uniform, HorizontalAlignment = HorizontalAlignment.Center};

            paragraph.Inlines.Add(container);

            return paragraph;
        }

        private Paragraph ProcessPType(PType pType)
        {
            var paragraph = new Paragraph();

            paragraph.Inlines.Add(new Run() { Text = "    " + pType.Text ?? string.Empty });

            foreach (var item in pType.Items.AsNotNull())
            {
                paragraph.Inlines.Add(ProcessBaseItems(item));
            }

            return paragraph;
        }

        private Run ProcessBaseItems(BaseFormatingStyle item)
        {
            if (item is StyleType)
                return ProcessStyleType(item as StyleType);
            if (item is LinkType)
                return ProcessLinkType(item as LinkType);

            return new Run() { Text = string.Empty };
        }

        private Run ProcessStyleType(StyleType style)
        {
            return new Run() { Text = $"{style.Text}", FontWeight = FontWeights.Bold };
        }

        private Run ProcessLinkType(LinkType link)
        {
            return new Run() { Text = $"{link.Href}", FontWeight = FontWeights.Normal };
        }
    }
}