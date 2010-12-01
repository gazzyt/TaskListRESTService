using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using TaskListDao.Model;

namespace TaskListRESTService.Models
{
	[DataContract(Name="TaskList", Namespace="")]
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
		
		[DataMember]
		public Guid Id
		{
			get
			{
				return _taskList.Id;
			}
			set
			{
				_taskList.Id = value;
			}
		}
		
		[DataMember]
		public string Name
		{
			get
			{
				return _taskList.Name;
			}
			set
			{
				_taskList.Name = value;
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

