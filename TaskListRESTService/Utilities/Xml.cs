
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
			DataContractSerializer ser = new DataContractSerializer(typeof(T));
			object o = ser.ReadObject(source);
			
			return (T)o;
		}
	}
}
