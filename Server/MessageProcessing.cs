using Lib;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Server.Models;
using Server.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
	partial class Program
	{
		static async Task<Response> SendMessage(string data, NetworkStream stream)
		{
			var messageData = JsonConvert.DeserializeObject<MessageData>(data);

			if (messageData == null) { return new Response { Status = "error", Data = "no data" }; }

			int fileId = 0;

			try
			{
				if(messageData.File != null)
				{
					var fileData = await ReceiveFile(stream, JsonConvert.SerializeObject(messageData.File));
					if (fileData.Status == "error") { return fileData; }
					else
					{
						fileId = int.Parse(fileData.Data);
					}
				}

				using (var db = new AppDbContext())
				{
					Message message = new Message
					{
						Content = messageData.Text,
						CreatedAt = messageData.Time,
						SenderId = db.Users.First(u => u.Username == messageData.Sender).Id,
						ReceiverId = db.Users.First(u => u.Username == messageData.Receiver).Id,
						IsRead = false,
						File = db.Files.FirstOrDefault(f => f.Id == fileId)
					};

					db.Messages.Add(message);
					db.SaveChanges();

					return new Response { Status = "ok", Data = "Message sent" };
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("SendMessage: " + ex.Message);
				return new Response { Status = "error", Data = ex.Message };
			}
		}

		static Response GetMessagesHistory(string data)
		{
			string[] users = data.Split(' ');

			if (users.Length != 2) { return new Response { Status = "error", Data = "invalid data" }; }

			List<MessageData> messages = new List<MessageData>();
			try
			{
				using (var db = new AppDbContext())
				{
					var messagesDb = db.Messages
						.Include(m => m.Sender)
						.Include(m => m.Receiver)
						.Include(m => m.File)
						.Where(m => (m.Sender.Username == users[0] && m.Receiver.Username == users[1]) ||
															(m.Sender.Username == users[1] && m.Receiver.Username == users[0]))
						.OrderBy(m => m.CreatedAt)
						.ToList();
					foreach(var message in messagesDb)
					{
						if(message.Receiver.Username == users[0])
						{
							message.IsRead = true;
						}
					}
					db.SaveChanges();
					foreach (var message in messagesDb)
					{
						FileData? fileData = null;

						if(message.File != null)
						{
							FileInfo fileInfo = new FileInfo(message.File.FilePath);

							fileData = new FileData
							{
								Id = message.File.Id,
								FileName = message.File.FileName,
								FileSize = (int)fileInfo.Length
							};
						}

						messages.Add(new MessageData
						{
							Sender = message.Sender.Username,
							Receiver = message.Receiver.Username,
							Text = message.Content,
							Time = message.CreatedAt,
							IsRead = message.IsRead,
							File = fileData
						});
					}	
				}
				return new Response { Status = "ok", Data = $"{JsonConvert.SerializeObject(messages)}" };
			}
			catch (Exception ex)
			{
				Console.WriteLine("GetMessagesHistory: " + ex.Message);
				return new Response { Status = "error", Data = ex.Message };
			}
		}
	}
}
