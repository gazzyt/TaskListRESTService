
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

using log4net;

using TaskListDao;
using TaskListDao.Model;
using TaskListRESTService.Models;
using TaskListRESTService.Mvc;

namespace TaskListRESTService.Controllers
{
	
	
	public class TaskListController : BaseController
	{
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private readonly ITaskListDao dao;
		
		public TaskListController(ITaskListDao dao)
		{
			if (dao == null)
			{
				throw new ArgumentNullException("dao");
			}
			
			this.dao = dao;
		}
		
		[ActionName("TaskList")]
		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult List(Guid taskListId, string format)
		{
			TaskList taskList = dao.GetTaskList(taskListId);
			if (taskList == null)
			{
				log.Debug("Tasklist is null");
				return new EmptyResultWithStatus(404);
			}
			
			TaskListViewModel viewModel = new TaskListViewModel(taskList);
			
			return SelectActionResult("ListTasks", viewModel, format);
		}
		
			
		[ActionName("TaskList")]
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Create(Guid taskListId, Task t)
		{
			t.TaskListId = taskListId;
			dao.AddTask(t);
			return new EmptyResult();
		}
		
		[ActionName("TaskList")]
		[AcceptVerbsWithOverride(HttpVerbs.Put)]
		public ActionResult Update(Guid taskListId, TaskList taskList)
		{
			taskList.Id = taskListId;
			dao.UpdateTaskList(taskList);
			return new EmptyResult();
		}
		
		[ActionName("TaskList")]
		[AcceptVerbsWithOverride(HttpVerbs.Delete)]
		public ActionResult Delete(Guid taskListId)
		{
			dao.DeleteTaskList(taskListId);
			return new EmptyResult();
		}
	}
}
