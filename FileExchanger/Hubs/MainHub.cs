using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace FileExchanger.Hubs
{
	public class MainHub : Hub
	{
		public MainHub()
		{
	
		}

		public override async Task OnConnectedAsync()
		{
			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
			await base.OnDisconnectedAsync(exception);
		}
	}
}
