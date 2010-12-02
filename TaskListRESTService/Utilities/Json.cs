
using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace TaskListRESTService.Utilities
{
	public static class Json
	{
		public static void ObjectToJson(object obj, Stream destination)
		{
			DataContractJsonSerializer ser = new DataContractJsonSerializer(obj.GetType());

			ser.WriteObject(destination, obj);
		}
		
		public static T JsonToObject<T>(Stream source)
		{
			DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
			object o = ser.ReadObject(source);
			
			return (T)o;
		}
	}
}
