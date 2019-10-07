using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileExchanger.Services
{
	public class NotificationService
	{
		private readonly IHubContext<Hub> _hubContext;
		public NotificationService(IHubContext<Hub> hubContext)
		{
			_hubContext = hubContext;
		}
	}
}
