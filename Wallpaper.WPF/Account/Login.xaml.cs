using System;
using System.Windows;
using Wallpaper.WPF.Model;

namespace Wallpaper.WPF.Account
{
    /// <summary>
    ///     Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly User user = new User();

        
        public Login()
        {
            InitializeComponent();
            LoginPanel.DataContext = user;
            this.Password.PasswordChanged += delegate
            {
                user.Password = this.Password.Password;
            };
        }

        private void CheckPassword(object sender, RoutedEventArgs e)
        {
            if (user.UserName.Equals("admin") && user.Password.Equals("111111"))
            {
                
                MainWindow mainWindow=new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show(string.Format("UserName:{0};Password:{1}", user.UserName, user.Password));
            }
        }
    }
}