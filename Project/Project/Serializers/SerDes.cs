using System.Xml;
using System.Xml.Serialization;

namespace Project.Serializers;

public class SerDes
{
    public static void SerializeToXml<T>(T obj, string filePath)
    {
        var serializer = new XmlSerializer(typeof(T));
        StreamWriter streamWriter = File.CreateText(filePath);
        using (var writer = new XmlTextWriter(streamWriter))
        {
            serializer.Serialize(writer, obj);
        }
    }
}