using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using FileExchanger.Models;
using Microsoft.AspNetCore.Identity;
using FileExchanger.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FileExchanger.Hubs
{
	public class MainHub : Hub
	{
		private readonly UserManager<User> _userManager;
		private readonly ApplicationDbContext _context;
		public MainHub(UserManager<User> userManager, ApplicationDbContext context)
		{
			_userManager = userManager;
			_context = context;
		}

		public override async Task OnConnectedAsync()
		{
			var connection = new Connection { ConnectionId = Context.ConnectionId };

			var user = await _context.Users // Get current user
				.Include(usr => usr.Connections)
				.Where(uid => uid.Id == _userManager.GetUserId(Context.User))
				.FirstOrDefaultAsync();

			if (user.Connections == null)
				user.Connections = new List<Connection>();

			user.Connections.Add(connection); // If user don't have connections list, 
											  // create list and add connection

			await _context.SaveChangesAsync();
			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
			var connection = await _context.Connections
				.Where(uid => uid.ConnectionId == Context.ConnectionId
					&& uid.UserId == _userManager.GetUserId(Context.User))
				.FirstOrDefaultAsync();

			if (connection != null)
				_context.Connections.Remove(connection);

			await _context.SaveChangesAsync();
			await base.OnDisconnectedAsync(exception);
		}
	}
}
