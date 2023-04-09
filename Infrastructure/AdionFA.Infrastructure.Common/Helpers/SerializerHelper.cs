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
        public static void XMLSerializeObject<T>(T serializableObject, string fileName)
        {
            if (serializableObject == null) { return; }

            try
            {
                var xmlDocument = new XmlDocument();
                var serializer = new XmlSerializer(serializableObject.GetType());
                using var stream = new MemoryStream();

                serializer.Serialize(stream, serializableObject);
                stream.Position = 0;
                xmlDocument.Load(stream);
                xmlDocument.Save(fileName);
            }
            catch (Exception ex)
            {
                LogHelper.LogException<Exception>(ex);
                throw;
            }
        }

        public static T XMLDeSerializeObject<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return default;

            T objectOut = default;

            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                var xmlString = xmlDocument.OuterXml;

                using var read = new StringReader(xmlString);
                var outType = typeof(T);

                var serializer = new XmlSerializer(outType);
                using var reader = new XmlTextReader(read);
                objectOut = (T)serializer.Deserialize(reader);
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
