using Lib;
using Newtonsoft.Json;
using Server.Models;

namespace Server
{
	partial class Program
	{
		static Response GetUsers(string data)
		{
			List<string> users = new List<string>();

			try
			{
				using (var db = new AppDbContext())
				{
					var currentUser = db.Users.FirstOrDefault(u => u.Username == data);
					currentUser.IsOnline = true;
					db.SaveChanges();

					var allUsers = db.Users.ToList();
					foreach (var user in allUsers)
					{
						users.Add(user.Username);
					}
				}
				Response response = new Response
				{
					Status = "ok",
					Data = JsonConvert.SerializeObject(users)
				};
				return response;
			}
			catch (Exception ex)
			{
				Console.WriteLine("GetUsers: " + ex.Message);
				return new Response { Status = "error", Data = ex.Message };
			}
		}

		static Response UserExit(string data)
		{
			var username = data;
			try
			{
				using (var db = new AppDbContext())
				{
					var user = db.Users.FirstOrDefault(u => u.Username == username);

					if (user == null) { return new Response { Status = "error", Data = "User not found" }; }

					user.IsOnline = false;
					user.LastActive = DateTime.Now;
					db.SaveChanges();

					return new Response { Status = "ok", Data = "User exit" };
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("UserExit: " + ex.Message);
				return new Response { Status = "error", Data = ex.Message };
			}

		}

		static Response GetResiverData(string data)
		{
			try
			{
				using(var db = new AppDbContext())
				{
					var user = db.Users.FirstOrDefault(u => u.Username == data);

					if (user == null) { return new Response { Status = "error", Data = "User not found" }; };

					ResiverData resiverData = new ResiverData
					{
						Username = user.Username,
						Email = user.Email,
						IsOnline = user.IsOnline,
						LastActive = user.LastActive
					};
					return new Response { Status = "ok", Data = JsonConvert.SerializeObject(resiverData) };
				}
				
			}
			catch (Exception ex)
			{
				Console.WriteLine("GetResiverData: " + ex.Message);
				return new Response { Status = "error", Data = ex.Message };
			}
		}
	} 
}
