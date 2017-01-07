using System.Xml.Serialization;
using ProjectEternity.FictionBookLibrary.Models.Base;

namespace ProjectEternity.FictionBookLibrary.Models.Formatings
{
    /// <summary>
    /// The table type.
    /// </summary>
    [XmlType(Namespace = "http://www.gribuser.ru/xml/fictionbook/2.0")]
    public class TableType : BaseFormatingStyle
    {
        /// <summary>
        /// The tr.
        /// </summary>
        [XmlElement("tr")]
        public TableTrType[] Tr { get; set; }
    }
}