using System.Xml.Serialization;
using ProjectEternity.FictionBookLibrary.Models.Base;
using ProjectEternity.FictionBookLibrary.Models.Values;

namespace ProjectEternity.FictionBookLibrary.Models.Formatings
{
    /// <summary>
    /// The epigraph type.
    /// </summary>
    [XmlType(Namespace = "http://www.gribuser.ru/xml/fictionbook/2.0")]
    public class EpigraphType
    {
        /// <summary>
        /// The items.
        /// </summary>
        [XmlElement("cite", typeof(CiteType))]
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
    }
}