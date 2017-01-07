using System.Xml.Serialization;
using ProjectEternity.FictionBookLibrary.Models.Base;

namespace ProjectEternity.FictionBookLibrary.Models.Formatings
{
    /// <summary>
    /// The base data.
    /// </summary>
    public class TextType 
        : BaseFormatingStyle
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [XmlText]   
        public virtual string Text { get; set; }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString()
        {
            return Text;
        }
    }
}