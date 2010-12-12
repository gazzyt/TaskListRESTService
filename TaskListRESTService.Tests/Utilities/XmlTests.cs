
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

using NUnit.Framework;

using TaskListRESTService.Utilities;

namespace TaskListRESTService.Tests.Utilities
{
	[TestFixture()]
	public class XMLTests
	{
		
		[Test()]
		public void TestSerialize()
		{
			SerializationClass entity = new SerializationClass{str1 = "s1", str2 = "s2"};
			
			using (MemoryStream ms = new MemoryStream())
			{
				Xml.ObjectToXml(entity, ms);
				ms.Position = 0;
				StreamReader sr = new StreamReader(ms);
				string result = sr.ReadToEnd();
				Assert.AreEqual("<SerializationClass xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\"><str1>s1</str1><str2>s2</str2></SerializationClass>", result);
			}
		}
		
		[Test]
		public void TestDeserialize()
		{
			MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes("<SerializationClass><str1>s1</str1><str2>s2</str2></SerializationClass>"));
			SerializationClass result = Xml.XmlToObject<SerializationClass>(ms);
			
			Assert.AreEqual("s1", result.str1);
			Assert.AreEqual("s2", result.str2);
		}
	}
}
