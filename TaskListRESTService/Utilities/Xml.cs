
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace TaskListRESTService.Utilities
{
	public static class Xml
	{
		public static void ObjectToXml(object obj, Stream destination)
		{
			DataContractSerializer ser = new DataContractSerializer(obj.GetType());
			XmlWriterSettings xmlSettings = new XmlWriterSettings
			{
				CloseOutput = false,
				OmitXmlDeclaration = true,
				Encoding = new UTF8Encoding(false)
			};

			using (XmlWriter xw = XmlWriter.Create(destination, xmlSettings))
			{
				ser.WriteObject(xw, obj);
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
