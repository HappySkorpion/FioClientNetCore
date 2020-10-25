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
        public static string Serialize(
            object data)
        {
            _ = data ?? throw new ArgumentNullException(nameof(data));

            using var builder = new Utf8StringWriter();
            using var writter = XmlWriter.Create(
                builder,
                new XmlWriterSettings
                {
                    NewLineHandling = NewLineHandling.Entitize,
                    NewLineChars = Environment.NewLine,
                    IndentChars = "  ",
                    Indent = true,
                });

            new XmlSerializer(data.GetType())
                .Serialize(writter, data);

            return builder.ToString();
        }

        public static T Deserialize<T>(Stream data)
        {
            _ = data ?? throw new ArgumentNullException(nameof(data));
            
            var serializer = new XmlSerializer(typeof(T));
            using var reader = XmlReader.Create(data);

            return (T)serializer.Deserialize(reader);
        }

        private class Utf8StringWriter
            : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }

    }
}
