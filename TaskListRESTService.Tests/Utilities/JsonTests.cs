using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

using Newtonsoft.Json;
using NUnit.Framework;

using TaskListRESTService.Utilities;

namespace TaskListRESTService.Tests.Utilities
{
	[TestFixture()]
	public class JsonTests
	{
		
		[Test()]
		public void TestSerialize()
		{
			SerializationClass entity = new SerializationClass{str1 = "s1", str2 = "s2", str3 = "s3"};
			
			using (MemoryStream ms = new MemoryStream())
			{
				Json.ObjectToJson(entity, ms);
				ms.Position = 0;
				StreamReader sr = new StreamReader(ms);
				string result = sr.ReadToEnd();
				Assert.AreEqual("{\"str1\":\"s1\",\"str2\":\"s2\"}", result);
			}
		}
		
		[Test]
		public void TestDeserialize()
		{
			MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes("{\"str1\":\"s1\",\"str2\":\"s2\"}"));
			SerializationClass result = Json.JsonToObject<SerializationClass>(ms);
			
			Assert.AreEqual("s1", result.str1);
			Assert.AreEqual("s2", result.str2);
		}
	}
}

