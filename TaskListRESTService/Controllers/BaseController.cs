
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

using TaskListRESTService.Mvc;
using TaskListRESTService.Utilities;

using JsonResult = TaskListRESTService.Mvc.JsonResult;

namespace TaskListRESTService
{
	
	
	public class BaseController : Controller
	{
		public virtual ActionResult SelectActionResult(string name, object model, string format)
		{
			ActionResult result = null;
			string requestedFormat = format ?? String.Empty;
			
			switch(requestedFormat.ToLower())
			{
			case "json":
				result = DoJson(model);
				break;
				
			case "xml":
				result = Xml(model);
				break;
				
			default:
				result = View(name, model);
				break;
			}
			
			return result;
		}
		
		protected internal virtual ActionResult SelectActionResult(string name, object model)
		{
			return SelectActionResult(name, model, HttpContext.Request.QueryString["format"]);
		}
		
		protected internal virtual ActionResult Xml(object model)
		{
			return new XmlResult{ Data = model };
		}
		
		protected internal virtual ActionResult DoJson(object model)
		{
			return new JsonResult{ Data = model };
		}
	}
}
