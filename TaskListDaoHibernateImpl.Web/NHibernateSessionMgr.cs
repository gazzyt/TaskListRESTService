
using System;
using System.Web;

using log4net;
using NHibernate;

namespace TaskListDaoHibernateImpl.Web
{
	
	
	public class NHibernateSessionMgr : IHttpModule
	{
		private const string NH_SESSION_KEY = "NH_SESSION_KEY";
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		#region IHttpModule implementation
		public void Dispose ()
		{
		}
		
		public void Init (HttpApplication application)
		{
			application.EndRequest += HandleEndRequest;
		}

		void HandleEndRequest(object sender, EventArgs e)
		{
			ISession session = HttpContext.Current.Items[NH_SESSION_KEY] as ISession;
			
			if (session != null)
			{
				log.Debug("Disposing NHibernate session");
				session.Dispose();
				HttpContext.Current.Items.Remove(NH_SESSION_KEY);
			}
		}
		#endregion
		
	}
}
