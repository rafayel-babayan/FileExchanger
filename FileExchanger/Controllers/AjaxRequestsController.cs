using FileExchanger.Data;
using FileExchanger.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FileExchanger.Helpers;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FileExchanger.Controllers
{
	public class AjaxRequestsController : Controller
    {
		private readonly ApplicationDbContext _context;
		private readonly UserManager<User> _userManager;
		public AjaxRequestsController(ApplicationDbContext context, UserManager<User> userManager)
		{
			_context = context;
			_userManager = userManager;
		}



		public async Task<IActionResult> ConnectToGroup(string userId)
		{
			
			if (Request.IsAjaxRequest())
			{
				var thisUserId = _userManager.GetUserId(User);

				var groups = _context.Groups
			   .Include(grp => grp.UserGroups).ToList();

				foreach(var item in groups)
				{
					if(item.Users.Contains(await _userManager.FindByIdAsync(thisUserId))
						&& item.Users.Contains(await _userManager.GetUserAsync(User)))
						   BadRequest("Group already exists!");
				};

				var group = new Group
				{
					UserGroups = new List<UserGroup>
					{
						{ new UserGroup{UserId = thisUserId} },
						{ new UserGroup{UserId = userId} }
					}
				};

				_context.Groups.Add(group);
				_context.SaveChanges();
			}

			return BadRequest();
		}
	}
}