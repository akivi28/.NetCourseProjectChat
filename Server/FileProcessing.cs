using Lib;
using Newtonsoft.Json;
using Server.Models;
using Server.Models.Db;
using System.Net.Sockets;
using System.Text;

namespace Server
{
	partial class Program
	{
		static async Task<Response> ReceiveFile(NetworkStream stream, string data)
		{
			int id = 0;
			try
			{
				FileData? fileData = JsonConvert.DeserializeObject<FileData>(data);

				if (fileData == null)
				{
					return new Response { Status = "error", Data = "Invalid data" };
				}

				string[] strings = fileData.FileName.Split('.');

				string guidName = Guid.NewGuid().ToString() + '.' + strings[strings.Length - 1];


				string filePath = Directory.GetCurrentDirectory() + "\\Files\\" + guidName ;

				using (FileStream fileStream = File.Create(filePath))
				{
					byte[] buffer = new byte[fileData.FileSize];
					int bytesRead;
					int totalBytesRead = 0;

					while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
					{
						fileStream.Write(buffer, 0, bytesRead);
						totalBytesRead += bytesRead;

						if (totalBytesRead >= fileData.FileSize)
						{
							break;
						}
					}
				}
				using (var db = new AppDbContext())
				{
					FileDb file = new FileDb
					{
						FileName = fileData.FileName,
						FilePath = filePath,
						GuidName = guidName
					};

					db.Files.Add(file);
					db.SaveChanges();
					id = file.Id;
				}
				return new Response { Status = "success", Data = $"{id}" };
			}
			catch (Exception e)
			{
				Console.WriteLine("ReceiveFile:  " + e.Message);
				return new Response { Status = "error", Data = e.Message };
			}
		}

		static async Task<Response> SendFile(NetworkStream stream, string data)
		{
			try
			{
				int id = int.Parse(data);

				using (var db = new AppDbContext())
				{
					var file = db.Files.Find(id);

					if (file == null)
					{
						return new Response { Status = "error", Data = "File not found" };
					}

					using (FileStream fileStream = File.OpenRead(file.FilePath))
					{
						byte[] buffer = new byte[1024];
						int bytesRead;
						int totalBytesRead = 0;

						while ((bytesRead = fileStream.Read(buffer,0,buffer.Length)) > 0)
						{
							await stream.WriteAsync(buffer, 0, bytesRead);
							totalBytesRead += bytesRead;
						}
					}
				}

				return new Response { Status = "success", Data = "File sent" };
			}
			catch (Exception ex)
			{
				Console.WriteLine("SendFile:  " + ex.Message);
				return new Response { Status = "error", Data = ex.Message };
			}

		}
	}
}
