
using System;

namespace TaskListDao.Model
{
	
	
	public class Task
	{
		public virtual Guid Id
		{
			get; set;
		}
		
		public virtual Guid TaskListId
		{
			get; set;
		}
		
		public virtual string Name
		{
			get; set;
		}
		
		public virtual string Description
		{
			get; set;
		}
		
		public virtual DateTime Due
		{
			get; set;
		}
		
		public virtual bool Complete
		{
			get; set;
		}
		
		public virtual void Merge(Task taskToMerge)
		{
			if (taskToMerge.Name != default(string))
			{
				Name = taskToMerge.Name;
			}
			
			if (taskToMerge.Description != default(string))
			{
				Description = taskToMerge.Description;
			}
			
			if (taskToMerge.Due != default(DateTime))
			{
				Due = taskToMerge.Due;
			}
			
			Complete = taskToMerge.Complete;
			
			if (taskToMerge.TaskListId != default(Guid))
			{
				TaskListId = taskToMerge.TaskListId;
			}
		}
	}
}
