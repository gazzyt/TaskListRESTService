using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using NUnit.Framework;
using Rhino.Mocks;

using TaskListDao;
using TaskListDao.Model;
using TaskListRESTService.Controllers;
using TaskListRESTService.Models;

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

		private void AssertModel(object model)
		{
			Assert.IsInstanceOf<TaskListsViewModel>(model);
			TaskListsViewModel viewModels = model as TaskListsViewModel;
			
			Assert.True(taskLists.All(x => viewModels.Any(vm => vm.Name == x.Name && vm.Id == x.Id)));
		}

		#region List Tests
		
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
			AssertModel(viewResult.ViewData.Model);
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
			AssertModel(xmlResult.Data);
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
			AssertModel(jsonResult.Data);
		}
		#endregion
		
		#region Create Tests
		
		[Test]
		public void TestCreate_Normal ()
		{
			//Arrange
			var stubDao = MockRepository.GenerateStub<ITaskListDao>();
			
			stubDao.Stub(x => x.AddTaskList(Arg<TaskList>.Is.Anything));
			TaskListsController controller = new TaskListsController(stubDao);
			TaskList tl = new TaskList {Name = "tl1"};
			
			// Act
			ActionResult result = controller.Create(tl);
			
			// Assert
			Assert.NotNull(result);
			Assert.IsInstanceOf<EmptyResultWithStatus>(result);
			
			EmptyResultWithStatus viewResult = result as EmptyResultWithStatus;
			Assert.AreEqual(201, viewResult.StatusCode);
			Assert.IsFalse(viewResult.Location.ToString().Contains(new Guid().ToString("D")), "Wrong Location url. Actual was {0}", viewResult.Location.ToString());
		}
		
		#endregion
		
		#region Delete Tests
		
		[Test]
		public void TestDelete_Normal ()
		{
			//Arrange
			var stubDao = MockRepository.GenerateStub<ITaskListDao>();
			
			TaskListsController controller = new TaskListsController(stubDao);
			
			// Act
			ActionResult result = controller.DeleteAll();
			
			// Assert
			stubDao.AssertWasCalled(x => x.DeleteAllTaskLists());
			Assert.NotNull(result);
			Assert.IsInstanceOf<EmptyResultWithStatus>(result);
			
			EmptyResultWithStatus viewResult = result as EmptyResultWithStatus;
			Assert.AreEqual(204, viewResult.StatusCode);
		}
		
		#endregion

	}
}

