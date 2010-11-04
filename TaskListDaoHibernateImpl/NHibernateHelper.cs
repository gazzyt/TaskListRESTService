using System;
using log4net;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

using Environment = NHibernate.Cfg.Environment;

using TaskListDao.Model;

namespace TaskListDaoHibernateImpl
{
    public class NHibernateHelper
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private ISessionStorage _sessionStorage;
        private ISessionFactory _sessionFactory;
		private Configuration _configuration;

		public NHibernateHelper(ISessionStorage sessionStorage)
		{
			_sessionStorage = sessionStorage;
		}
		
		public string Dialect {get; set;}
		public string DriverClass {get; set;}
		public string ConnectionString {get; set;}
		public bool ExportSchema {get; set;}
		
		private Configuration Configuration
		{
			get
			{
				if (_configuration == null)
				{
                    var configuration = new Configuration();
                    configuration.Configure();
					
					configuration.SetProperty(Environment.Dialect, Dialect);
					configuration.SetProperty(Environment.ConnectionDriver, DriverClass);
					configuration.SetProperty(Environment.ConnectionString, ConnectionString);
					
                    configuration.AddAssembly(typeof(NHibernateHelper).Assembly);
					
					_configuration = configuration;
				}
				
				return _configuration;
			}
		}
					
        private ISessionFactory SessionFactory
        {
            get
            {
                if(_sessionFactory == null)
                {
                    _sessionFactory = Configuration.BuildSessionFactory();

					if (ExportSchema)
					{
						log.Debug("About to create schema");
						//ISession session = _sessionFactory.OpenSession();
						//new SchemaExport(Configuration).Execute(true, true, false, session.Connection, Console.Out);
						new SchemaExport(Configuration).Create(true, true);
						//session.Dispose();
						log.Debug("Schema created");
					}
                }
                return _sessionFactory;
            }
        }
 
        public ISession OpenSession()
        {
			ISession session = _sessionStorage.GetSession();
			
			if (session == null)
			{
				log.Debug("Creating NHibernate session");
				session = SessionFactory.OpenSession();
				
				_sessionStorage.SetSession(session);
				
				if (ExportSchema)
				{
					log.Debug("About to create schema");
					//ISession session = _sessionFactory.OpenSession();
					//new SchemaExport(Configuration).Execute(true, true, false, session.Connection, Console.Out);
					// new SchemaExport(Configuration).Create(true, true);
					//session.Dispose();
					log.Debug("Schema created");
				}
			}
			
			return session;
        }
    }
}

