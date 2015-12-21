using swk5.ufo.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
        public string generateRealPwd(string email, string pwd)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                string input = email + "|" + pwd;
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach(byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public LoginWindow()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            ICommanderBL commander = BLFactory.GetCommander();

            string actualPwd = generateRealPwd(txtEMail.Text, txtPwd.Password);

            if (commander.AuthenticateUser(txtEMail.Text, actualPwd) == false)
            {
                txtError.Text = "Combination of EMail and password is incorrect!";
            }
            else
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            //else if (txtUserName.Text == "")
            //{
            //    txtUserNameError.Text = "User Name required!.";
            //    txtPwdError.Text = "";
            //}
            //else
            //{
            //    txtUserNameError.Text = "";
            //    txtPwdError.Text = "Wrong password";
            //}
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
