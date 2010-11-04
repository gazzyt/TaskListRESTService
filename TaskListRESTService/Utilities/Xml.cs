
using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace TaskListRESTService.Utilities
{
	public static class Xml
	{
		
		public static void ObjectToXml(object obj, Stream destination)
		{
			XmlWriterSettings xmlSettings = new XmlWriterSettings
			{
				CloseOutput = false,
				OmitXmlDeclaration = true
			};
			
			XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
			namespaces.Add(String.Empty, String.Empty);
			
			using (XmlWriter xw = XmlWriter.Create(destination, xmlSettings))
			{
				XmlSerializer ser = new XmlSerializer(obj.GetType());
				
				ser.Serialize(xw, obj,namespaces);
			}
		}
		
		public static T XmlToObject<T>(Stream source)
		{
			XmlSerializer ser = new XmlSerializer(typeof(T));
			object o = ser.Deserialize(source);
			
			return (T)o;
		}
	}
}
