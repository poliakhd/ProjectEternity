using System.Xml.Serialization;
using ProjectEternity.FictionBookLibrary.Models.Values;

namespace ProjectEternity.FictionBookLibrary.Models
{
    /// <summary>
    /// The fiction book description publishinfo.
    /// </summary>
    [XmlType(AnonymousType = true, Namespace = "http://www.gribuser.ru/xml/fictionbook/2.0")]
    public class PublishInfo
    {
        /// <summary>
        /// The bookname.
        /// </summary>
        [XmlElement("book-name")]
        public TextFieldValue BookName { get; set; }

        /// <summary>
        /// The publisher.
        /// </summary>
        [XmlElement("publisher")]
        public TextFieldValue Publisher { get; set; }

        /// <summary>
        /// The city.
        /// </summary>
        [XmlElement("city")]
        public TextFieldValue City { get; set; }

        /// <summary>
        /// The year.
        /// </summary>
        [XmlElement(DataType = "gYear")]
        public string Year { get; set; }

        /// <summary>
        /// The isbn.
        /// </summary>
        [XmlElement("isbn")]
        public TextFieldValue Isbn { get; set; }

        /// <summary>
        /// The sequence.
        /// </summary>
        [XmlElement("sequence")]
        public SequenceValue[] Sequence { get; set; }
    }
}