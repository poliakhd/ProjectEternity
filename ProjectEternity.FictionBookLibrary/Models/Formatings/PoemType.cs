using System.Xml.Schema;
using System.Xml.Serialization;
using ProjectEternity.FictionBookLibrary.Models.Base;
using ProjectEternity.FictionBookLibrary.Models.Values;

namespace ProjectEternity.FictionBookLibrary.Models.Formatings
{
    /// <summary>
    /// The poem type.
    /// </summary>
    [XmlType(Namespace = "http://www.gribuser.ru/xml/fictionbook/2.0")]
    public class PoemType : BaseFormatingStyle
    {
        /// <summary>
        /// The title.
        /// </summary>
        [XmlElement("title")]
        public TitleType Title { get; set; }

        /// <summary>
        ///  The epigraph.
        /// </summary>
        [XmlElement("epigraph")]
        public EpigraphType[] Epigraph { get; set; }

        /// <summary>
        /// The stanza.
        /// </summary>
        [XmlElement("stanza")]
        public PoemStanzaType[] Stanza { get; set; }

        /// <summary>
        /// The textauthor.
        /// </summary>
        [XmlElement("text-author")]
        public TextFieldValue[] TextAuthor { get; set; }

        /// <summary>
        /// The date.
        /// </summary>
        [XmlElement("date")]
        public DateValue Date { get; set; }

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