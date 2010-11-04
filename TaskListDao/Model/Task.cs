
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
	}
}
