
using System;
using System.Web;
using System.Web.Mvc;

using TaskListDao.Model;

namespace TaskListRESTService.Mvc.Binders
{
	
	
	public class TaskBinder : BaseBinder
	{

		public override object BindModel (ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			object retval = DeserializeRequestBody<Task>(controllerContext.HttpContext.Request);
			
			return retval;
		}		
	}
}
