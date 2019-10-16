using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileExchanger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace FileExchanger.Services
{
	public class NotificationService : Controller
	{
		private readonly IHubContext<Hubs.MainHub> _hubContext;
        private readonly UserManager<User> _userManager;
        public NotificationService(IHubContext<Hubs.MainHub> hubContext, UserManager<User> userManager)
		{
			_hubContext = hubContext;
            _userManager = userManager;
		}

        public void OnNewMessage(Message msg)
        {
            var initiatorIds = Hubs.MainHub._connections.GetConnections(_userManager.GetUserId(User));

            PartialViewResult inView = PartialView("Messages/_IncomingMessagePartial", msg);
            PartialViewResult outView = PartialView("Messages/_OutgoingMessagePartial", msg);

            _hubContext.Clients.AllExcept(initiatorIds.ToList()).SendAsync("ReceiveMessage", outView);
            _hubContext.Clients.Users(initiatorIds.ToList()).SendAsync("SendMessage", inView);
        }
    }
}
