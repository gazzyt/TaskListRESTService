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
	public class TaskListControllerTests
	{

		#region List Tests
		
		private static TaskListController CreateControllerForGet (TaskList taskList)
		{
			var stubDao = MockRepository.GenerateStub<ITaskListDao>();
			
			stubDao.Stub(x => x.GetTaskList(Arg<Guid>.Is.Anything)).Return(taskList);
			
			return new TaskListController(stubDao);
		}
		
		private void AssertModel(object model, TaskList taskList)
		{
			Assert.IsInstanceOf<TaskListViewModel>(model);
			TaskListViewModel viewModel = model as TaskListViewModel;
			
			Assert.AreSame(taskList, viewModel.TaskList);
		}
		
		[Test]
		public void TestGetTaskList_Html_Success ()
		{
			//Arrange
			TaskList taskList = new TaskList() { Name="TaskList"};
			TaskListController controller = CreateControllerForGet(taskList);
			
			// Act
			ActionResult result = controller.List(new Guid(), "");
			
			// Assert
			Assert.NotNull(result);
			Assert.IsInstanceOf<ViewResult>(result);
			
			ViewResult viewResult = result as ViewResult;
			AssertModel(viewResult.ViewData.Model, taskList);
		}
		
		[TestCase("xml")]
		public void TestList_Xml_Normal (string format)
		{
			//Arrange
			TaskList taskList = new TaskList() { Name="TaskList"};
			TaskListController controller = CreateControllerForGet(taskList);
			
			// Act
			ActionResult result = controller.List(new Guid(), format);
			
			// Assert
			Assert.NotNull(result);
			Assert.IsInstanceOf<XmlResult>(result);
			
			XmlResult xmlResult = result as XmlResult;
			AssertModel(xmlResult.Data, taskList);
		}
		
		[TestCase("json")]
		public void TestList_Json_Normal (string format)
		{
			//Arrange
			TaskList taskList = new TaskList() { Name="TaskList"};
			TaskListController controller = CreateControllerForGet(taskList);
			
			// Act
			ActionResult result = controller.List(new Guid(), format);
			
			// Assert
			Assert.NotNull(result);
			Assert.IsInstanceOf<JsonResult>(result);
			
			JsonResult jsonResult = result as JsonResult;
			AssertModel(jsonResult.Data, taskList);
		}
		
		[Test]
		public void TestList_NotFound()
		{
			//Arrange
			var stubDao = MockRepository.GenerateStub<ITaskListDao>();
			stubDao.Stub(x => x.GetTaskList(Arg<Guid>.Is.Anything)).Return(null);
			TaskListController controller = new TaskListController(stubDao);
			
			//Act
			ActionResult result = controller.List(new Guid(), "");
			
			//Assert
			Assert.IsInstanceOf<EmptyResultWithStatus>(result);
			EmptyResultWithStatus emptyResult = result as EmptyResultWithStatus;
			Assert.AreEqual(404, emptyResult.StatusCode);
		}
		
		#endregion
		
	}
}

