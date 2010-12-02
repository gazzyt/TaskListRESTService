using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using NUnit.Framework;

using TaskListRESTService.Utilities;

namespace TaskListRESTService.Tests.Utilities
{
	[DataContract(Namespace="")]
	public class TestEntity
	{
		[DataMember] public string str1 {get;set;}
		[DataMember] public string str2 {get;set;}
	}
	
	[TestFixture()]
	public class JsonTests
	{
		
		[Test()]
		public void TestSerialize()
		{
			TestEntity entity = new TestEntity{str1 = "s1", str2 = "s2"};
			
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
			TestEntity result = Json.JsonToObject<TestEntity>(ms);
			
			Assert.AreEqual("s1", result.str1);
			Assert.AreEqual("s2", result.str2);
		}
	}
}

