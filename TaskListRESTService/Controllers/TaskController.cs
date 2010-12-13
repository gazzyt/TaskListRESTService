
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

using TaskListDao;
using TaskListDao.Model;
using TaskListRESTService.Models;
using TaskListRESTService.Mvc;

namespace TaskListRESTService.Controllers
{
	
	
	public class TaskController : BaseController
	{
		private readonly ITaskListDao dao;
		
		public TaskController(ITaskListDao dao)
		{
			if (dao == null)
			{
				throw new ArgumentNullException("dao");
			}
			
			this.dao = dao;
		}
		
		[ActionName("Task")]
		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult GetTask(Guid taskId, string format)
		{
			Task task = dao.GetTask(taskId);
			if (task == null)
			{
				return new EmptyResultWithStatus(404);
			}
			
			TaskViewModel tvm = new TaskViewModel(task);
			
			return SelectActionResult("Task", tvm, format);
		}
		
		[ActionName("TaskListTasks")]
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Create(Guid taskListId, Task t)
		{
			t.Id = Guid.NewGuid();
			t.TaskListId = taskListId;
			dao.AddTask(t);
			return new EmptyResultWithStatus(201, new Uri(t.Id.ToString("D"), UriKind.Relative));
		}
		

		[ActionName("Task")]
		[AcceptVerbsWithOverride(HttpVerbs.Put)]
		public ActionResult GetTask(Guid taskId, Task task)
		{
			Task taskToUpdate = dao.GetTask(taskId);
			taskToUpdate.Merge(task);
			dao.UpdateTask(taskToUpdate);
			return new EmptyResult();
		}
		
		[ActionName("Task")]
		[AcceptVerbsWithOverride(HttpVerbs.Delete)]
		public ActionResult DeleteTask(Guid taskId)
		{
			dao.DeleteTask(taskId);
			return new EmptyResult();
		}
			
	}
}
