using System.ComponentModel;
using System.Xml.Serialization;
using ProjectEternity.FictionBookLibrary.Models.Types;

namespace ProjectEternity.FictionBookLibrary.Models.Formatings
{
    /// <summary>
    /// The table type tr.
    /// </summary>
    [XmlType(AnonymousType = true, Namespace = "http://www.gribuser.ru/xml/fictionbook/2.0")]
    public class TableTrType
    {
        /// <summary>
        /// The td.
        /// </summary>
        [XmlElement("td")]
        public PType[] Td { get; set; }

        /// <summary>
        /// The align.
        /// </summary>
        [XmlAttribute("align")]
        [DefaultValue(AlignType.Left)]
        public AlignType Align { get; set; } = AlignType.Left;
    }
}