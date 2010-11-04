using System;
using System.Web;

using log4net;
using NHibernate;

using TaskListDaoHibernateImpl;

namespace TaskListDaoHibernateImpl.Web
{
	
	public class WebSessionStorage : ISessionStorage
	{
		private const string NH_SESSION_KEY = "NH_SESSION_KEY";
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		#region ISessionStorage implementation
		public ISession GetSession ()
		{
			log.Debug("Returning session from HttpContext.Items");
			return HttpContext.Current.Items[NH_SESSION_KEY] as ISession;
		}
		
		public void SetSession (ISession session)
		{
			log.Debug("Storing session in HTTPContext.Items");
			HttpContext.Current.Items[NH_SESSION_KEY] = session;
		}
		#endregion

	}

}
