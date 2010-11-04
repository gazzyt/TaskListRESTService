
using System;

using NHibernate;

namespace TaskListDaoHibernateImpl
{
	
	public interface ISessionStorage
	{
		ISession GetSession();
		void SetSession(ISession session);
	}
}
