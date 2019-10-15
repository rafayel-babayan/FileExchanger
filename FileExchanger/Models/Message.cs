using System;

namespace FileExchanger.Models
{
     
	public class Message
	{
        public string Id { get; set; }
        public DateTime SendDate { get; set; }
        public string Content { get; set; }
		public bool IsFile { get; set; }
		public string FilePath { get; set; }
		public string AuthorId { get; set; }
		public User Author { get; set; }
		public string GroupId { get; set; }
		public Group Group { get; set; }
	}
}
