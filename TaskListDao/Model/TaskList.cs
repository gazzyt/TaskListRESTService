
using System;
using System.Collections.Generic;

namespace TaskListDao.Model
{
	
	
	public class TaskList
	{
		
		public TaskList()
		{
		}
		
		public virtual Guid Id
		{
			get; set;
		}
		
		public virtual string Name
		{
			get; set;
		}
		
		public virtual ICollection<Task> Tasks
		{
			get; set;
		}
	}
}
