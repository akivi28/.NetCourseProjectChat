using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
	public class MessageData
	{
		public string Sender { get; set; } = "";
		public string Receiver { get; set; } = "";
		public string Text { get; set; } = "";
		public DateTime Time { get; set; } 
		public bool IsRead { get; set; }
		public FileData? File { get; set; }

		public override bool Equals(object? obj)
		{
			if (obj == null || GetType() != obj.GetType()) 
			{ 
				return false;
			}

			MessageData messageData = (MessageData)obj;
			return Sender == messageData.Sender && Receiver == messageData.Receiver && Text == messageData.Text && Time == messageData.Time && IsRead == messageData.IsRead;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Sender, Receiver, Text, Time, IsRead);
		}
	}
}
