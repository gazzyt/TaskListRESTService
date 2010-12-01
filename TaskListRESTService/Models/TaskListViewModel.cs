using System;
using System.Collections.Generic;

using TaskListDao.Model;

namespace TaskListRESTService.Models
{
	public class TaskListViewModel
	{
		private TaskList _taskList;
		
		public TaskListViewModel()
		{
		}
		
		public TaskListViewModel (TaskList taskList)
		{
			_taskList = taskList;
		}

		public String Self
		{
			get
			{
				return "/lala/" + _taskList.Id.ToString("D");
			}
		}
		
		public Guid Id
		{
			get
			{
				return _taskList.Id;
			}
		}
		
		public string Name
		{
			get
			{
				return _taskList.Name;
			}
		}
		
		public ICollection<Task> Tasks
		{
			get
			{
				return _taskList.Tasks;
			}
		}
	}
}

