
using Lib;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
	partial class Program
	{
		private static async Task Main(string[] args)
		{
			TcpListener server = null;

			try
			{
				IPAddress ip = IPAddress.Parse("127.0.0.1");
				server = new TcpListener(ip, 8080);
				server.Start();
				Console.WriteLine("Server started");

				while (true)
				{
					Console.WriteLine("Waiting for a connection...");
					TcpClient client = await server.AcceptTcpClientAsync();

					_ = HandleClient(client);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Main:  " + e.Message);
			}
			finally
			{
				server?.Stop();
			}
		}

		static async Task HandleClient(TcpClient client)
		{
			NetworkStream stream = client.GetStream();

			try
			{
				byte[] buffer = new byte[1024];
				int bytesRead;
				StringBuilder message = new StringBuilder();

				while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
				{
					message.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
					if (!stream.DataAvailable)
					{
						break;
					}
				}

				string json = message.ToString();
				Console.WriteLine("Received: " + json);

				string response = JsonConvert.SerializeObject(await ProcessRequest(json, stream));

				byte[] bytes = Encoding.UTF8.GetBytes(response);
				await stream.WriteAsync(bytes, 0, bytes.Length);
			}
			catch (Exception e)
			{
				Console.WriteLine("HandleClient: " + e.Message);
			}
			finally
			{
				client.Close();
			}
		}

		static async Task<Response> ProcessRequest(string json, NetworkStream stream = null)
		{
			try
			{
				Request? request = JsonConvert.DeserializeObject<Request>(json);

				if (request == null) { return new Response { Status = "error", Data = "no data" }; }

				switch (request.Type)
				{
					case "registration":
						return RegisterUser(request.Data);
					case "login":
						return LoginUser(request.Data);
					case "getUsers":
						return GetUsers(request.Data);
					case "sendMessage":
						return await SendMessage(request.Data, stream);
					case "getHistoryMessages":
						return GetMessagesHistory(request.Data);
					case "userExit":
						return UserExit(request.Data);
					case "getResiverData":
						return GetResiverData(request.Data);
					case "getFile":
						return await SendFile(stream, request.Data);
					default:
						return new Response { Status = "error", Data = "Unknown request type" };
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("ProcessRequest: " + e.Message);
				return new Response { Status = "error", Data = e.Message };
			}
		}
	}
}
