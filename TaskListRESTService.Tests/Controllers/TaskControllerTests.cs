using System;
using System.Web.Mvc;

using NUnit.Framework;
using Rhino.Mocks;

using TaskListDao;
using TaskListDao.Model;
using TaskListRESTService.Controllers;
using TaskListRESTService.Models;
using TaskListRESTService.Mvc;

using JsonResult = TaskListRESTService.Mvc.JsonResult;

namespace TaskListRESTService.Tests
{
	[TestFixture()]
	public class TaskControllerTests
	{
		#region GetTaskTests

		private Guid missingGuid = new Guid("00000000-0000-0000-0000-000000000001");
		private Guid foundGuid = new Guid("00000000-0000-0000-0000-000000000002");

		private TaskController SetUpTaskControllerForGet()
		{
			Task foundTask = new Task()
			{
				Id = foundGuid,
				Name = "FoundTaskList"
			};
			
			var stubDao = MockRepository.GenerateStub<ITaskListDao>();
			stubDao.Stub(x => x.GetTask(Arg<Guid>.Is.Equal(missingGuid))).Return(null);
			stubDao.Stub(x => x.GetTask(Arg<Guid>.Is.Equal(foundGuid))).Return(foundTask);
			
			return new TaskController(stubDao);
		}
		
		private void AssertModelForGet(object model)
		{
			Assert.IsInstanceOf<TaskViewModel>(model);
			TaskViewModel tvm = model as TaskViewModel;
			Assert.AreEqual(foundGuid, tvm.Task.Id);
			Assert.AreEqual("FoundTaskList", tvm.Name);
		}
		
		[Test]
		public void TestGetTask_Html_Success ()
		{
			//Arrange
			TaskController controller = SetUpTaskControllerForGet();
			
			//Act
			ActionResult result = controller.GetTask(foundGuid, "html");
			
			//Assert
			Assert.IsInstanceOf<ViewResult>(result);
			ViewResult vr = result as ViewResult;
			AssertModelForGet(vr.ViewData.Model);
		}
		
		[Test]
		public void TestGetTask_Xml_Success ()
		{
			//Arrange
			TaskController controller = SetUpTaskControllerForGet();
			
			//Act
			ActionResult result = controller.GetTask(foundGuid, "xml");
			
			//Assert
			Assert.IsInstanceOf<XmlResult>(result);
			XmlResult xr = result as XmlResult;
			AssertModelForGet(xr.Data);
		}
		
		[Test]
		public void TestGetTask_Json_Success ()
		{
			//Arrange
			TaskController controller = SetUpTaskControllerForGet();
			
			//Act
			ActionResult result = controller.GetTask(foundGuid, "json");
			
			//Assert
			Assert.IsInstanceOf<JsonResult>(result);
			JsonResult jr = result as JsonResult;
			AssertModelForGet(jr.Data);
		}	
		
		[Test]
		public void TestGetTask_NotFound()
		{
			//Arrange
			TaskController controller = SetUpTaskControllerForGet();
			
			//Act
			ActionResult result = controller.GetTask(missingGuid, "json");
			
			//Assert
			Assert.IsInstanceOf<EmptyResultWithStatus>(result);
			EmptyResultWithStatus er = result as EmptyResultWithStatus;
			Assert.AreEqual(404, er.StatusCode);
		}
		
		#endregion
	}
}

