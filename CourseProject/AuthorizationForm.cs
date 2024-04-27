
using Lib;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;

namespace CourseProject;

public partial class AuthorizationForm : Form
{
	private TcpClient client;
	private NetworkStream stream;
	public AuthorizationForm()
	{
		InitializeComponent();
	}

	private async void registerButton_Click(object sender, EventArgs e)
	{
		if(registerEmailInput.Text == "" || 
			registerPasswordInput.Text == "" || 
			registerConfirmPasswordInput.Text == "" || 
			registerUserNameInput.Text == "")
		{
			registerErrorLabel.Text = "All fields must be filled";
			return;
		}
		if (registerEmailInput.Text.Contains(" ") ||
			registerPasswordInput.Text.Contains(" ") ||
			registerConfirmPasswordInput.Text.Contains(" ") ||
			registerUserNameInput.Text.Contains(" "))
		{
			registerErrorLabel.Text = "Fields cannot contain spaces";
			return;
		}


		try
		{
			registerErrorLabel.Text = "";
			client = new TcpClient("127.0.0.1", 8080);
			stream = client.GetStream();

			UserRegister userRegister = new UserRegister
			{
				Email = registerEmailInput.Text,
				Password = registerPasswordInput.Text,
				ConfirmPassword = registerConfirmPasswordInput.Text,
				Username = registerUserNameInput.Text
			};

			string userRegisterJson = JsonConvert.SerializeObject(userRegister);

			Request request = new Request
			{
				Type = "registration",
				Data = $"{userRegisterJson}"
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
				registerErrorLabel.Text = response.Data;
			}
			else
			{
				Chat chat = new Chat(response.Data);
				chat.Show();
				this.Hide();
			}
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
	}


	private async void loginButton_Click(object sender, EventArgs e)
	{
		if (loginEmailInput.Text == "" ||
						loginPasswordInput.Text == "")
		{
			loginErrorLabel.Text = "All fields must be filled";
			return;
		}
		if (loginEmailInput.Text.Contains(" ") ||
						loginPasswordInput.Text.Contains(" "))
		{
			loginErrorLabel.Text = "Fields cannot contain spaces";
			return;
		}
		try
		{
			loginErrorLabel.Text = "";
			client = new TcpClient("127.0.0.1", 8080);
			stream = client.GetStream();

			UserLogin userLogin = new UserLogin
			{
				Email = loginEmailInput.Text,
				Password = loginPasswordInput.Text
			};

			string userLoginJson = JsonConvert.SerializeObject(userLogin);

			Request request = new Request
			{
				Type = "login",
				Data = $"{userLoginJson}"
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
				loginErrorLabel.Text = response.Data;
			}
			else
			{
				Chat chat = new Chat(response.Data);
				chat.Show();
				this.Hide();
			}
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

	}
}