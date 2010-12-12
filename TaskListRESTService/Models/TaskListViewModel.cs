using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using log4net;
using Newtonsoft.Json;

using TaskListDao.Model;

namespace TaskListRESTService.Models
{
	[DataContract(Name="TaskList", Namespace="")]
	public class TaskListViewModel
	{
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private TaskList _taskList;
		
		public TaskListViewModel()
		{
			log.Debug("Calling default constructor");
			_taskList = new TaskList();
		}
		
		public TaskListViewModel (TaskList taskList)
		{
			log.DebugFormat("Calling constructor with {0}", taskList);
			_taskList = taskList;
		}
		
		[OnDeserializingAttribute]
		internal void Initialize(StreamingContext context)
		{
			_taskList = new TaskList();
		}
		
		[JsonIgnore]
		public TaskList TaskList
		{
			get
			{
				return _taskList;
			}
		}
		
		[DataMember]
		public String Self
		{
			get
			{
				return string.Format(Configuration.TaskListUrlPattern, _taskList.Id.ToString("D"));
			}
			set
			{
			}
		}

		[DataMember]
		public String TasksLink
		{
			get
			{
				return string.Format(Configuration.TaskListTasksUrlPattern, _taskList.Id.ToString("D"));
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
				return _taskList.Id;
			}
			set
			{
				log.DebugFormat("Setting Id to {0}", value.ToString("D"));
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
				log.DebugFormat("Setting Name to {0}", value);
				_taskList.Name = value;
			}
		}
		
		[JsonIgnore]
		public ICollection<Task> Tasks
		{
			get
			{
				return _taskList.Tasks;
			}
		}
	}
}

