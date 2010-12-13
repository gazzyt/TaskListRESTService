
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using TaskListDao.Model;
using TaskListRESTService.Models;
using TaskListRESTService.Mvc;
using TaskListRESTService.Mvc.Binders;

namespace TaskListRESTService
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
			                "Task",
			                "TaskList/{taskListId}/tasks/{taskId}",
			                new { controller = "Task", action = "Task" },
							new { taskListId = ".*", taskId = ".*" }
			);
			
			routes.MapRoute(
			                "TaskListTasks",
			                "TaskList/{taskListId}/tasks",
			                new { controller = "Task", action = "TaskListTasks" },
							new { taskListId = ".*"}
			);
							

			routes.MapRoute(
			                "Tasks",
			                "TaskList/{taskListId}",
			                new { controller = "TaskList", action = "TaskList" },
							new { taskListId = ".*" }
			);

			routes.MapRoute(
			                "TaskLists",
			                "TaskList",
			                new { controller = "TaskLists", action = "TaskLists" }
			);
			
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = "" }
            );

        }
		
		public static void RegisterBinders()
		{
			ModelBinders.Binders[typeof(TaskListViewModel)] = new TaskListBinder();
			ModelBinders.Binders[typeof(TaskViewModel)] = new TaskBinder();
			ModelBinders.Binders[typeof(Task)] = new TaskBinder();
		}

        protected void Application_Start()
        {
			RegisterBinders();
            RegisterRoutes(RouteTable.Routes);
			ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory());
        }
    }
}