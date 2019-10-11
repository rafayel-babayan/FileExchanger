using FileExchanger.Data;
using FileExchanger.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
    }
}