
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
		public ActionResult List(string format)
		{
			IEnumerable<TaskList> taskLists = dao.GetTaskLists();
			
			return SelectActionResult("List", taskLists, format);
		}
		
			
		[ActionName("TaskLists")]
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Create(TaskList tl)
		{
			tl.Id = Guid.NewGuid();
			dao.AddTaskList(tl);
			return new EmptyResultWithStatus(201, new Uri(tl.Id.ToString("D"), UriKind.Relative));
		}
		
		[ActionName("TaskLists")]
		[AcceptVerbs(HttpVerbs.Delete)]
		public ActionResult DeleteAll()
		{
			dao.DeleteAllTaskLists();
			return new EmptyResult();
		}
	}
}
