namespace ARLauncher
{
    partial class MainForm
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
            this.tlpMainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblCaption = new System.Windows.Forms.Label();
            this.lblLogin = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblForgotPassword = new System.Windows.Forms.LinkLabel();
            this.lblCreateAccount = new System.Windows.Forms.LinkLabel();
            this.tbLogin = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.lblBackToLogIn = new System.Windows.Forms.LinkLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.tlpMainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMainPanel
            // 
            this.tlpMainPanel.ColumnCount = 2;
            this.tlpMainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainPanel.Controls.Add(this.lblCaption, 0, 0);
            this.tlpMainPanel.Controls.Add(this.lblLogin, 0, 1);
            this.tlpMainPanel.Controls.Add(this.lblPassword, 0, 2);
            this.tlpMainPanel.Controls.Add(this.lblForgotPassword, 0, 3);
            this.tlpMainPanel.Controls.Add(this.lblCreateAccount, 0, 4);
            this.tlpMainPanel.Controls.Add(this.tbLogin, 1, 1);
            this.tlpMainPanel.Controls.Add(this.tbPassword, 1, 2);
            this.tlpMainPanel.Controls.Add(this.lblBackToLogIn, 0, 5);
            this.tlpMainPanel.Controls.Add(this.button1, 0, 6);
            this.tlpMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainPanel.Location = new System.Drawing.Point(0, 0);
            this.tlpMainPanel.Name = "tlpMainPanel";
            this.tlpMainPanel.RowCount = 8;
            this.tlpMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainPanel.Size = new System.Drawing.Size(417, 297);
            this.tlpMainPanel.TabIndex = 0;
            // 
            // lblCaption
            // 
            this.lblCaption.AutoSize = true;
            this.tlpMainPanel.SetColumnSpan(this.lblCaption, 2);
            this.lblCaption.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCaption.Location = new System.Drawing.Point(16, 16);
            this.lblCaption.Margin = new System.Windows.Forms.Padding(16);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(145, 31);
            this.lblCaption.TabIndex = 0;
            this.lblCaption.Text = "Caption here";
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.Location = new System.Drawing.Point(8, 71);
            this.lblLogin.Margin = new System.Windows.Forms.Padding(8);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(46, 20);
            this.lblLogin.TabIndex = 0;
            this.lblLogin.Text = "Login";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(8, 114);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(8);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(70, 20);
            this.lblPassword.TabIndex = 0;
            this.lblPassword.Text = "Password";
            // 
            // lblForgotPassword
            // 
            this.lblForgotPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblForgotPassword.AutoSize = true;
            this.tlpMainPanel.SetColumnSpan(this.lblForgotPassword, 2);
            this.lblForgotPassword.Location = new System.Drawing.Point(117, 157);
            this.lblForgotPassword.Margin = new System.Windows.Forms.Padding(8);
            this.lblForgotPassword.Name = "lblForgotPassword";
            this.lblForgotPassword.Size = new System.Drawing.Size(183, 20);
            this.lblForgotPassword.TabIndex = 1;
            this.lblForgotPassword.TabStop = true;
            this.lblForgotPassword.Text = "Forgot login or password?";
            // 
            // lblCreateAccount
            // 
            this.lblCreateAccount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCreateAccount.AutoSize = true;
            this.tlpMainPanel.SetColumnSpan(this.lblCreateAccount, 2);
            this.lblCreateAccount.Location = new System.Drawing.Point(144, 193);
            this.lblCreateAccount.Margin = new System.Windows.Forms.Padding(8);
            this.lblCreateAccount.Name = "lblCreateAccount";
            this.lblCreateAccount.Size = new System.Drawing.Size(128, 20);
            this.lblCreateAccount.TabIndex = 1;
            this.lblCreateAccount.TabStop = true;
            this.lblCreateAccount.Text = "Create an account";
            // 
            // tbLogin
            // 
            this.tbLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLogin.Location = new System.Drawing.Point(94, 71);
            this.tbLogin.Margin = new System.Windows.Forms.Padding(8);
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Size = new System.Drawing.Size(315, 27);
            this.tbLogin.TabIndex = 3;
            // 
            // tbPassword
            // 
            this.tbPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPassword.Location = new System.Drawing.Point(94, 114);
            this.tbPassword.Margin = new System.Windows.Forms.Padding(8);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(315, 27);
            this.tbPassword.TabIndex = 3;
            // 
            // lblBackToLogIn
            // 
            this.lblBackToLogIn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblBackToLogIn.AutoSize = true;
            this.tlpMainPanel.SetColumnSpan(this.lblBackToLogIn, 2);
            this.lblBackToLogIn.Location = new System.Drawing.Point(138, 229);
            this.lblBackToLogIn.Margin = new System.Windows.Forms.Padding(8);
            this.lblBackToLogIn.Name = "lblBackToLogIn";
            this.lblBackToLogIn.Size = new System.Drawing.Size(141, 20);
            this.lblBackToLogIn.TabIndex = 1;
            this.lblBackToLogIn.TabStop = true;
            this.lblBackToLogIn.Text = "Back to Log In page";
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tlpMainPanel.SetColumnSpan(this.button1, 2);
            this.button1.Location = new System.Drawing.Point(147, 260);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 29);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 297);
            this.Controls.Add(this.tlpMainPanel);
            this.Name = "MainForm";
            this.Text = "ARLauncher";
            this.tlpMainPanel.ResumeLayout(false);
            this.tlpMainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tlpMainPanel;
        private Label lblCaption;
        private Label lblLogin;
        private Label lblPassword;
        private LinkLabel lblForgotPassword;
        private LinkLabel lblCreateAccount;
        private Button button1;
        private TextBox tbLogin;
        private TextBox tbPassword;
        private LinkLabel lblBackToLogIn;
    }
}