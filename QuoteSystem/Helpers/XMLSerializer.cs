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
        /// Serialize an instance of a Serializable object and write it directly to an XML file
        /// </summary>
        /// <typeparam name="T">The type ofthe object being serialized</typeparam>
        /// <param name="instance">The instance of the object to be serialized</param>
        /// <param name="filepath">The file name where the output XML should be written</param>
        /// <param name="useBaseDirectory">Control whether the filepath should use the base directory, default false</param>
        public static void SerializeXMLToFile<T>(T instance, string filepath, bool useBaseDirectory = false)
        {
            string baseDir = (useBaseDirectory) ? $"{AppDomain.CurrentDomain.BaseDirectory}\\" : "";

            var serializedInstance = SerializeToXml<T>(instance);

            File.WriteAllBytes($"{baseDir}{filepath}", serializedInstance);
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

        /// <summary>
        /// Deserialize an XML file into an instance of a Serializable object
        /// </summary>
        /// <typeparam name="T">the Type of the the object being deserialized</typeparam>
        /// <param name="filepath">The XML file to be read and deserialized</param>
        /// <param name="useBaseDirectory">Control whether the filepath should use the base directory, default false</param>
        /// <returns>object of type T</returns>
        public static T DeserializeXMLFromFile<T>(string filepath, bool useBaseDirectory = false)
        {
            string baseDir = (useBaseDirectory) ? $"{AppDomain.CurrentDomain.BaseDirectory}\\" : "";

            var bytes = File.ReadAllBytes($"{baseDir}{filepath}");
            return DeserializeFromXML<T>(bytes);
        }
    }
}
