
using System;
using System.Web;
using System.Web.Mvc;

using TaskListRESTService.Models;

namespace TaskListRESTService.Mvc.Binders
{
	
	
	public class TaskBinder : BaseBinder
	{

		public override object BindModel (ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			object retval = DeserializeRequestBody<TaskViewModel>(controllerContext.HttpContext.Request);
			
			return retval;
		}		
	}
}
