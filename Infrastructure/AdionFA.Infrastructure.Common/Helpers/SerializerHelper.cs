using AdionFA.Infrastructure.Common.Logger.Helpers;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
/**https://stackoverflow.com/questions/6115721/how-to-save-restore-serializable-object-to-from-file**/
namespace AdionFA.Infrastructure.Common.Helpers
{
    public static class SerializerHelper
    {
        /// <summary>
        /// Serializes an object and create an XML file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializableObject"></param>
        /// <param name="fileName"></param>
        public static void XMLSerializeObject<T>(T serializableObject, string fileName)
        {
            if (serializableObject == null) { return; }

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, serializableObject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(fileName);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException<Exception>(ex);
                throw;
            }
        }

        /// <summary>
        /// Deserializes an XML file into an object list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T XMLDeSerializeObject<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) { return default(T); }

            T objectOut = default(T);

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(T);

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.Deserialize(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException<Exception>(ex);
                throw;
            }

            return objectOut;
        }
    }
}
