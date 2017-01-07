using System.Xml.Serialization;
using ProjectEternity.FictionBookLibrary.Models.Formatings;

namespace ProjectEternity.FictionBookLibrary
{
    /// <summary>
    /// The fiction book stylesheet.
    /// </summary>
    [XmlType(AnonymousType = true, Namespace = "http://www.gribuser.ru/xml/fictionbook/2.0")]
    public class FictionBookStylesheet : TextType
    {
        /// <summary>
        /// The type.
        /// </summary>
        [XmlAttribute("type")]
        public string Type { get; set; }

        /// <summary>
        /// The value.
        /// </summary>
        [XmlText]
        public override string Text { get; set; }
    }
}