using System.Xml.Schema;
using System.Xml.Serialization;
using ProjectEternity.FictionBookLibrary.Models.Base;
using ProjectEternity.FictionBookLibrary.Models.Values;

namespace ProjectEternity.FictionBookLibrary.Models.Formatings
{
    /// <summary>
    /// The cite type.
    /// </summary>
    [XmlType(Namespace = "http://www.gribuser.ru/xml/fictionbook/2.0")]
    public class CiteType : BaseFormatingStyle
    {
        /// <summary>
        /// The items.
        /// </summary>
        [XmlElement("empty-line", typeof(BaseFormatingStyle))]
        [XmlElement("p", typeof(PType))]
        [XmlElement("poem", typeof(PoemType))]
        public BaseFormatingStyle[] Items { get; set; }

        /// <summary>
        /// The textauthor.
        /// </summary>
        [XmlElement("text-author")]
        public TextFieldValue[] TextAuthor { get; set; }

        /// <summary>
        /// The id.
        /// </summary>
        [XmlAttribute("id", DataType = "ID")]
        public string Id { get; set; }

        /// <summary>
        /// The lang.
        /// </summary>
        [XmlAttribute("lang", Form = XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string Lang { get; set; }
    }
}