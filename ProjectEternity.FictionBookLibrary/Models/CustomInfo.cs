using System.Xml.Serialization;
using ProjectEternity.FictionBookLibrary.Models.Values;

namespace ProjectEternity.FictionBookLibrary.Models
{
    /// <summary>
    /// The fiction book description custominfo.
    /// </summary>
    [XmlType(AnonymousType = true, Namespace = "http://www.gribuser.ru/xml/fictionbook/2.0")]
    public class CustomInfo : TextFieldValue
    {
        /// <summary>
        /// The infotype.
        /// </summary>
        [XmlAttribute("info-type")]
        public string InfoType { get; set; }
    }
}