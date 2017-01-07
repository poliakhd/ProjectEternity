using System.Xml.Schema;
using System.Xml.Serialization;
using ProjectEternity.FictionBookLibrary.Models.Formatings;

namespace ProjectEternity.FictionBookLibrary.Models.Values
{
    /// <summary>
    /// The image type.
    /// </summary>
    [XmlType(Namespace = "http://www.gribuser.ru/xml/fictionbook/2.0")]
    public class ImageValue : TextType
    {
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        [XmlIgnore]
        public override string Text
        {
            get { return Type; }
            set { Type = value; }
        }

        /// <summary>
        /// The type.
        /// </summary>
        [XmlAttribute("type", Form = XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/1999/xlink")]
        public string Type { get; set; } = "simple";

        /// <summary>
        /// The href.
        /// </summary>
        [XmlAttribute("href", Form = XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/1999/xlink")]
        public string Href { get; set; }

        /// <summary>
        /// The alt.
        /// </summary>
        [XmlAttribute("alt")]
        public string Alt { get; set; }
    }
}