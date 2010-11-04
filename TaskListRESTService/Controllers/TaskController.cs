
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

using TaskListDao;
using TaskListDao.Model;
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
		public ActionResult GetTask(Guid taskId)
		{
			return SelectActionResult("Task", dao.GetTask(taskId));
		}

		[ActionName("Task")]
		[AcceptVerbsWithOverride(HttpVerbs.Put)]
		public ActionResult GetTask(Guid taskListId, Guid taskId, Task task)
		{
			task.TaskListId = taskListId;
			task.Id = taskId;
			dao.UpdateTask(task);
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
