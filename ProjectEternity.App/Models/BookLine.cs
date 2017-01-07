using Windows.UI.Text;
using Windows.UI.Xaml.Documents;

namespace ProjectEternity.App.Models
{
    public enum LineType
    {
        PlainText,
        Image
    }

    public class BookLine
    {
        public Paragraph Value { get; set; }
        public LineType Type { get; set; }
    }
}