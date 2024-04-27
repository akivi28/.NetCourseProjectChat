using Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CourseProject
{
	partial class Chat
	{
		private async Task getHistory()
		{
			try
			{
				using (TcpClient usingClient = new TcpClient("127.0.0.1", 8080))
				{
					using (NetworkStream usingStream = usingClient.GetStream())
					{
						try
						{

							Request request = new Request
							{
								Type = "getHistoryMessages",
								Data = $"{username} {selectedUser}"
							};

							string jsonRequest = JsonConvert.SerializeObject(request);

							byte[] requestData = Encoding.UTF8.GetBytes(jsonRequest);
							await usingStream.WriteAsync(requestData, 0, requestData.Length);

							List<byte> responseData = new List<byte>();
							byte[] buffer = new byte[1024];
							int bytesRead;

							while ((bytesRead = await usingStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
							{
								responseData.AddRange(buffer.Take(bytesRead));
							}

							byte[] responseDataArray = responseData.ToArray();
							string responseString = Encoding.UTF8.GetString(responseDataArray);

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
								List<MessageData>? messages = JsonConvert.DeserializeObject<List<MessageData>>(response.Data);

								if (messages == null) { return; }

								bool needToRefill = false;

								if (messages.Count > currentMessages.Count) { needToRefill = true; }

								if (!needToRefill)
								{
									for (int i = 0; i < messages.Count; i++)
									{
										if (!messages[i].Equals(currentMessages[i]))
										{
											needToRefill = true;
											break;
										}
									}
								}

								currentMessages = messages;

								if (needToRefill)
								{
									chatPanel.Controls.Clear();
									fillChat(currentMessages);
								}

							}
						}
						catch (Exception ex)
						{
							MessageBox.Show("get History   " + ex.Message);
						}
					}
				}
			}
			catch (SocketException ex)
			{
				selectedUser = "";
				DialogResult result = MessageBox.Show("Exit", "Server is not available ", MessageBoxButtons.YesNo);
				if (result == DialogResult.Yes)
				{
					Application.Exit();
				}
			}
		}

		private async Task getResiverData()
		{
			try
			{
				using (TcpClient usingClient = new TcpClient("127.0.0.1", 8080))
				{
					using (NetworkStream usingStream = usingClient.GetStream())
					{
						try
						{
							Request request = new Request
							{
								Type = "getResiverData",
								Data = $"{selectedUser}"
							};

							string jsonRequest = JsonConvert.SerializeObject(request);

							byte[] requestData = Encoding.UTF8.GetBytes(jsonRequest);
							await usingStream.WriteAsync(requestData, 0, requestData.Length);

							List<byte> responseData = new List<byte>();
							byte[] buffer = new byte[1024];
							int bytesRead;

							while ((bytesRead = await usingStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
							{
								responseData.AddRange(buffer.Take(bytesRead));
							}

							byte[] responseDataArray = responseData.ToArray();
							string responseString = Encoding.UTF8.GetString(responseDataArray);

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
								ResiverData? resiverData = JsonConvert.DeserializeObject<ResiverData>(response.Data);
								if (resiverData == null)
								{
									MessageBox.Show("No response from server");
									return;
								}
								else
								{
									userInfoLabel.Text = $"{resiverData.Username}";
									if (resiverData.IsOnline)
									{
										userInfoLabel.Text += " - online";
									}
									else
									{
										userInfoLabel.Text += $" - was online {resiverData.LastActive.Day}.{resiverData.LastActive.Month} {resiverData.LastActive.Hour}:{resiverData.LastActive.Minute}";
									}
								}
							}

						}
						catch (Exception ex)
						{
							MessageBox.Show("get resiver Data   " + ex.Message);
						}
					}
				}
			}
			catch (SocketException ex)
			{
				selectedUser = "";
				DialogResult result = MessageBox.Show("Exit", "Server is not available ", MessageBoxButtons.YesNo);
				if (result == DialogResult.Yes)
				{
					Application.Exit();
				}
			}
		}

		private void fillChat(List<MessageData> messages)
		{
			foreach (var message in messages)
			{

				TableLayoutPanel messagePanel = new TableLayoutPanel();
				messagePanel.AutoSize = true;
				messagePanel.MaximumSize = new Size(chatPanel.Width / 2, 0);
				messagePanel.Padding = new Padding(10);

				int x = 7;
				int y = 0;

				foreach (var item in chatPanel.Controls)
				{
					y += ((Control)item).Height + 10;
				}

				Label textLabel = new Label();
				textLabel.Text = $"{message.Text}";
				textLabel.AutoSize = true;
				textLabel.MaximumSize = new Size(chatPanel.Width / 2 - 10, 0);
				textLabel.Font = new Font("Arial", 12);
				textLabel.ForeColor = Color.White;
				messagePanel.SetRow(textLabel, 0);
				messagePanel.Controls.Add(textLabel);
				messagePanel.Width = textLabel.Width + 10;

				if (message.File != null)
				{
					LinkLabel fileLabel = new LinkLabel();
					fileLabel.Text = $"{message.File.FileName}";
					fileLabel.AutoSize = true;
					fileLabel.Font = new Font("Arial", 10);
					fileLabel.Padding = new Padding(0, 10, 0, 10);
					fileLabel.LinkColor = Color.White;
					fileLabel.ActiveLinkColor = Color.White;
					fileLabel.Name = message.File.Id.ToString();
					fileLabel.Click += new EventHandler(fileLabel_Click);
					messagePanel.SetRow(fileLabel, 1);
					messagePanel.Controls.Add(fileLabel);
					if (messagePanel.Width <= fileLabel.Width)
					{
						messagePanel.Width = fileLabel.Width + 10;
					}
				}

				Label timeLabel = new Label();
				timeLabel.Text = $"{message.Time.Hour}:{message.Time.Minute}";
				timeLabel.AutoSize = true;
				timeLabel.Font = new Font("Arial", 8);
				timeLabel.ForeColor = Color.White;
				messagePanel.SetRow(timeLabel, 2);
				messagePanel.Controls.Add(timeLabel);
				if (messagePanel.Width <= timeLabel.Width)
				{
					messagePanel.Width = timeLabel.Width + 10;
				}

				if (message.Sender == username)
				{
					messagePanel.BackColor = Color.FromArgb(0, 122, 204);
					timeLabel.Text += "  ✓";
					if (message.IsRead)
					{
						timeLabel.Text += "✓";
					}
				}
				else
				{
					messagePanel.BackColor = Color.Gray;
				}

				chatPanel.Controls.Add(messagePanel);
				if (message.Sender == username)
				{
					x = chatPanel.Width - messagePanel.Width - 20;
				}
				messagePanel.Location = new Point(x, y);
				messagePanel.Height = textLabel.Height + timeLabel.Height + 10;
			}
			chatPanel.AutoScrollPosition = new Point(0, chatPanel.VerticalScroll.Maximum);
		}
	}
}
