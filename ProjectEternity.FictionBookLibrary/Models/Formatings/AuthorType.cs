using System.Diagnostics;
using System.Xml.Serialization;
using ProjectEternity.FictionBookLibrary.Models.Types;
using ProjectEternity.FictionBookLibrary.Models.Values;

namespace ProjectEternity.FictionBookLibrary.Models.Formatings
{
    /// <summary>
    /// The author type.
    /// </summary>
    [DebuggerStepThrough]
    [XmlType(Namespace = "http://www.gribuser.ru/xml/fictionbook/2.0")]
    public class AuthorType
    {
        /// <summary>
        /// The items.
        /// </summary>
        [XmlElement("email", typeof(TextType))]
        [XmlElement("first-name", typeof(TextFieldValue))]
        [XmlElement("home-page", typeof(TextType))]
        [XmlElement("last-name", typeof(TextFieldValue))]
        [XmlElement("middle-name", typeof(TextFieldValue))]
        [XmlElement("nickname", typeof(TextFieldValue))]
        [XmlChoiceIdentifier("ItemsElementName")]
        public TextType[] Items { get; set; }

        /// <summary>
        /// The items element name.
        /// </summary>
        [XmlElement("ItemsElementName")]
        [XmlIgnore]
        public AuthorSupportedType[] ItemsElementName { get; set; }
    }
}