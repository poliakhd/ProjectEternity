using System.Xml.Schema;
using System.Xml.Serialization;
using ProjectEternity.FictionBookLibrary.Models.Base;
using ProjectEternity.FictionBookLibrary.Models.Types;
using ProjectEternity.FictionBookLibrary.Models.Values;

namespace ProjectEternity.FictionBookLibrary.Models.Formatings
{
    /// <summary>
    /// The named style type.
    /// </summary>
    [XmlType(Namespace = "http://www.gribuser.ru/xml/fictionbook/2.0")]
    public class NamedStyleType : BaseFormatingStyle
    {
        /// <summary>
        /// The items.
        /// </summary>
        [XmlElement("a", typeof(LinkType))]
        [XmlElement("emphasis", typeof(StyleType))]
        [XmlElement("image", typeof(ImageValue))]
        [XmlElement("strong", typeof(StyleType))]
        [XmlElement("style", typeof(NamedStyleType))]
        [XmlChoiceIdentifier("ItemsElementName")]
        public BaseFormatingStyle[] Items { get; set; }

        /// <summary>
        /// The items element name.
        /// </summary>
        [XmlElement("ItemsElementName")]
        [XmlIgnore]
        public StyleSupportedType[] ItemsElementName { get; set; }

        /// <summary>
        /// The text.
        /// </summary>
        [XmlText]
        public string Text { get; set; }

        /// <summary>
        /// The lang.
        /// </summary>
        [XmlAttribute("lang", Form = XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string Lang { get; set; }

        /// <summary>
        /// The name.
        /// </summary>
        [XmlAttribute("name", DataType = "token")]
        public string Name { get; set; }
    }
}