using System.Collections.Generic;

namespace FileExchanger.Models
{
	public class Group
	{
		public string Id { get; set; }
		public virtual ICollection<UserGroup> UserGroups { get; set; }
	}
}
