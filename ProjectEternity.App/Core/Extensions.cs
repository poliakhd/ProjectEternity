using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using Caliburn.Micro;

namespace ProjectEternity.App.Core
{
    public static class Extensions
    {
        /// <summary>
        /// Converting source collection to BindableCollection
        /// </summary>
        /// <typeparam name="T">Collection item type</typeparam>
        /// <param name="sourceCollection">Collection to convert</param>
        /// <returns>Converted collection</returns>
        public static BindableCollection<T> ToBindableCollection<T>(this IEnumerable<T> sourceCollection)
        {
            return new BindableCollection<T>(sourceCollection);
        }

        /// <summary>
        /// Serialize specific model
        /// </summary>
        /// <typeparam name="T">Type of model</typeparam>
        /// <param name="model">Model instance</param>
        /// <returns>Serialized model</returns>
        public static byte[] Serialize<T>(this T model)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var ms = new MemoryStream())
            {
                var setting = new XmlWriterSettings()
                {
                    CheckCharacters = true
                };

                using (var writer = XmlWriter.Create(ms, setting))
                {
                    xmlSerializer.Serialize(ms, model);
                    return ms.ToArray();
                }
            }
        }

        /// <summary>
        /// Serialize specific model
        /// </summary>
        /// <typeparam name="T">Type of model</typeparam>
        /// <param name="stream">Model represent as <see cref="Stream"/></param>
        /// <returns>Deserialized model instance</returns>
        public static T Deserialize<T>(this Stream stream)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            var settings = new XmlReaderSettings
            {
                CheckCharacters = true,
                IgnoreComments = true,
                IgnoreWhitespace = true
            };

            using (var reader = XmlReader.Create(stream, settings))
            {
                return (T)xmlSerializer.Deserialize(reader);
            }
        }


        public static IEnumerable<T> AsNotNull<T>(this IEnumerable<T> original)
        {
            return original ?? new T[0];
        }

        public static void UnloadFromMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}