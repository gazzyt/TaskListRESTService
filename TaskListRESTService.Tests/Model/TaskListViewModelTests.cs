using System;
using NUnit.Framework;

using TaskListRESTService.Models;

namespace TaskListRESTService.Tests
{
	[TestFixture()]
	public class TaskListViewModelTests
	{
		[Test()]
		public void TestUrl ()
		{
			TaskListViewModel vm = new TaskListViewModel();
			vm.Id = new Guid("0c51048d-1820-421d-ac41-13d579d4a6ad");
			
			Assert.AreEqual("/TaskList/0c51048d-1820-421d-ac41-13d579d4a6ad", vm.Self);
		}
		
		[Test]
		public void TestTasksUrl ()
		{
			TaskListViewModel vm = new TaskListViewModel();
			vm.Id = new Guid("0c51048d-1820-421d-ac41-13d579d4a6ad");
			
			Assert.AreEqual("/TaskList/0c51048d-1820-421d-ac41-13d579d4a6ad/tasks", vm.TasksLink);
		}
	}
}

