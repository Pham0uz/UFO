using MahApps.Metro.Controls.Dialogs;
using swk5.ufo.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ufo.commander
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow
    {
        private ProgressDialogController _controller;
        private ICommanderBL commander = BLFactory.GetCommander();

        //public string generateRealPwd(string email, string pwd)
        //{
        //    using (SHA1Managed sha1 = new SHA1Managed())
        //    {
        //        string input = email + "|" + pwd;
        //        var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
        //        var sb = new StringBuilder(hash.Length * 2);

        //        foreach (byte b in hash)
        //        {
        //            sb.Append(b.ToString("x2"));
        //        }

        //        return sb.ToString();
        //    }
        //}

        public LoginWindow()
        {
            InitializeComponent();
        }
        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var email = txtEMail.Text;
            var pwd = txtPwd.Password;

            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(pwd))
            {
                txtError.Text = "Please enter a EMail and a Password!";
                return;
            }
            else if (string.IsNullOrEmpty(pwd))
            {
                txtError.Text = "Please enter a Password";
                return;
            }
            else if (string.IsNullOrEmpty(email))
            {
                txtError.Text = "Please enter a EMail";
                return;
            }
            txtError.Text = "";

            IsEnabled = false;
            _controller = await this.ShowProgressAsync("Logging in...", "Authenticating...");
            _controller.SetIndeterminate();

            var result = await Task.Factory.StartNew(() =>
            {
                return commander.AuthenticateUser(email, pwd);
            });

            txtPwd.Password = "";

            await _controller.CloseAsync();

            IsEnabled = true;
            if (result == true)
            {
                await this.ShowMessageAsync("Authenticated", "Login success!");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
            else
            {
                txtError.Text = "Combination of EMail and Password is incorrect!";
            }
            _controller.SetProgress(1);
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
