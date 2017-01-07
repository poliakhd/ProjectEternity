﻿using System.Xml.Serialization;
using ProjectEternity.FictionBookLibrary.Models.Values;

namespace ProjectEternity.FictionBookLibrary
{
    [XmlType(AnonymousType=true, Namespace="http://www.gribuser.ru/xml/fictionbook/2.0")]
    [XmlRoot(Namespace="http://www.gribuser.ru/xml/fictionbook/2.0", IsNullable=false)]
    public class FictionBook
    {
        /// <summary>
        /// The stylesheet.
        /// </summary>
        [XmlElement("stylesheet")]
        public FictionBookStylesheet[] Stylesheet { get; set; }

        /// <summary>
        /// The description.
        /// </summary>
        [XmlElement("description")]
        public FictionBookDescription Description { get; set; }

        /// <summary>
        /// The body.
        /// </summary>
        [XmlElement("body")]
        public FictionBookBody[] Body { get; set; }

        /// <summary>
        /// The binary.
        /// </summary>
        [XmlElement("binary")]
        public BinaryValue[] Binary { get; set; }
    }
}