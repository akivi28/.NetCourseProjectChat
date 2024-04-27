using Lib;
using Newtonsoft.Json;
using Server.Models.Db;
using Server.Models;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography;

namespace Server
{
	partial class Program
	{
		static Response RegisterUser(string data)
		{
			var user = JsonConvert.DeserializeObject<UserRegister>(data);

            if (user == null){ return new Response { Status = "error", Data = "no data" };}

            try
			{
				var tmp = new MailAddress(user.Email);
				if (user.Password != user.ConfirmPassword)
				{
					return new Response { Status = "error", Data = "Passwords do not match" };
				}

				using (var db = new AppDbContext())
				{
					if (db.Users.Any(u => u.Email == user.Email))
					{
						return new Response { Status = "error", Data = "User with this email already exists" };
					}
					if (db.Users.Any(u => u.Username == user.Username))
					{
						return new Response { Status = "error", Data = "User with this username already exists" };
					}

					db.Users.Add(new User
					{
						Email = user.Email,
						Password = HashPassword(user.Password),
						Username = user.Username
					});
					db.SaveChanges();
					var currentUser = db.Users.First(u => u.Email == user.Email);
					return new Response { Status = "ok", Data = $"{currentUser.Username}" };
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("RegisterUser: " + e.Message);
				return new Response { Status = "error", Data = e.Message };
			}
		}

		static Response LoginUser(string data)
		{
			var user = JsonConvert.DeserializeObject<UserLogin>(data);

			if (user == null) { return new Response { Status = "error", Data = "no data" }; }

			try
			{
				using (var db = new AppDbContext())
				{
					var currentUser = db.Users.FirstOrDefault(u => u.Email == user.Email);
					if (currentUser == null)
					{
						return new Response { Status = "error", Data = "User with this email does not exist" };
					}
					if (currentUser.Password != HashPassword(user.Password))
					{
						return new Response { Status = "error", Data = "Incorrect password" };
					}
					return new Response { Status = "ok", Data = $"{currentUser.Username}" };
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("LoginUser: " + e.Message);
				return new Response { Status = "error", Data = e.Message };
			}
		}

		public static string HashPassword(string password)
		{
			using (var sha256 = SHA256.Create())
			{
				byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
				return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
			}
		}
	}
}
