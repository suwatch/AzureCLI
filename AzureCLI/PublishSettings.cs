using System;
using System.IO;
using System.Xml.Serialization;

namespace AzureCLI
{
    [XmlRoot(ElementName = "PublishData")]
    public class PublishSettings
    {
        public PublishProfile PublishProfile { get; set; }

        public static PublishSettings LoadFrom(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PublishSettings));
            using (FileStream fs = File.OpenRead(fileName))
            {
                return (PublishSettings)serializer.Deserialize(fs);
            }
        }
    }
}
