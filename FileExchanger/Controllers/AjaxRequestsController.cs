using FileExchanger.Data;
using FileExchanger.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FileExchanger.Helpers;
using System.Collections.Generic;

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



		public IActionResult ConnectToGroup(string userId)
		{
			if (Request.IsAjaxRequest())
			{
				var thisUserIs = _userManager.GetUserId(User);

				var group = new Group
				{
					UserGroups = new List<UserGroup>
					{
						{ new UserGroup{UserId = thisUserIs} },
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