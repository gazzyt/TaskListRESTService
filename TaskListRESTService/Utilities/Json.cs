
using System;
using System.IO;
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
			JsonSerializer ser = new JsonSerializer();
			StreamReader sr = new StreamReader(source);
			JsonReader jr = new JsonTextReader(sr);
			T o = ser.Deserialize<T>(jr);
			
			return o;
		}
	}
}
