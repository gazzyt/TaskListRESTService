
using System;
using System.Web;
using System.Web.Mvc;

namespace TaskListRESTService.Mvc.Binders
{
	
	
	public abstract class BaseBinder : IModelBinder
	{
		
		protected internal T DeserializeRequestBody<T>(HttpRequestBase request)
		{
			string mimeType = request.ContentType;
			T retval = default(T);
			
			if (mimeType.Contains("text/xml"))
			{
				retval = TaskListRESTService.Utilities.Xml.XmlToObject<T>(request.InputStream);
			}
			else
			{
				throw new ArgumentException("Unrecognised content type: " + mimeType);
			}
			
			return retval;
		}

		#region IModelBinder implementation
		public abstract object BindModel (ControllerContext controllerContext, ModelBindingContext bindingContext);
		#endregion

	}
}
