using System;
using System.Web.Mvc;
using NUnit.Framework;

using TaskListRESTService.Controllers;
using TaskListRESTService.Mvc;

using JsonResult = TaskListRESTService.Mvc.JsonResult;

namespace TaskListRESTService.Tests
{
	[TestFixture()]
	public class BaseControllerTests
	{
		[TestCase("xml")]
		[TestCase("Xml")]
		[TestCase("XML")]
		public void TestSelectActionResult_Xml (string format)
		{
			//Arrange
			BaseController controller = new BaseController();
			var model = new {a = "1", b = 2};
			
			// Act
			ActionResult result = controller.SelectActionResult("testName", model, format);
			
			// Assert
			Assert.NotNull(result);
			Assert.IsInstanceOf<XmlResult>(result);
			
			XmlResult xmlResult = result as XmlResult;
			Assert.AreSame(model, xmlResult.Data);
		}
		
		[TestCase("json")]
		[TestCase("Json")]
		[TestCase("JSON")]
		public void TestSelectActionResult_Json (string format)
		{
			//Arrange
			BaseController controller = new BaseController();
			var model = new {a = "1", b = 2};
			
			// Act
			ActionResult result = controller.SelectActionResult("testName", model, format);
			
			// Assert
			Assert.NotNull(result);
			Assert.IsInstanceOf<JsonResult>(result);
			
			JsonResult jsonResult = result as JsonResult;
			Assert.AreSame(model, jsonResult.Data);
		}
		
		[TestCase("")]
		[TestCase("html")]
		[TestCase("wibble")]
		public void TestSelectActionResult_Html (string format)
		{
			//Arrange
			BaseController controller = new BaseController();
			var model = new {a = "1", b = 2};
			
			// Act
			string viewName = "testName";
			ActionResult result = controller.SelectActionResult(viewName, model, format);
			
			// Assert
			Assert.NotNull(result);
			Assert.IsInstanceOf<ViewResult>(result);
			
			ViewResult viewResult = result as ViewResult;
			Assert.AreSame(model, viewResult.ViewData.Model);
			Assert.AreSame(viewName, viewResult.ViewName);
		}
	}
}

