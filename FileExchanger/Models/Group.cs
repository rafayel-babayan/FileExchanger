using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileExchanger.Models
{
	public class Group
	{
		public string Id { get; set; }
		public virtual ICollection<UserGroup> UserGroups { get; set; }
		public virtual ICollection<Message> Messages { get; set; }

		[NotMapped]
		public ICollection<User> Users => UserGroups.Select(o => o.User).ToList();

	}
}
