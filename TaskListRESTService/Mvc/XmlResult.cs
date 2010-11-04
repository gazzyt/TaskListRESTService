
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

using TaskListRESTService.Utilities;

namespace TaskListRESTService
{
	
	
	public class XmlResult : ActionResult
	{

		public XmlResult()
		{
		}
		
		public object Data {get; set;}

		public override void ExecuteResult (ControllerContext context)
		{
			if (Data != null)
			{
				HttpResponseBase resp = context.RequestContext.HttpContext.Response;
				
				resp.ContentType = "text/xml";
				
				using (Stream respStream = resp.OutputStream)
				{
					Xml.ObjectToXml(Data, respStream);
				}
			}
		}		
	}
}
