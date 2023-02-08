using EvoS.DirectoryServer.ARLauncher.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Steamworks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARLauncher
{
    public partial class MainForm : Form
    {
        enum State
        {
            LogIn,
            ResetPassword,
            CreateAccount
        }
        private State _state;
        public MainForm()
        {
            InitializeComponent();
        }

        static CancellationTokenSource CreateCts() => new(10000);

        private static string SteamTicketInvalid => "Provided Steam Ticket was not valid, cannot identify your SteamId";
        private static string SteamServersDown => "SteamWebApi seems to be down. Please, try again later (in a couple of hours they are usually back up)";
        private static string NoSteam => "Server is not configured to use SteamWebApi. You cannot use any features related to it.";

        private static string UsernameOrPasswordInvalid(string? details) => $"Username or password are invalid: {details}. Try resetting the password or creating a new account?";

        private static string SteamAccountAlreadyUsed => "Steam account was already linked to game account. Try resetting password";
        private static string SteamAccountNotFound => "Steam account is not found. Try creating account instead of resetting password";

        private async void BtnOk_Click(object sender, EventArgs e)
        {
            await (_state switch
            {
                State.CreateAccount => CreateAccountAsync(),
                State.ResetPassword => ResetPasswordAsync(),
                State.LogIn => LogInAsync(),
                _ => throw new Exception("unreachable")
            });
        }

        private async Task LogInAsync()
        {
            try
            {
                using var cts = CreateCts();
                var cancellationToken = cts.Token;
                var resp = await DirectoryServerConnector.Instance.GetAsync<LogInResponse>(new LauncherRequest()
                {
                    RequestType = LauncherRequest.LauncherRequestType.LogIn,
                    Username = tbLogin.Text,
                    Password = tbPassword.Text,
                }, cancellationToken);

                if (resp.ResponseType == LogInResponse.LogInResponseType.Success)
                {
                    LaunchGame();
                    return;
                }
                if (resp.ResponseType == LogInResponse.LogInResponseType.UsernameOrPasswordInvalid)
                {
                    ShowError(UsernameOrPasswordInvalid(resp.ErrorDescription));
                    return;
                }
                if (resp.ResponseType == LogInResponse.LogInResponseType.MustLinkExistingAccountToSteam)
                {
                    await LinkAccount();
                    return;
                }
                throw new Exception(resp.ErrorDescription);
            }
            catch (Exception ex)
            {
                ShowError($"Unexpected error: {ex.Message}");
            }
        }

        private async Task LinkAccount()
        {
            try
            {
                var userResponse = MessageBox.Show("Current game account is not linked to Steam, but server is configured to use SteamWebApi. To play the game, you must link your game account to Steam. " +
                        "Mind that Steam account cannot be linked more than once. Do you wish to link accounts?",
                        "Link to Steam", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (userResponse != DialogResult.Yes)
                {
                    return;
                }
                using var cts = CreateCts();
                var cancellationToken = cts.Token;
                var resp = await DirectoryServerConnector.Instance.GetAsync<LinkToSteamResponse>(new LauncherRequest()
                {
                    RequestType = LauncherRequest.LauncherRequestType.LinkExistingAccountToSteam,
                    Username = tbLogin.Text,
                    Password = tbPassword.Text,
                    SteamTicket = await SteamConnector.GetSteamTicketAsync()
                }, cancellationToken);

                if (resp.ResponseType == LinkToSteamResponse.LinkToSteamResponseType.Success)
                {
                    ShowSuccess("Accounts were successfully linked.");
                    LaunchGame();
                    return;
                }

                var s = resp.ResponseType switch
                {
                    LinkToSteamResponse.LinkToSteamResponseType.SteamTicketInvalid => SteamTicketInvalid,
                    LinkToSteamResponse.LinkToSteamResponseType.SteamAccountAlreadyUsed => SteamAccountAlreadyUsed,
                    LinkToSteamResponse.LinkToSteamResponseType.SteamServersDown => SteamServersDown,
                    LinkToSteamResponse.LinkToSteamResponseType.UsernameOrPasswordInvalid => UsernameOrPasswordInvalid(resp.ErrorDescription),
                    LinkToSteamResponse.LinkToSteamResponseType.NoSteam => NoSteam,
                    _ => throw new Exception(resp.ErrorDescription),
                };
                ShowError(s);
            }
            catch (Exception ex)
            {
                ShowError($"Unexpected error: {ex.Message}");
            }
        }

        private async Task CreateAccountAsync()
        {
            try
            {
                var userReponse = MessageBox.Show("Are you sure that you want to create an account with these Login and Password? Login cannot be changed. If the server uses SteamWebApi, then it will also be linked to your Steam account, " +
                    "and you will not be able to link your Steam account to a different game account", "Creating account", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (userReponse != DialogResult.Yes)
                {
                    return;
                }

                using var cts = CreateCts();
                var cancellationToken = cts.Token;
                var resp = await DirectoryServerConnector.Instance.GetAsync<CreateAccountResponse>(new LauncherRequest()
                {
                    RequestType = LauncherRequest.LauncherRequestType.CreateAccount,
                    Username = tbLogin.Text,
                    Password = tbPassword.Text,
                    SteamTicket = await SteamConnector.GetSteamTicketAsync()
                }, cancellationToken);

                if (resp.ResponseType == CreateAccountResponse.CreateAccountResponseType.Success)
                {
                    ShowSuccess("Acount was created!");
                    LaunchGame();
                    return;
                }

                var s = resp.ResponseType switch
                {
                    CreateAccountResponse.CreateAccountResponseType.UsernameWasAlreadyUsed => "This username was already used for another account. Try logging in or using a different username.",
                    CreateAccountResponse.CreateAccountResponseType.UsernameOrPasswordProhibited => $"Username or password are prohibited: {resp.ErrorDescription}.",
                    CreateAccountResponse.CreateAccountResponseType.SteamTicketInvalid => SteamTicketInvalid,
                    CreateAccountResponse.CreateAccountResponseType.SteamAccountAlreadyUsed => SteamAccountAlreadyUsed,
                    CreateAccountResponse.CreateAccountResponseType.SteamServersDown => SteamServersDown,
                    _ => throw new Exception(resp.ErrorDescription),
                };
                ShowError(s);
            }
            catch (Exception ex)
            {
                ShowError($"Unexpected error: {ex.Message}");
            }
        }

        private async Task ResetPasswordAsync()
        {
            try
            {
                using var cts = CreateCts();
                var cancellationToken = cts.Token;
                var resp = await DirectoryServerConnector.Instance.GetAsync<ResetPasswordResponse>(new LauncherRequest()
                {
                    RequestType = LauncherRequest.LauncherRequestType.ResetPassword,
                    Password = tbPassword.Text,
                    SteamTicket = await SteamConnector.GetSteamTicketAsync()
                }, cancellationToken);

                if (resp.ResponseType == ResetPasswordResponse.ResetPasswordResponseType.Success)
                {
                    ShowSuccess("Password was reset!");
                    LaunchGame();
                    return;
                }

                var s = resp.ResponseType switch
                {
                    ResetPasswordResponse.ResetPasswordResponseType.PasswordProhibited => "Password is prohibited. Try using a different one, please.",
                    ResetPasswordResponse.ResetPasswordResponseType.SteamTicketInvalid => SteamTicketInvalid,
                    ResetPasswordResponse.ResetPasswordResponseType.SteamServersDown => SteamServersDown,
                    ResetPasswordResponse.ResetPasswordResponseType.AccountNotFound => SteamAccountNotFound,
                    ResetPasswordResponse.ResetPasswordResponseType.NoSteam => NoSteam,
                    _ => throw new Exception(resp.ErrorDescription),
                };
                ShowError(s);
            }
            catch (Exception ex)
            {
                ShowError($"Unexpected error: {ex.Message}");
            }
        }

        private void SetState(State state)
        {
            _state = state;
            lblCaption.Text = state switch
            {
                State.LogIn => "Log In",
                State.ResetPassword => "Reset Password",
                State.CreateAccount => "Create Account",
                _ => throw new Exception("unreachable")
            };
            tbLogin.ReadOnly = state == State.ResetPassword;
            lblCreateAccount.Visible = state == State.LogIn;
            lblForgotPassword.Visible = state == State.LogIn;
            lblBackToLogIn.Visible = state != State.LogIn;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var pwdToolTip = new ToolTip
            {
                AutoPopDelay = 0,
                InitialDelay = 1000,
                ShowAlways = true
            };
            pwdToolTip.SetToolTip(lblPassword, "Mind that login and password are stored in plain text in .json file nearby");            
            tlpMainPanel.RowStyles[tlpMainPanel.GetCellPosition(lblBackToLogIn).Row].SizeType = SizeType.AutoSize;
            SetState(State.LogIn);
            SteamClient.Init(480);
            ReadLoginPassword();
        }

        private async void LblForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                using var cts = CreateCts();
                var cancellationToken = cts.Token;                
                var resp = await DirectoryServerConnector.Instance.GetAsync<RemindUsernameResponse>(new LauncherRequest()
                {
                    RequestType = LauncherRequest.LauncherRequestType.RemindUsername,
                    SteamTicket = await SteamConnector.GetSteamTicketAsync()
                }, cts.Token);
                if (resp.ResponseType == RemindUsernameResponse.RemindUsernameResponseType.Success)
                {
                    tbLogin.Text = resp.Username;
                    SetState(State.ResetPassword);
                }
                else
                {
                    var s = resp.ResponseType switch
                    {
                        RemindUsernameResponse.RemindUsernameResponseType.SteamTicketInvalid => SteamTicketInvalid,
                        RemindUsernameResponse.RemindUsernameResponseType.SteamAccountNotUsed => SteamAccountNotFound,
                        RemindUsernameResponse.RemindUsernameResponseType.SteamServersDown => SteamServersDown,
                        RemindUsernameResponse.RemindUsernameResponseType.NoSteam => NoSteam,
                        _ => throw new Exception(resp.ErrorDescription)
                    };
                    ShowError(s);
                }
                
            }
            catch (Exception ex)
            {
                ShowError($"Unexpected error: {ex.Message}");
            }
        }

        

        private void LaunchGame()
        {            
            try
            {
                WriteLoginPassword();
            }
            catch (Exception ex)
            {
                ShowError($"Failed to write Login/Password into config file. Launcher will be closed. Exception was: {ex.Message}");
                Close();
                return;
            }
            try
            {
                Process.Start(Path.Combine(Environment.CurrentDirectory, "..", "Win64", "AtlasReactor.exe"));
            }
            catch (Exception ex)
            {
                ShowError($"Could not start the game: {ex.Message}. Are you sure that ARLauncher is placed in Config folder? Try launching the game manually.");
            }
            Close();
        }

        private void ReadLoginPassword()
        {
            try
            {
                var jobj = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(Program.JsonConfigFullPath));
                if (jobj == null)
                    throw new Exception("Deserialized null JObject");
                tbLogin.Text = jobj["PlatformUserName"]?.Value<string?>();
                tbPassword.Text = jobj["PlatformPassword"]?.Value<string?>();
            }
            catch (Exception ex)
            {
                ShowError($"Failed to read Login and Password from {Program.JsonConfigFullPath}. Make sure that launcher is located in Config folder. Exception was: {ex.Message}");
            }
        }

        private void WriteLoginPassword()
        {

            var jobj = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(Program.JsonConfigFullPath));
            if (jobj == null)
                throw new Exception("Deserialized null JObject");
            jobj["PlatformUserName"] = tbLogin.Text;
            jobj["PlatformPassword"] = tbPassword.Text;

            File.WriteAllText(Program.JsonConfigFullPath, JsonConvert.SerializeObject(jobj, Formatting.Indented));
        }

        private static void ShowError(string? text) => MessageBox.Show(text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        private static void ShowSuccess(string? text) => MessageBox.Show(text, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void LblCreateAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => SetState(State.CreateAccount);

        private void LblBackToLogIn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => SetState(State.LogIn);
    }
}
