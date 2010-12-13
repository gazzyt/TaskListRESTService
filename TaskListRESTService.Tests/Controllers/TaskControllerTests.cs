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
		
		#region Create Tests
		
		[Test]
		public void TestCreate_Success()
		{
			//Arrange
			Guid taskListGuid = new Guid("00000000-0000-0000-1111-000000000001");
			Task newTask = new Task()
			{
				Complete = false,
				Description = "Desc",
				Due = new DateTime(2010, 1, 1),
				Name = "Name"
			};
			TaskViewModel newTaskViewModel = new TaskViewModel(newTask);
			var stubDao = MockRepository.GenerateStub<ITaskListDao>();
			TaskController controller = new TaskController(stubDao);
			
			//Act
			ActionResult ar = controller.Create(taskListGuid, newTaskViewModel);
			
			//Assert
			stubDao.AssertWasCalled(x => x.AddTask(Arg<Task>.Matches(t => t.Id != default(Guid))));
			stubDao.AssertWasCalled(x => x.AddTask(Arg<Task>.Matches(t => t.Complete == false)));
	        stubDao.AssertWasCalled(x => x.AddTask(Arg<Task>.Matches(t => t.Description == "Desc")));
	        stubDao.AssertWasCalled(x => x.AddTask(Arg<Task>.Matches(t => t.Due == new DateTime(2010, 1, 1))));
	        stubDao.AssertWasCalled(x => x.AddTask(Arg<Task>.Matches(t => t.Name == "Name")));
	        stubDao.AssertWasCalled(x => x.AddTask(Arg<Task>.Matches(t => t.TaskListId == taskListGuid)));
			
			Assert.IsInstanceOf<EmptyResultWithStatus>(ar);
			EmptyResultWithStatus ers = ar as EmptyResultWithStatus;
			Assert.AreEqual(201, ers.StatusCode);
			Assert.IsNotNull(ers.Location);
			Assert.Greater(ers.Location.ToString().Length, 0);
			Assert.AreNotEqual(default(Guid).ToString("D"), ers.Location.ToString());
		}
		
		#endregion
		
		#region Update Tests
		
		[Test]
		public void TestUpdate_Success()
		{
			//Arrange
			Guid taskListGuid = new Guid("00000000-0000-0000-1111-000000000001");
			Guid taskGuid = new Guid("00000000-0000-0000-0000-000000000001");
			Task originalTask = new Task()
			{
				Id = taskGuid,
				Complete = false,
				Description = "OrigDesc",
				Due = new DateTime(2010, 1, 1),
				Name = "OrigName",
				TaskListId = taskListGuid
			};
			Task newTask = new Task()
			{
				Complete = false,
				Description = "NewDesc",
				Due = new DateTime(2011, 1, 1),
				Name = "NewName"
			};
			var stubDao = MockRepository.GenerateStub<ITaskListDao>();
			stubDao.Stub(x => x.GetTask(Arg<Guid>.Is.Equal(taskGuid))).Return(originalTask);
			TaskController controller = new TaskController(stubDao);
			
			//Act
			ActionResult ar = controller.UpdateTask(taskGuid, newTask);
			
			//Assert
			stubDao.AssertWasCalled(x => x.UpdateTask(Arg<Task>.Matches(t => t.Id == taskGuid)));
			stubDao.AssertWasCalled(x => x.UpdateTask(Arg<Task>.Matches(t => t.Complete == false)));
	        stubDao.AssertWasCalled(x => x.UpdateTask(Arg<Task>.Matches(t => t.Description == "NewDesc")));
	        stubDao.AssertWasCalled(x => x.UpdateTask(Arg<Task>.Matches(t => t.Due == new DateTime(2011, 1, 1))));
	        stubDao.AssertWasCalled(x => x.UpdateTask(Arg<Task>.Matches(t => t.Name == "NewName")));
	        stubDao.AssertWasCalled(x => x.UpdateTask(Arg<Task>.Matches(t => t.TaskListId == taskListGuid)));
			
			Assert.IsInstanceOf<EmptyResult>(ar);
		}

		[Test]
		public void TestUpdate_NotFound()
		{
			//Arrange
			Guid taskGuid = new Guid("00000000-0000-0000-0000-000000000001");

			Task newTask = new Task()
			{
				Complete = false,
				Description = "NewDesc",
				Due = new DateTime(2011, 1, 1),
				Name = "NewName"
			};
			var stubDao = MockRepository.GenerateStub<ITaskListDao>();
			stubDao.Stub(x => x.GetTask(Arg<Guid>.Is.Anything)).Return(null);
			TaskController controller = new TaskController(stubDao);
			
			//Act
			ActionResult ar = controller.UpdateTask(taskGuid, newTask);
			
			//Assert
			stubDao.AssertWasNotCalled(x => x.UpdateTask(Arg<Task>.Is.Anything));
			
			Assert.IsInstanceOf<EmptyResultWithStatus>(ar);
			EmptyResultWithStatus ers = ar as EmptyResultWithStatus;
			Assert.AreEqual(404, ers.StatusCode);
		}

		#endregion		
		
		#region Delete Tests
		
		[Test]
		public void TestDelete_Success()
		{
			//Arrange
			Guid taskGuid = new Guid("00000000-0000-0000-0000-000000000001");
			var stubDao = MockRepository.GenerateStub<ITaskListDao>();
			TaskController controller = new TaskController(stubDao);
			
			//Act
			ActionResult ar = controller.DeleteTask(taskGuid);
			
			//Assert
			stubDao.AssertWasCalled(x => x.DeleteTask(Arg<Guid>.Is.Equal(taskGuid)));
			
			Assert.IsInstanceOf<EmptyResultWithStatus>(ar);
			
			EmptyResultWithStatus ers = ar as EmptyResultWithStatus;
			Assert.AreEqual(204, ers.StatusCode);
		}

		#endregion	
	}
}

