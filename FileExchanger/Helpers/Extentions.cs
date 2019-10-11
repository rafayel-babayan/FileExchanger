using Microsoft.AspNetCore.Http;
using System;

namespace FileExchanger.Helpers
{
	public static class Extentions
	{
		private const string RequestedWithHeader = "X-Requested-With";
		private const string XmlHttpRequest = "XMLHttpRequest";
		public static bool IsAjaxRequest(this HttpRequest request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}

			if (request.Headers != null) 
			{
				return request.Headers[RequestedWithHeader] == XmlHttpRequest;
			}

			return false;
		}
	}
}
