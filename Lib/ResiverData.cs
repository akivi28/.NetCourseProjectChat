using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
	public class ResiverData
	{
		public string Username { get; set; } = "";
		public string Email { get; set; } = "";
		public bool IsOnline { get; set; }
		public DateTime LastActive { get; set; }
	}
}
