using System;
using NUnit.Framework;

using TaskListRESTService.Utilities;

namespace TaskListRESTService.Tests
{
	[TestFixture()]
	public class UriHelperTests
	{
		[TestCase("http://a.b/", "c/d", Result="http://a.b/c/d")]
		[TestCase("http://a.b/foo", "c/d", Result="http://a.b/foo/c/d")]
		[TestCase("http://a.b/foo/", "c/d", Result="http://a.b/foo/c/d")]
		[TestCase("http://a.b/foo", "/c/d", Result="http://a.b/foo/c/d")]
		[TestCase("http://a.b/foo/", "/c/d", Result="http://a.b/foo/c/d")]
		[TestCase("http://a.b/?format=xml", "c/d", Result="http://a.b/c/d")]
		[TestCase("http://a.b/foo?format=xml", "c/d", Result="http://a.b/foo/c/d")]
		[TestCase("http://a.b/foo/?format=xml", "c/d", Result="http://a.b/foo/c/d")]
		[TestCase("http://a.b/foo?format=xml", "/c/d", Result="http://a.b/foo/c/d")]
		[TestCase("http://a.b/foo/?format=xml", "/c/d", Result="http://a.b/foo/c/d")]
		public string TestCase (string root, string extra)
		{
			return new Uri(root).Append(new Uri(extra, UriKind.Relative)).ToString();
		}
	}
}

