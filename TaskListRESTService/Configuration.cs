using System;
using System.Configuration;

namespace TaskListRESTService
{
	public static class Configuration
	{
		public static string TaskListUrlPattern
		{
			get
			{
				return ConfigurationManager.AppSettings["TaskListUrlPattern"];
			}
		}
		
		public static string TaskListTasksUrlPattern
		{
			get
			{
				return ConfigurationManager.AppSettings["TaskListTasksUrlPattern"];
			}
		}
	}
}

