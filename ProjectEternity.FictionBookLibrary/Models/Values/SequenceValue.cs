using System.Xml.Schema;
using System.Xml.Serialization;

namespace ProjectEternity.FictionBookLibrary.Models.Values
{
    /// <summary>
    /// The sequence type.
    /// </summary>
    [XmlType(Namespace = "http://www.gribuser.ru/xml/fictionbook/2.0")]
    public class SequenceValue
    {
        /// <summary>
        /// The sequence.
        /// </summary>
        [XmlElement("sequence")]
        public SequenceValue[] Sequence { get; set; }

        /// <summary>
        /// The name.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// The number.
        /// </summary>
        [XmlAttribute("number", DataType = "integer")]
        public string Number { get; set; }

        /// <summary>
        /// The lang.
        /// </summary>
        [XmlAttribute("lang", Form = XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string Lang { get; set; }
    }
}