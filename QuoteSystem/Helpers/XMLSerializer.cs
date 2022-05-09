using System.Xml.Serialization;

namespace QuoteSystem.Helpers
{
    /// <summary>
    /// Library containing methods for Serializing objects into XML
    /// </summary>
    public class XMLSerializer
    {
        /// <summary>
        /// Serialize an instance of a Serializable object to a byte array
        /// </summary>
        /// <typeparam name="T">The type ofthe object being serialized</typeparam>
        /// <param name="instance">The instance of the object to be serialized</param>
        /// <returns>byte array of the converted object</returns>
        public static byte[] SerializeToXml<T>(T instance)
        {
            var serializer = new XmlSerializer(typeof(T));
            using var stream = new MemoryStream();

            serializer.Serialize(stream, instance);
            return stream.ToArray();
        }

        /// <summary>
        /// Deserialize a byte array into an instance of a Serializable object
        /// </summary>
        /// <typeparam name="T">the Type of the the object being deserialized</typeparam>
        /// <param name="instance">the byte array to deserialize</param>
        /// <returns>object of type T</returns>
        public static T DeserializeFromXML<T>(byte[] instance)
        {
            var serializer = new XmlSerializer(typeof(T));
            using var stream = new MemoryStream(instance);

            return (T)serializer.Deserialize(stream);
        }
    }
}
