using System;
using log4net;

namespace TaskListRESTService
{
	static public class UriHelper
	{
        //private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private static readonly char[] trimChars = {'/'};
		
		public static Uri Append(this Uri root, Uri extra)
		{
			UriBuilder builder = new UriBuilder(root);
			
			builder.Query = null;
			
			if (!builder.Path.EndsWith("/"))
			{
				builder.Path = builder.Path + "/";
			}
			
			string extraString = extra.ToString();
			if (extraString.StartsWith("/"))
			{
				extraString = extraString.TrimStart(trimChars);
			}
			
			builder.Path = builder.Path + extraString;
			
			return builder.Uri;
		}
	}
}

