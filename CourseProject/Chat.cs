using Lib;
using Newtonsoft.Json;
using System.Drawing;
using System.Net.Sockets;
using System.Security.Policy;
using System.Text;

namespace CourseProject
{
	public partial class Chat : Form
	{
		string username;
		TcpClient client;
		NetworkStream stream;
		string selectedUser;
		List<MessageData> currentMessages = new List<MessageData>();
		MessageData currentMessage = new MessageData();
		public Chat(string Username)
		{
			InitializeComponent();
			username = Username;
			Text = $"User - {username}";
			sendButton.Enabled = false;
			openFileDialog1.FileName = "";
		}

		private async void selectUserButton_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			selectedUser = button.Name;
			sendButton.Enabled = true;
			userInfoLabel.Text = $"{selectedUser}";
			chatPanel.Controls.Clear();
			await Task.WhenAll(getHistory(), getResiverData());
			timer1.Start();
		}

		private async void Chat_Load(object sender, EventArgs e)
		{
			try
			{
				client = new TcpClient("127.0.0.1", 8080);
				stream = client.GetStream();

				Request request = new Request
				{
					Type = "getUsers",
					Data = $"{username}"
				};

				string jsonRequest = JsonConvert.SerializeObject(request);

				byte[] data = Encoding.UTF8.GetBytes(jsonRequest);
				await stream.WriteAsync(data, 0, data.Length);

				data = new byte[1024];
				int bytesRead = await stream.ReadAsync(data, 0, data.Length);
				string responseString = Encoding.UTF8.GetString(data, 0, bytesRead);

				Response? response = JsonConvert.DeserializeObject<Response>(responseString);

				if (response == null)
				{
					MessageBox.Show("No response from server");
					return;
				}

				if (response.Status == "error")
				{
					MessageBox.Show(response.Data);
					return;
				}
				else
				{
					List<string>? users = JsonConvert.DeserializeObject<List<string>>(response.Data);

					if (users == null) { return; }

					foreach (var user in users)
					{
						if (user != username)
						{
							Button button = new Button();
							button.Name = user;
							button.Text = user;
							button.Width = usersPanel.Width - 25;
							button.Height = 50;
							button.BackColor = Color.White;
							button.FlatStyle = FlatStyle.Flat;
							button.FlatAppearance.BorderSize = 0;
							int x = 7;
							int y = usersPanel.Controls.Count * (button.Height + 10) + 10;
							button.Click += new EventHandler(selectUserButton_Click);

							button.Location = new Point(x, y);

							usersPanel.Controls.Add(button);
						}
					}
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				stream?.Close();
				client?.Close();
			}
		}

		private void Chat_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				client = new TcpClient("127.0.0.1", 8080);
				stream = client.GetStream();

				Request request = new Request
				{
					Type = "userExit",
					Data = $"{username}"
				};

				string jsonRequest = JsonConvert.SerializeObject(request);

				byte[] data = Encoding.UTF8.GetBytes(jsonRequest);
				stream.Write(data, 0, data.Length);

				data = new byte[1024];
				int bytesRead = stream.Read(data, 0, data.Length);
				string responseString = Encoding.UTF8.GetString(data, 0, bytesRead);

				Response? response = JsonConvert.DeserializeObject<Response>(responseString);

				if (response == null)
				{
					MessageBox.Show("No response from server");
					return;
				}
				if (response.Data == "error")
				{
					MessageBox.Show(response.Data);
					return;
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
			finally
			{
				stream?.Close();
				client?.Close();
			}
			Application.Exit();
		}

		private async void sendButton_Click(object sender, EventArgs e)
		{
			try
			{
				client = new TcpClient("127.0.0.1", 8080);
				stream = client.GetStream();

				currentMessage.Sender = username;
				currentMessage.Receiver = selectedUser;
				currentMessage.Text = messageTextBox.Text;
				currentMessage.Time = DateTime.Now;

				string jsonMessage = JsonConvert.SerializeObject(currentMessage);

				Request request = new Request
				{
					Type = "sendMessage",
					Data = $"{jsonMessage}"
				};

				string jsonRequest = JsonConvert.SerializeObject(request);

				byte[] data = Encoding.UTF8.GetBytes(jsonRequest);
				await stream.WriteAsync(data, 0, data.Length);

				if (currentMessage.File != null)
				{
					string path = openFileDialog1.FileName;

					using (FileStream fileStream = File.OpenRead(path))
					{
						byte[] buffer = new byte[1024];
						int fileBytesRead;
						int totalBytesRead = 0;

						while ((fileBytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
						{
							stream.Write(buffer, 0, fileBytesRead);
							totalBytesRead += fileBytesRead;
						}
					}
				}

				data = new byte[1024];
				int bytesRead = await stream.ReadAsync(data, 0, data.Length);
				string responseString = Encoding.UTF8.GetString(data, 0, bytesRead);

				Response? response = JsonConvert.DeserializeObject<Response>(responseString);

				if (response == null)
				{
					MessageBox.Show("No response from server");
					return;
				}

				if (response.Status == "error")
				{
					MessageBox.Show(response.Data);
				}
				messageTextBox.Text = "";
				openFileDialog1.FileName = "";
				AddFile.Text = "Add file";
				currentMessage.File = null;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.Message);
			}
			finally
			{
				stream?.Close();
				client?.Close();
			}
			await getHistory();
		}

		private async void timer1_Tick(object sender, EventArgs e)
		{
			if (selectedUser != "")
			{
				await Task.WhenAll(getHistory(), getResiverData());
			}
		}

		private void AddFile_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.FileName == "")
			{
				openFileDialog1.ShowDialog();
			}
			else
			{
				openFileDialog1.FileName = "";
				AddFile.Text = "Add file";
				currentMessage.File = null;
			}
		}

		private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (currentMessage.File == null)
			{
				currentMessage.File = new FileData();
			}

			FileInfo fileInfo = new FileInfo(openFileDialog1.FileName);
			string name = Path.GetFileName(openFileDialog1.FileName);

			currentMessage.File.FileName = name;
			currentMessage.File.FileSize = (int)fileInfo.Length;
			AddFile.Text = "Remove file";
		}

		private void fileLabel_Click(object? sender, EventArgs e)
		{ 
			DialogResult result = folderBrowserDialog1.ShowDialog();
			if (result == DialogResult.OK)
			{
				try
				{
					if (sender is LinkLabel linkLabel)
					{
						client = new TcpClient("127.0.0.1", 8080);
						stream = client.GetStream();

						Request request = new Request
						{
							Type = "getFile",
							Data = $"{linkLabel.Name}"
						};

						string jsonRequest = JsonConvert.SerializeObject(request);

						byte[] data = Encoding.UTF8.GetBytes(jsonRequest);
						stream.Write(data, 0, data.Length);

						FileData file = new FileData();

						foreach (var item in currentMessages)
						{
							if (item.File != null && item.File.Id == int.Parse(linkLabel.Name))
							{
								file = item.File;
							}
						}

						string path = folderBrowserDialog1.SelectedPath + "\\" + file.FileName;
						bool fileExists = File.Exists(path);
						int count = 0;

						if (fileExists)
						{
							string[] strings = file.FileName.Split('.');
							string name = strings[0];
							string extension = strings[1];

							while (fileExists)
							{
								count++;
								path = folderBrowserDialog1.SelectedPath + "\\" + name + $"({count})." + extension;
								fileExists = File.Exists(path);
							}

						}

						using(FileStream fileStream = File.Create(path))
						{
							byte[] buffer = new byte[file.FileSize];
							int fileBytesRead;
							int totalBytesRead = 0;

							while ((fileBytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
							{
								fileStream.Write(buffer, 0, fileBytesRead);
								totalBytesRead += fileBytesRead;

								if (totalBytesRead >= file.FileSize)
								{
									break;
								}
							}
						}

						data = new byte[1024];
						int bytesRead = stream.Read(data, 0, data.Length);
						string responseString = Encoding.UTF8.GetString(data, 0, bytesRead);

						Response? response = JsonConvert.DeserializeObject<Response>(responseString);

						if (response == null)
						{
							MessageBox.Show("No response from server");
							return;
						}

						if (response.Status == "error")
						{
							MessageBox.Show(response.Data);
							return;
						}

					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
				finally
				{
					client?.Close();
					stream?.Close();
				}
			}
		}
	}
}
