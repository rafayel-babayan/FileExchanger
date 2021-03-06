﻿using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using FileExchanger.Models;
using FileExchanger.Data;
using Microsoft.AspNetCore.Identity;
using FileExchanger.Hubs;

namespace FileExchanger.Controllers
{
    public class HomeController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<User> _userManager;
		public HomeController(ApplicationDbContext context, UserManager<User> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public IActionResult Index()
		{
           
			ViewBag.OnlineUsers = _context.Users
				.Where(usr => MainHub._connections.HasConnected(usr.Id) 
                                && usr.Id != _userManager.GetUserId(User))
				.ToList(); // Get all online users and send to view

			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
