using FileExchanger.Data;
using FileExchanger.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FileExchanger.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using FileExchanger.ViewModels;

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

        #region Messanger 
        public async Task<IActionResult> ConnectToGroup(string userId)
        {
            if (Request.IsAjaxRequest())
            {
                IEnumerable<Message> messages = null;

                User thisUser = await _userManager.GetUserAsync(User);
                User otherUser = await _userManager.FindByIdAsync(userId);

                var groups = _context.Groups
                                     .Include(grp => grp.UserGroups)
                                     .ToList();

                if (groups.Any(item => item.Users.Contains(thisUser)
                                  && item.Users.Contains(otherUser)))
                {
                    string id = groups.SingleOrDefault(x => x.Users.Contains(thisUser) && x.Users.Contains(otherUser)).Id; // If group exist
                    messages = _context.Messages.Where(msg => msg.GroupId == id).ToList();                 // loading messages
                }
                else
                {
                    var group = new Group            // If group not exist
                    {                               // creating new group 
                        UserGroups = new List<UserGroup>
                        {
                            { new UserGroup { UserId = thisUser.Id } },
                            { new UserGroup { UserId = otherUser.Id } }
                        },

                        Messages = new List<Message>
                        {
                            { new Message { Content = "Group created.", SendDate = DateTime.Now } },
                            { new Message { Content = $"You and {otherUser.UserName} are now friends.", SendDate = DateTime.Now } }
                        }
                    };


                    _context.Groups.Add(group);
                    _context.SaveChanges();

                    messages = group.Messages;
                }

               messages =  messages.OrderBy(x => x.SendDate);

                return PartialView("Messages/_DialogPartial", messages); // returning partial gialog
            }

            return BadRequest();
        }

        public async Task<IActionResult> SendMessage(MessageViewModel model)
        {
            User thisUser = await _userManager.GetUserAsync(User);
            User otherUser = await _userManager.FindByIdAsync(model.To);

            Group grp = _context.Groups
                .Include(x => x.UserGroups)
                .ToList()
                .SingleOrDefault(x=>x.Users.Contains(thisUser) && x.Users.Contains(otherUser));

            Message msg = new Message
            {
                AuthorId = thisUser.Id,
                Content = model.Content,
                SendDate = DateTime.Now,
                GroupId = grp.Id
            };
            await _context.Messages.AddAsync(msg);
            await _context.SaveChangesAsync();

            return Ok();
        }
        #endregion
    }
}