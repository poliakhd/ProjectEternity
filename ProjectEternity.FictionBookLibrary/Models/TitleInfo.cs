using System.Xml.Serialization;
using ProjectEternity.FictionBookLibrary.Models.Formatings;
using ProjectEternity.FictionBookLibrary.Models.Values;

namespace ProjectEternity.FictionBookLibrary.Models
{
    /// <summary>
    /// The fiction book description titleinfo.
    /// </summary>
    [XmlType(AnonymousType = true, Namespace = "http://www.gribuser.ru/xml/fictionbook/2.0")]
    public class TitleInfo
    {
        /// <summary>
        /// The genre.
        /// </summary>
        [XmlElement("genre")]
        public TitleInfoGenreValue[] Genre { get; set; }

        /// <summary>
        /// The author.
        /// </summary>
        [XmlElement("author")]
        public AuthorType[] Author { get; set; }

        /// <summary>
        /// The booktitle.
        /// </summary>
        [XmlElement("book-title")]
        public TextFieldValue BookTitle { get; set; }

        /// <summary>
        /// The annotation.
        /// </summary>
        [XmlElement("annotation")]
        public AnnotationType Annotation { get; set; }

        /// <summary>
        /// The keywords.
        /// </summary>
        [XmlElement("keywords")]
        public TextFieldValue Keywords { get; set; }

        /// <summary>
        /// The date.
        /// </summary>
        [XmlElement("date")]
        public DateValue Date { get; set; }

        /// <summary>
        /// The coverpage.
        /// </summary>
        [XmlArray("coverpage")]
        [XmlArrayItem("image", IsNullable = false)]
        public ImageValue[] Coverpage { get; set; }

        /// <summary>
        /// The lang.
        /// </summary>
        [XmlElement("lang", DataType = "language")]
        public string Lang { get; set; }

        /// <summary>
        /// The srclang.
        /// </summary>
        [XmlElement("src-lang", DataType = "language")]
        public string Srclang { get; set; }

        /// <summary>
        /// The translator.
        /// </summary>
        [XmlElement("translator")]
        public AuthorType[] Translator { get; set; }

        /// <summary>
        /// The sequence.
        /// </summary>
        [XmlElement("sequence")]
        public SequenceValue[] Sequence { get; set; }
    }
}