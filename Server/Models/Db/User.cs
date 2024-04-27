using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models.Db
{
	public class User
	{
		public int Id { get; set; }
		public string Username { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string Password { get; set; } = null!;
		public bool IsOnline { get; set; }
		public DateTime LastActive { get; set; }
	}
}
