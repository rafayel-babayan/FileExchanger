﻿using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileExchanger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace FileExchanger.Services
{
	public class NotificationService 
	{
		private readonly IHubContext<Hubs.MainHub> _hubContext;
        private readonly UserManager<User> _userManager;
        private readonly IViewRenderService _renderService;
        public NotificationService(IHubContext<Hubs.MainHub> hubContext, UserManager<User> userManager, IViewRenderService renderService)
		{
			_hubContext = hubContext;
            _userManager = userManager;
            _renderService = renderService;
		}

        public void OnNewMessage(Message msg)
        {
            var initiatorIds = Hubs.MainHub._connections.GetConnections(msg.AuthorId);

            var inView = _renderService.RenderToStringAsync("Messages/_IncomingMessagePartial", msg);
            var outView = _renderService.RenderToStringAsync("Messages/_OutgoingMessagePartial", msg);

            var others = _hubContext.Clients.AllExcept(initiatorIds.ToList());
            others.SendAsync("ReceiveMessage", inView);

            var i = _hubContext.Clients.Clients(initiatorIds.ToList());
            i.SendAsync("SendMessage", outView);
        }                                                               
    }
}
