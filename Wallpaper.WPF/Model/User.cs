using System.ComponentModel;
using System.Runtime.CompilerServices;
using Wallpaper.WPF.Annotations;

namespace Wallpaper.WPF.Model
{
    public class User:INotifyPropertyChanged
    {
        private string userName;
        private string password;
        public User()
        {
            userName = string.Empty;
            password = string.Empty;
        }

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                this.OnPropertyChanged("UserName");
            }
        }


        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                this.OnPropertyChanged("Password");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}