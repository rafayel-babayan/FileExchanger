namespace FileExchanger.Models
{
	public class UserGroup
	{
		public string Id { get; set; }
		public User User { get; set; }
		public string UserId { get; set; }
		public Group Group { get; set; }
		public string GroupId { get; set; }
	}
}
