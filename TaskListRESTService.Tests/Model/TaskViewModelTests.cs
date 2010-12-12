using System;
using NUnit.Framework;

using TaskListRESTService.Models;

namespace TaskListRESTService.Tests
{
	[TestFixture()]
	public class TaskViewModelTests
	{
		[Test()]
		public void TestUrl ()
		{
			TaskViewModel vm = new TaskViewModel();
			vm.Id = new Guid("0c51048d-1820-421d-ac41-13d579d4a6ad");
			
			Assert.AreEqual("/Task/0c51048d-1820-421d-ac41-13d579d4a6ad", vm.Self);
		}
	}
}

