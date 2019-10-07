using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FileExchanger.Models
{
	public class User : IdentityUser
	{
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }

		public bool IsConnected { get; set; }
		public virtual ICollection<Connection> Connections { get; set; }
		public virtual ICollection<UserGroup> UserGroups { get; set; }
	}
}
