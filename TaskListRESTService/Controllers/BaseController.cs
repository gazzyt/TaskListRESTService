
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

using TaskListRESTService.Utilities;

namespace TaskListRESTService
{
	
	
	public class BaseController : Controller
	{
		protected internal virtual ActionResult SelectActionResult(string name, object model)
		{
			ActionResult result = null;
			string requestedFormat = HttpContext.Request.QueryString["format"] ?? String.Empty;
			
			switch(requestedFormat)
			{
			case "json":
				result = Json(model);
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
		
		protected internal virtual ActionResult Xml(object model)
		{
			return new XmlResult{ Data = model };
		}
	}
}
