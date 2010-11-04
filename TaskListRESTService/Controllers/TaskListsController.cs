
using System;
using System.Collections.Generic;
using System.Web.Mvc;

using TaskListDao;
using TaskListDao.Model;

namespace TaskListRESTService.Controllers
{
	
	
	public class TaskListsController : BaseController
	{
		private readonly ITaskListDao dao;
		
		public TaskListsController(ITaskListDao dao)
		{
			if (dao == null)
			{
				throw new ArgumentNullException("dao");
			}
			
			this.dao = dao;
		}
		
		[ActionName("TaskLists")]
		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult List()
		{
			IEnumerable<TaskList> taskLists = dao.GetTaskLists();
			
			return SelectActionResult("List", taskLists);
		}
		
			
		[ActionName("TaskLists")]
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Create(TaskList tl)
		{
			dao.AddTaskList(tl);
			return new EmptyResult();
		}
	}
}
