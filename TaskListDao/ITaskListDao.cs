
using System;
using System.Collections.Generic;

using TaskListDao.Model;

namespace TaskListDao
{
	
	
	public interface ITaskListDao
	{
		IEnumerable<TaskList> GetTaskLists();
		void AddTaskList(TaskList newList);
		void UpdateTaskList(TaskList taskList);
		void DeleteTaskList(Guid taskListId);
		
		IEnumerable<Task> GetTasksInList(Guid taskListId);
		void AddTask(Task newTask);
		Task GetTask(Guid taskId);
		void UpdateTask(Task task);
		void DeleteTask(Guid taskId);
	}
}
