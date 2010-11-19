using System;
using System.Web.Mvc;

using NUnit.Framework;
using Rhino.Mocks;

using TaskListDao;
using TaskListDao.Model;
using TaskListRESTService.Controllers;

namespace TaskListRESTService.Tests
{
	[TestFixture()]
	public class TaskListsControllerTests
	{
		private TaskList[] taskLists = new TaskList[] { 
			new TaskList{Id = new Guid("00000000-0000-0000-0000-000000000001"), Name = "T1"}, 
			new TaskList{Id = new Guid("00000000-0000-0000-0000-000000000002"), Name = "T2"} 
		};

		private static TaskListsController CreateControllerForList (TaskList[] taskLists)
		{
			var stubDao = MockRepository.GenerateStub<ITaskListDao>();
			
			stubDao.Stub(x => x.GetTaskLists()).Return(taskLists);
			
			return new TaskListsController(stubDao);
		}

		
		[Test()]
		public void TestList_Html_Normal ()
		{
			//Arrange
			TaskListsController controller = CreateControllerForList(taskLists);
			
			// Act
			ActionResult result = controller.List("");
			
			// Assert
			Assert.NotNull(result);
			Assert.IsInstanceOf<ViewResult>(result);
			
			ViewResult viewResult = result as ViewResult;
			Assert.AreSame(taskLists, viewResult.ViewData.Model);
		}
		
		[TestCase("xml")]
		public void TestList_Xml_Normal (string format)
		{
			//Arrange
			TaskListsController controller = CreateControllerForList(taskLists);
			
			// Act
			ActionResult result = controller.List(format);
			
			// Assert
			Assert.NotNull(result);
			Assert.IsInstanceOf<XmlResult>(result);
			
			XmlResult xmlResult = result as XmlResult;
			Assert.AreSame(taskLists, xmlResult.Data);
		}
		
		[TestCase("json")]
		public void TestList_Json_Normal (string format)
		{
			//Arrange
			TaskListsController controller = CreateControllerForList(taskLists);
			
			// Act
			ActionResult result = controller.List(format);
			
			// Assert
			Assert.NotNull(result);
			Assert.IsInstanceOf<JsonResult>(result);
			
			JsonResult jsonResult = result as JsonResult;
			Assert.AreSame(taskLists, jsonResult.Data);
		}
	}
}

