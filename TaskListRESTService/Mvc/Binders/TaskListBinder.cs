
using System;
using System.Web;
using System.Web.Mvc;

using TaskListRESTService.Models;

namespace TaskListRESTService.Mvc.Binders
{
	
	
	public class TaskListBinder : BaseBinder
	{

		#region IModelBinder implementation
		public override object BindModel (ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			object retval = DeserializeRequestBody<TaskListViewModel>(controllerContext.HttpContext.Request);
			
			return retval;
		}
		#endregion
		
		public TaskListBinder()
		{
		}
	}
}
