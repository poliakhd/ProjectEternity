using System.Xml.Serialization;
using ProjectEternity.FictionBookLibrary.Models.Formatings;
using ProjectEternity.FictionBookLibrary.Models.Values;

namespace ProjectEternity.FictionBookLibrary.Models
{
    /// <summary>
    /// The fiction book description documentinfo.
    /// </summary>
    [XmlType(AnonymousType = true, Namespace = "http://www.gribuser.ru/xml/fictionbook/2.0")]
    public class DocumentInfo
    {
        /// <summary>
        /// The author.
        /// </summary>
        [XmlElement("author")]
        public AuthorType[] Author { get; set; }

        /// <summary>
        /// The programused.
        /// </summary>
        [XmlElement("program-used")]
        public TextFieldValue ProgramUsed { get; set; }

        /// <summary>
        /// The date.
        /// </summary>
        [XmlElement("date")]
        public DateValue Date { get; set; }

        /// <summary>
        /// The srcurl.
        /// </summary>
        [XmlElement("src-url")]
        public string[] SrcUrl { get; set; }

        /// <summary>
        /// The srcocr.
        /// </summary>
        [XmlElement("src-ocr")]
        public TextFieldValue SrcOcr { get; set; }

        /// <summary>
        /// The id.
        /// </summary>
        [XmlElement(DataType = "token")]
        public string Id { get; set; }

        /// <summary>
        /// The version.
        /// </summary>
        [XmlElement("version")]
        public float Version { get; set; }

        /// <summary>
        /// The history.
        /// </summary>
        [XmlElement("history")]
        public AnnotationType History { get; set; }
    }
}