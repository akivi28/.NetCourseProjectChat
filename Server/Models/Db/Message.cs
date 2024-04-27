using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models.Db
{
	public class Message
	{
		public int Id { get; set; }
		public string Content { get; set; } = null!;
		public DateTime CreatedAt { get; set; }
		public int SenderId { get; set; }
		public User Sender { get; set; } = null!;
		public int ReceiverId { get; set; }
		public User Receiver { get; set; } = null!;
		public bool IsRead { get; set; }
		public FileDb? File { get; set; }
	}
}
