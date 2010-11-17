using System;
using System.Web.Mvc;

using log4net;

namespace TaskListRESTService
{
	public class EmptyResultWithStatus : EmptyResult
	{
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private int _statusCode;
		private Uri _location;
		
		public EmptyResultWithStatus (int statusCode, Uri location )
		{
			_statusCode = statusCode;
			_location = location;
		}
		
		public override void ExecuteResult (ControllerContext context)
		{
			context.HttpContext.Response.StatusCode = _statusCode;
			
			if (_location != null)
			{
				Uri locationUri = _location;
				
				if (!locationUri.IsAbsoluteUri)
				{
					UriBuilder builder = new UriBuilder(context.HttpContext.Request.Url);
					if (!builder.Path.EndsWith("/"))
					{
						builder.Path = builder.Path + "/";
					}
					builder.Path = builder.Path + locationUri.ToString();
					
					log.DebugFormat("Request Url is: {0}", context.HttpContext.Request.Url);
					log.DebugFormat("loc uri is: {0}", locationUri);

					locationUri = builder.Uri;
				}
				
				log.DebugFormat("Setting HTTP Location header to: {0}", locationUri);
				context.HttpContext.Response.AddHeader("Location", locationUri.ToString());
			}
			
			base.ExecuteResult (context);
		}
	}
}

