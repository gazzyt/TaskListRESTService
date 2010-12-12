using System;
using NUnit.Framework;

using TaskListDao.Model;

namespace TaskListDao.Tests.Model
{
	[TestFixture()]
	public class TaskTests
	{
		[Test]
		public void TestMerge_FullMerge ()
		{
			//Arrange
			Task mainTask = new Task()
			{
				Complete = false,
				Description = "mainDesc",
				Due = new DateTime(2010, 11, 1),
				Id = new Guid("00000000-0000-0000-0000-000000000003"),
				Name = "mainName",
				TaskListId = new Guid("00000000-0000-0000-0000-000000000001")
			};
			
			Task mergeTask = new Task()
			{
				Complete = true,
				Description = "mergeDesc",
				Due = new DateTime(2010, 12, 2),
				Id = new Guid("00000000-0000-0000-0000-000000000004"),
				Name = "mergeName",
				TaskListId = new Guid("00000000-0000-0000-0000-000000000002")
			};
			
			//Act
			mainTask.Merge(mergeTask);
			
			//Assert
			Assert.AreEqual(true, mainTask.Complete);
			Assert.AreEqual("mergeDesc", mainTask.Description);
			Assert.AreEqual(new DateTime(2010, 12, 2), mainTask.Due);
			Assert.AreEqual(new Guid("00000000-0000-0000-0000-000000000003"), mainTask.Id); // Should not change
			Assert.AreEqual("mergeName", mainTask.Name);
			Assert.AreEqual(new Guid("00000000-0000-0000-0000-000000000002"), mainTask.TaskListId);
		}
		
		[Test]
		public void TestMerge_EmptyMerge ()
		{
			//Arrange
			Task mainTask = new Task()
			{
				Complete = true,
				Description = "mainDesc",
				Due = new DateTime(2010, 11, 1),
				Id = new Guid("00000000-0000-0000-0000-000000000003"),
				Name = "mainName",
				TaskListId = new Guid("00000000-0000-0000-0000-000000000001")
			};
			
			Task mergeTask = new Task()
			{
			};
			
			//Act
			mainTask.Merge(mergeTask);
			
			//Assert
			Assert.AreEqual(false, mainTask.Complete); //bool is not nullable so default will be false
			Assert.AreEqual("mainDesc", mainTask.Description);
			Assert.AreEqual(new DateTime(2010, 11, 1), mainTask.Due);
			Assert.AreEqual(new Guid("00000000-0000-0000-0000-000000000003"), mainTask.Id); // Should not change
			Assert.AreEqual("mainName", mainTask.Name);
			Assert.AreEqual(new Guid("00000000-0000-0000-0000-000000000001"), mainTask.TaskListId);
		}
	}
}

