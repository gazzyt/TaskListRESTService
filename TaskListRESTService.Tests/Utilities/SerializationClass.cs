using System;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace TaskListRESTService.Tests.Utilities
{
	[DataContract(Namespace="")]
	public class SerializationClass
	{
		[DataMember] public string str1 {get;set;}
		[DataMember] public string str2 {get;set;}
		[JsonIgnore] public string str3 {get;set;}
	}
}

