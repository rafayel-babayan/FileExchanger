using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using FileExchanger.Models;
using Microsoft.AspNetCore.Identity;
using FileExchanger.Data;

namespace FileExchanger.Hubs
{
    public class MainHub : Hub
	{
		//private readonly UserManager<User> _userManager;
		//private readonly ApplicationDbContext _context;
        public static ConnectionMapping<string> _connections = new ConnectionMapping<string>();
		public MainHub(UserManager<User> userManager, ApplicationDbContext context)
		{
			//_userManager = userManager;
			//_context = context;
		}

        #region Connect / Disconnect
        public override async Task OnConnectedAsync()
		{
            _connections.Add(Context.UserIdentifier, Context.ConnectionId);

            //var connection = new Connection { ConnectionId = Context.ConnectionId };
			//var user = await _context.Users // Get current user with connection list
			//	.Include(usr => usr.Connections)
			//	.Where(uid => uid.Id == _userManager.GetUserId(Context.User))
			//	.FirstOrDefaultAsync();

			//if (user.Connections == null)
			//	user.Connections = new List<Connection>();
			//											 // If user don't have connections list, 
			//user.Connections.Add(connection);           // create list and add connection

			//(await _userManager.GetUserAsync(Context.User)).IsConnected = true; // Connecting user

			//await _context.SaveChangesAsync();
			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
            _connections.Remove(Context.UserIdentifier, Context.ConnectionId);
            //	var connections = await _context.Connections // Get current user's all connections
            //		.Where(uid => uid.UserId == _userManager.GetUserId(Context.User))
            //		.ToListAsync();

            //	var connection = connections // Get current user's current connection
            //		.Where(uid => uid.ConnectionId == Context.ConnectionId)
            //		.FirstOrDefault();

            //	if (connection != null) 
            //	{
            //		_context.Connections.Remove(connection); // Removing current connection
            //		connections.Remove(connection);

            //		if (connections.Count < 1) 
            //		{
            //			(await _userManager.GetUserAsync(Context.User)).IsConnected = false; // Disconnecting user					
            //		}

            //		await _context.SaveChangesAsync();
            //	}

            await base.OnDisconnectedAsync(exception);
		}
        #endregion
    }
}
