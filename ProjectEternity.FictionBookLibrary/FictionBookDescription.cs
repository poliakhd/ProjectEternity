using System.Xml.Serialization;
using ProjectEternity.FictionBookLibrary.Models;

namespace ProjectEternity.FictionBookLibrary
{
    /// <summary>
    /// The fiction book description.
    /// </summary>
    [XmlType(AnonymousType = true, Namespace = "http://www.gribuser.ru/xml/fictionbook/2.0")]
    public class FictionBookDescription
    {
        /// <summary>
        /// The titleinfo.
        /// </summary>
        [XmlElement("title-info")]
        public TitleInfo TitleInfo { get; set; }

        /// <summary>
        /// The documentinfo.
        /// </summary>
        [XmlElement("document-info")]
        public DocumentInfo DocumentInfo { get; set; }

        /// <summary>
        /// The publishinfo.
        /// </summary>
        [XmlElement("publish-info")]
        public PublishInfo PublishInfo { get; set; }

        /// <summary>
        /// The custominfo.
        /// </summary>
        [XmlElement("custom-info")]
        public CustomInfo[] CustomInfo { get; set; }
    }
}