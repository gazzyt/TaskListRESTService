
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using TaskListDao.Model;
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
			                "TaskList/{taskListId}/{taskId}",
			                new { controller = "Task", action = "Task" },
							new { taskListId = ".*", taskId = ".*" }
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
			ModelBinders.Binders[typeof(TaskList)] = new TaskListBinder();
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