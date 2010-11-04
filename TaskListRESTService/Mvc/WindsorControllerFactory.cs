
using System;
using System.Web;
using System.Web.Mvc;

using Castle.Windsor;
using log4net;

namespace TaskListRESTService.Mvc
{
	
	
	public class WindsorControllerFactory : IControllerFactory
	{
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private readonly WindsorContainer container = new WindsorContainer(HttpContext.Current.Server.MapPath("~/windsor.config"));
		
		#region IControllerFactory implementation
		public IController CreateController (System.Web.Routing.RequestContext requestContext, string controllerName)
		{
			return container.Resolve<IController>(controllerName);
		}
		
		public void ReleaseController (IController controller)
		{
			container.Release(controller);
		}
		#endregion
		
		public WindsorControllerFactory()
		{
			log.Debug("Creating WindsorControllerFactory");
		}
	}
}
