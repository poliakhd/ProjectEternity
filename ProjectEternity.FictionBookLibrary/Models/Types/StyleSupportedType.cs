using System.Xml.Serialization;

namespace ProjectEternity.FictionBookLibrary.Models.Types
{
    /// <summary>
    /// The items choice type 3.
    /// </summary>
    [XmlType(Namespace = "http://www.gribuser.ru/xml/fictionbook/2.0", IncludeInSchema = false)]
    public enum StyleSupportedType
    {
        /// <summary>
        /// The a.
        /// </summary>
        [XmlEnum("a")]
        A,

        /// <summary>
        ///     The emphasis.
        /// </summary>
        [XmlEnum("emphasis")]
        Emphasis,

        /// <summary>
        /// The image.
        /// </summary>
        [XmlEnum("image")]
        Image,

        /// <summary>
        /// The strong.
        /// </summary>
        [XmlEnum("strong")]
        Strong,

        /// <summary>
        /// The style.
        /// </summary>
        [XmlEnum("style")]
        Style
    }
}