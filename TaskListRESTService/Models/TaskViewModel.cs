using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using log4net;
using Newtonsoft.Json;

using TaskListDao.Model;

namespace TaskListRESTService.Models
{
	[DataContract(Name="Task", Namespace="")]
	public class TaskViewModel
	{
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private Task _task;
		
		public TaskViewModel()
		{
			log.Debug("Calling default constructor");
			_task = new Task();
		}
		
		public TaskViewModel (Task task)
		{
			log.DebugFormat("Calling constructor with {0}", task);
			_task = task;
		}
		
		[OnDeserializingAttribute]
		internal void Initialize(StreamingContext context)
		{
			_task = new Task();
		}
		
		[JsonIgnore]
		public Task Task
		{
			get
			{
				return _task;
			}
		}
		
		[DataMember]
		public String Self
		{
			get
			{
				return string.Format(Configuration.TaskUrlPattern, _task.Id.ToString("D"));
			}
			set
			{
			}
		}

		[JsonIgnore]
		public Guid Id
		{
			get
			{
				return _task.Id;
			}
			set
			{
				log.DebugFormat("Setting Id to {0}", value.ToString("D"));
				_task.Id = value;
			}
		}
		
		[DataMember]
		public string Name
		{
			get
			{
				return _task.Name;
			}
			set
			{
				log.DebugFormat("Setting Name to {0}", value);
				_task.Name = value;
			}
		}
		
		[DataMember]
		public string Description
		{
			get
			{
				return _task.Description;
			}
			set
			{
				log.DebugFormat("Setting Description to {0}", value);
				_task.Description = value;
			}
		}
	}
}

