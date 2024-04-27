using System.ComponentModel;

namespace CourseProject;

partial class AuthorizationForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

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
		components = new Container();
		tabControl1 = new TabControl();
		tabPage1 = new TabPage();
		loginErrorLabel = new Label();
		loginButton = new Button();
		loginPasswordInput = new TextBox();
		label1 = new Label();
		loginEmailInput = new TextBox();
		LoginEmaiInput = new Label();
		tabPage2 = new TabPage();
		registerErrorLabel = new Label();
		registerUserNameInput = new TextBox();
		label5 = new Label();
		registerConfirmPasswordInput = new TextBox();
		label4 = new Label();
		registerButton = new Button();
		registerPasswordInput = new TextBox();
		label2 = new Label();
		registerEmailInput = new TextBox();
		label3 = new Label();
		toolTip1 = new ToolTip(components);
		tabControl1.SuspendLayout();
		tabPage1.SuspendLayout();
		tabPage2.SuspendLayout();
		SuspendLayout();
		// 
		// tabControl1
		// 
		tabControl1.Controls.Add(tabPage1);
		tabControl1.Controls.Add(tabPage2);
		tabControl1.Font = new Font("Arial", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
		tabControl1.Location = new Point(12, 5);
		tabControl1.Name = "tabControl1";
		tabControl1.SelectedIndex = 0;
		tabControl1.Size = new Size(360, 444);
		tabControl1.TabIndex = 0;
		// 
		// tabPage1
		// 
		tabPage1.Controls.Add(loginErrorLabel);
		tabPage1.Controls.Add(loginButton);
		tabPage1.Controls.Add(loginPasswordInput);
		tabPage1.Controls.Add(label1);
		tabPage1.Controls.Add(loginEmailInput);
		tabPage1.Controls.Add(LoginEmaiInput);
		tabPage1.Location = new Point(4, 33);
		tabPage1.Name = "tabPage1";
		tabPage1.Padding = new Padding(3);
		tabPage1.Size = new Size(352, 407);
		tabPage1.TabIndex = 0;
		tabPage1.Text = "Login";
		tabPage1.UseVisualStyleBackColor = true;
		// 
		// loginErrorLabel
		// 
		loginErrorLabel.AutoSize = true;
		loginErrorLabel.ForeColor = Color.Red;
		loginErrorLabel.Location = new Point(27, 204);
		loginErrorLabel.MaximumSize = new Size(300, 100);
		loginErrorLabel.Name = "loginErrorLabel";
		loginErrorLabel.Size = new Size(0, 24);
		loginErrorLabel.TabIndex = 5;
		// 
		// loginButton
		// 
		loginButton.Location = new Point(27, 319);
		loginButton.Name = "loginButton";
		loginButton.Size = new Size(304, 66);
		loginButton.TabIndex = 4;
		loginButton.Text = "Log in";
		loginButton.UseVisualStyleBackColor = true;
		loginButton.Click += loginButton_Click;
		// 
		// loginPasswordInput
		// 
		loginPasswordInput.Location = new Point(136, 78);
		loginPasswordInput.Name = "loginPasswordInput";
		loginPasswordInput.Size = new Size(195, 32);
		loginPasswordInput.TabIndex = 3;
		toolTip1.SetToolTip(loginPasswordInput, "must be at least 8 characters");
		// 
		// label1
		// 
		label1.AutoSize = true;
		label1.Location = new Point(27, 81);
		label1.Name = "label1";
		label1.Size = new Size(103, 24);
		label1.TabIndex = 2;
		label1.Text = "Password";
		// 
		// loginEmailInput
		// 
		loginEmailInput.Location = new Point(136, 28);
		loginEmailInput.Name = "loginEmailInput";
		loginEmailInput.Size = new Size(195, 32);
		loginEmailInput.TabIndex = 1;
		toolTip1.SetToolTip(loginEmailInput, "example: test@gmail.com");
		// 
		// LoginEmaiInput
		// 
		LoginEmaiInput.AutoSize = true;
		LoginEmaiInput.Location = new Point(36, 31);
		LoginEmaiInput.Name = "LoginEmaiInput";
		LoginEmaiInput.Size = new Size(61, 24);
		LoginEmaiInput.TabIndex = 0;
		LoginEmaiInput.Text = "Email";
		// 
		// tabPage2
		// 
		tabPage2.Controls.Add(registerErrorLabel);
		tabPage2.Controls.Add(registerUserNameInput);
		tabPage2.Controls.Add(label5);
		tabPage2.Controls.Add(registerConfirmPasswordInput);
		tabPage2.Controls.Add(label4);
		tabPage2.Controls.Add(registerButton);
		tabPage2.Controls.Add(registerPasswordInput);
		tabPage2.Controls.Add(label2);
		tabPage2.Controls.Add(registerEmailInput);
		tabPage2.Controls.Add(label3);
		tabPage2.Location = new Point(4, 33);
		tabPage2.Name = "tabPage2";
		tabPage2.Padding = new Padding(3);
		tabPage2.Size = new Size(352, 407);
		tabPage2.TabIndex = 1;
		tabPage2.Text = "Register";
		tabPage2.UseVisualStyleBackColor = true;
		// 
		// registerErrorLabel
		// 
		registerErrorLabel.AutoSize = true;
		registerErrorLabel.ForeColor = Color.Red;
		registerErrorLabel.Location = new Point(6, 234);
		registerErrorLabel.MaximumSize = new Size(300, 100);
		registerErrorLabel.Name = "registerErrorLabel";
		registerErrorLabel.Size = new Size(0, 24);
		registerErrorLabel.TabIndex = 14;
		// 
		// registerUserNameInput
		// 
		registerUserNameInput.Location = new Point(151, 195);
		registerUserNameInput.Name = "registerUserNameInput";
		registerUserNameInput.Size = new Size(195, 32);
		registerUserNameInput.TabIndex = 13;
		toolTip1.SetToolTip(registerUserNameInput, "exaple: user123");
		// 
		// label5
		// 
		label5.AutoEllipsis = true;
		label5.AutoSize = true;
		label5.Location = new Point(6, 198);
		label5.MinimumSize = new Size(100, 0);
		label5.Name = "label5";
		label5.Size = new Size(105, 24);
		label5.TabIndex = 12;
		label5.Text = "Username";
		// 
		// registerConfirmPasswordInput
		// 
		registerConfirmPasswordInput.Location = new Point(151, 130);
		registerConfirmPasswordInput.Name = "registerConfirmPasswordInput";
		registerConfirmPasswordInput.Size = new Size(195, 32);
		registerConfirmPasswordInput.TabIndex = 11;
		toolTip1.SetToolTip(registerConfirmPasswordInput, "must match the password");
		// 
		// label4
		// 
		label4.AutoEllipsis = true;
		label4.AutoSize = true;
		label4.Location = new Point(6, 114);
		label4.MinimumSize = new Size(100, 0);
		label4.Name = "label4";
		label4.Size = new Size(100, 48);
		label4.TabIndex = 10;
		label4.Text = "Confirm\r\npassword";
		// 
		// registerButton
		// 
		registerButton.Location = new Point(6, 316);
		registerButton.Name = "registerButton";
		registerButton.Size = new Size(340, 66);
		registerButton.TabIndex = 9;
		registerButton.Text = "Register";
		registerButton.UseVisualStyleBackColor = true;
		registerButton.Click += registerButton_Click;
		// 
		// registerPasswordInput
		// 
		registerPasswordInput.Location = new Point(151, 75);
		registerPasswordInput.Name = "registerPasswordInput";
		registerPasswordInput.Size = new Size(195, 32);
		registerPasswordInput.TabIndex = 8;
		toolTip1.SetToolTip(registerPasswordInput, "must be at least 8 characters");
		// 
		// label2
		// 
		label2.AutoSize = true;
		label2.Location = new Point(3, 75);
		label2.Name = "label2";
		label2.Size = new Size(103, 24);
		label2.TabIndex = 7;
		label2.Text = "Password";
		// 
		// registerEmailInput
		// 
		registerEmailInput.Location = new Point(151, 25);
		registerEmailInput.Name = "registerEmailInput";
		registerEmailInput.Size = new Size(195, 32);
		registerEmailInput.TabIndex = 6;
		toolTip1.SetToolTip(registerEmailInput, "example: test@gmail.com");
		// 
		// label3
		// 
		label3.AutoSize = true;
		label3.Location = new Point(3, 25);
		label3.Name = "label3";
		label3.Size = new Size(61, 24);
		label3.TabIndex = 5;
		label3.Text = "Email";
		// 
		// AuthorizationForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(384, 461);
		Controls.Add(tabControl1);
		Name = "AuthorizationForm";
		Text = "AuthorizationForm";
		tabControl1.ResumeLayout(false);
		tabPage1.ResumeLayout(false);
		tabPage1.PerformLayout();
		tabPage2.ResumeLayout(false);
		tabPage2.PerformLayout();
		ResumeLayout(false);
	}

	#endregion

	private TabControl tabControl1;
	private TabPage tabPage1;
	private TabPage tabPage2;
	private TextBox loginPasswordInput;
	private Label label1;
	private TextBox loginEmailInput;
	private Label LoginEmaiInput;
	private Button loginButton;
	private Button registerButton;
	private TextBox registerPasswordInput;
	private Label label2;
	private TextBox registerEmailInput;
	private Label label3;
	private TextBox registerConfirmPasswordInput;
	private Label label4;
	private TextBox registerUserNameInput;
	private Label label5;
	private Label loginErrorLabel;
	private Label registerErrorLabel;
	private ToolTip toolTip1;
}