
using System;
using System.Web;
using System.Web.Mvc;

using TaskListDao.Model;

namespace TaskListRESTService.Mvc.Binders
{
	
	
	public class TaskListBinder : BaseBinder
	{

		#region IModelBinder implementation
		public override object BindModel (ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			object retval = DeserializeRequestBody<TaskList>(controllerContext.HttpContext.Request);
			
			return retval;
		}
		#endregion
		
		public TaskListBinder()
		{
		}
	}
}
