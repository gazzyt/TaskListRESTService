using System;
using System.Web.Mvc;

using log4net;

using TaskListRESTService.Utilities;

namespace TaskListRESTService
{
	public class EmptyResultWithStatus : EmptyResult
	{
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private int _statusCode;
		private Uri _location;
		
		public Uri Location {
			get {
				return _location;
			}
			set {
				_location = value;
			}
		}

		public int StatusCode
		{
			get {
				return _statusCode;
			}
			set {
				_statusCode = value;
			}
		}
		
	
		
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
					locationUri = context.HttpContext.Request.Url.Append(locationUri);
				}
				
				log.DebugFormat("Setting HTTP Location header to: {0}", locationUri);
				context.HttpContext.Response.AddHeader("Location", locationUri.ToString());
			}
			
			base.ExecuteResult (context);
		}
	}
}

