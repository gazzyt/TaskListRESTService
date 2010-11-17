using System;
using System.Web.Mvc;

namespace TaskListRESTService
{
	public class EmptyResultWithStatus : EmptyResult
	{
		private int _statusCode;
		
		public EmptyResultWithStatus (int statusCode)
		{
			_statusCode = statusCode;
		}
		
		public override void ExecuteResult (ControllerContext context)
		{
			context.HttpContext.Response.StatusCode = _statusCode;
			base.ExecuteResult (context);
		}
	}
}

