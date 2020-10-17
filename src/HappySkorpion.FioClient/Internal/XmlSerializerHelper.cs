using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace HappySkorpion.FioClient.Internal
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public static class XmlSerializerHelper
    {
        public static string Serialize(object data)
        {
            _ = data ?? throw new ArgumentNullException(nameof(data));

            var builder = new StringBuilder();
            using var writter = XmlWriter.Create(
                builder,
                new XmlWriterSettings
                {
                    NewLineHandling = NewLineHandling.Entitize,
                    NewLineChars = Environment.NewLine,
                    IndentChars = "  ",
                    Indent = true,
                });
            var serializer = new XmlSerializer(data.GetType());

            serializer.Serialize(writter, data);

            return builder.ToString();
        }

        public static T Deserialize<T>(Stream data)
        {
            _ = data ?? throw new ArgumentNullException(nameof(data));
            
            var serializer = new XmlSerializer(typeof(T));
            using var reader = XmlReader.Create(data);

            return (T)serializer.Deserialize(reader);
        }
    }
}
