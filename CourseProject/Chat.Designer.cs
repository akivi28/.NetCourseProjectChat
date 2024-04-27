namespace CourseProject
{
	partial class Chat
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			usersPanel = new Panel();
			userInfoLabel = new Label();
			messageTextBox = new TextBox();
			sendButton = new Button();
			chatPanel = new Panel();
			timer1 = new System.Windows.Forms.Timer(components);
			openFileDialog1 = new OpenFileDialog();
			AddFile = new Button();
			folderBrowserDialog1 = new FolderBrowserDialog();
			SuspendLayout();
			// 
			// usersPanel
			// 
			usersPanel.AutoScroll = true;
			usersPanel.BackColor = SystemColors.InactiveCaption;
			usersPanel.Font = new Font("Arial", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
			usersPanel.Location = new Point(0, 0);
			usersPanel.Name = "usersPanel";
			usersPanel.Size = new Size(253, 450);
			usersPanel.TabIndex = 0;
			// 
			// userInfoLabel
			// 
			userInfoLabel.BackColor = SystemColors.ControlDarkDark;
			userInfoLabel.Font = new Font("Arial", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
			userInfoLabel.ForeColor = SystemColors.ButtonHighlight;
			userInfoLabel.Location = new Point(252, 0);
			userInfoLabel.Name = "userInfoLabel";
			userInfoLabel.Size = new Size(548, 58);
			userInfoLabel.TabIndex = 1;
			userInfoLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// messageTextBox
			// 
			messageTextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
			messageTextBox.Location = new Point(253, 397);
			messageTextBox.Multiline = true;
			messageTextBox.Name = "messageTextBox";
			messageTextBox.Size = new Size(381, 53);
			messageTextBox.TabIndex = 2;
			// 
			// sendButton
			// 
			sendButton.Font = new Font("Arial", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
			sendButton.Location = new Point(702, 397);
			sendButton.Name = "sendButton";
			sendButton.Size = new Size(98, 53);
			sendButton.TabIndex = 3;
			sendButton.Text = "Send";
			sendButton.UseVisualStyleBackColor = true;
			sendButton.Click += sendButton_Click;
			// 
			// chatPanel
			// 
			chatPanel.AutoScroll = true;
			chatPanel.Location = new Point(252, 61);
			chatPanel.Name = "chatPanel";
			chatPanel.Size = new Size(548, 330);
			chatPanel.TabIndex = 4;
			// 
			// timer1
			// 
			timer1.Interval = 1000;
			timer1.Tick += timer1_Tick;
			// 
			// openFileDialog1
			// 
			openFileDialog1.FileName = "openFileDialog1";
			openFileDialog1.FileOk += openFileDialog1_FileOk;
			// 
			// AddFile
			// 
			AddFile.Location = new Point(640, 402);
			AddFile.Name = "AddFile";
			AddFile.Size = new Size(56, 41);
			AddFile.TabIndex = 5;
			AddFile.Text = "Add File";
			AddFile.UseVisualStyleBackColor = true;
			AddFile.Click += AddFile_Click;
			// 
			// Chat
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(AddFile);
			Controls.Add(chatPanel);
			Controls.Add(sendButton);
			Controls.Add(messageTextBox);
			Controls.Add(userInfoLabel);
			Controls.Add(usersPanel);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			Name = "Chat";
			Text = "Chat";
			FormClosing += Chat_FormClosing;
			Load += Chat_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Panel usersPanel;
		private Label userInfoLabel;
		private TextBox messageTextBox;
		private Button sendButton;
		private Panel chatPanel;
		private System.Windows.Forms.Timer timer1;
		private OpenFileDialog openFileDialog1;
		private Button AddFile;
		private FolderBrowserDialog folderBrowserDialog1;
	}
}