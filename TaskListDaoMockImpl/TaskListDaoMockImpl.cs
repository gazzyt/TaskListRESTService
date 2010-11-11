
using System;
using System.Collections.Generic;

using TaskListDao;
using TaskListDao.Model;

namespace TaskListDaoMockImpl
{
	
	
	public class TaskListDaoMockImpl : ITaskListDao
	{
		private List<TaskList> _taskLists;

		#region ITaskListDao implementation
		public IEnumerable<TaskList> GetTaskLists ()
		{
			return _taskLists;
		}

		public void AddTaskList (TaskList newList)
		{
			_taskLists.Add(newList);
		}
		
		public void UpdateTaskList(TaskList taskList)
		{
			throw new NotImplementedException();
		}

		public void DeleteTaskList(Guid taskListId)
		{
			throw new NotImplementedException();
		}
		
		public void DeleteAllTaskLists()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Task> GetTasksInList(Guid taskListId)
		{
			throw new NotImplementedException();
		}

		public void AddTask(Task newTask)
		{
			throw new NotImplementedException();
		}

		public Task GetTask(Guid taskId)
		{
			throw new NotImplementedException();
		}

		public void UpdateTask(Task task)
		{
			throw new NotImplementedException();
		}

		public void DeleteTask(Guid taskId)
		{
			throw new NotImplementedException();
		}

#endregion
		
		public TaskListDaoMockImpl()
		{
			_taskLists = new List<TaskList>();
			_taskLists.Add(
			               new TaskList
			               {
			 					Id = Guid.NewGuid(),
								Name = "Tasklist1"
							});
		}


	}
}
