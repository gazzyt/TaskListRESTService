
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

using TaskListRESTService.Utilities;

namespace TaskListRESTService.Mvc
{
	
	
	public class JsonResult : ActionResult
	{

		public JsonResult()
		{
		}
		
		public object Data {get; set;}

		public override void ExecuteResult (ControllerContext context)
		{
			if (Data != null)
			{
				HttpResponseBase resp = context.RequestContext.HttpContext.Response;
				
				resp.ContentType = "application/json";
				
				using (Stream respStream = resp.OutputStream)
				{
					Json.ObjectToJson(Data, respStream);
				}
			}
		}		
	}
}
