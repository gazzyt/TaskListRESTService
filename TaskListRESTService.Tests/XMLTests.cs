
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

using NUnit.Framework;

using TaskListRESTService.Utilities;

namespace TaskListRESTService.Tests
{
	[DataContract(Namespace="")]
	public class TestEntity
	{
		[DataMember] public string str1 {get;set;}
		[DataMember] public string str2 {get;set;}
	}
	
	[TestFixture()]
	public class XMLTests
	{
		
		[Test()]
		public void TestSerialize()
		{
			TestEntity entity = new TestEntity{str1 = "s1", str2 = "s2"};
			
			using (MemoryStream ms = new MemoryStream())
			{
				Xml.ObjectToXml(entity, ms);
				ms.Position = 0;
				StreamReader sr = new StreamReader(ms);
				string result = sr.ReadToEnd();
				Assert.AreEqual("<TestEntity xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\"><str1>s1</str1><str2>s2</str2></TestEntity>", result);
			}
		}
		
		[Test]
		public void TestDeserialize()
		{
			MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes("<TestEntity><str1>s1</str1><str2>s2</str2></TestEntity>"));
			TestEntity result = Xml.XmlToObject<TestEntity>(ms);
			
			Assert.AreEqual("s1", result.str1);
			Assert.AreEqual("s2", result.str2);
		}
	}
}
