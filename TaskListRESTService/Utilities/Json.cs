
using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json;

namespace TaskListRESTService.Utilities
{
	public static class Json
	{
		public static void ObjectToJson(object obj, Stream destination)
		{
			JsonSerializer ser = new JsonSerializer();
			
			StreamWriter sw = new StreamWriter(destination);
			JsonWriter jw = new JsonTextWriter(sw);
			ser.Serialize(jw, obj);
			jw.Flush();
		}
		
		public static T JsonToObject<T>(Stream source)
		{
			DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
			object o = ser.ReadObject(source);
			
			return (T)o;
		}
	}
}
