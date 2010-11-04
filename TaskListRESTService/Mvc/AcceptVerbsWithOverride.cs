
using System;
using System.Linq;
using System.Web.Mvc;

namespace TaskListRESTService.Mvc
{
	
	public class AcceptVerbsWithOverride : ActionMethodSelectorAttribute
	{
		private const string methodOverrideParam = "action";
		
		private readonly AcceptVerbsAttribute _acceptVerbs;

		public AcceptVerbsWithOverride(HttpVerbs verbs)
		{
			_acceptVerbs = new AcceptVerbsAttribute(verbs);
		}

		public override bool IsValidForRequest (ControllerContext controllerContext, System.Reflection.MethodInfo methodInfo)
		{
			bool retval;
			
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}
			
			string incomingVerb = controllerContext.HttpContext.Request.HttpMethod;
			
			// if it is a POST then we allow an override for low REST
			// e.g. ?action=delete or ?action=put
			string methodOverride = "";
			if (string.Compare(incomingVerb, "POST", true) == 0)
			{
				methodOverride = controllerContext.HttpContext.Request.Params[methodOverrideParam];
			}
			
			if (string.IsNullOrEmpty(methodOverride))
			{
				retval = _acceptVerbs.IsValidForRequest(controllerContext, methodInfo);
			}
			else
			{
				retval = _acceptVerbs.Verbs.Contains(methodOverride, StringComparer.OrdinalIgnoreCase);
			}
			
			return retval;
		}		

	}
}
