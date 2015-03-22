using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Wallpaper.WPF.Annotations;

namespace Wallpaper.WPF.Model
{
    public class WallPaperViewModel : INotifyPropertyChanged
    {
       
        public MainWindow View { get; set; }

        private string _id;
        private string _model;
        private string _series;
        private DateTime _addTime;
        private string _number;
        private int _count;

        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        } 
        public string Model
        {
            get
            {
                return _model;
            }
            set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        public string Series
        {
            get
            {
                return _series;
            }
            set
            {
                _series = value;
                OnPropertyChanged();
            }
        }

        public DateTime AddTime
        {
            get
            {
                return _addTime;
            }
            set
            {
                _addTime = value;
                OnPropertyChanged();
            }
        }

        public string Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
                OnPropertyChanged();
            }
        }

        public int Amount
        {
            get
            {
                return _count;
            }
            set
            {
                _count = value;
                OnPropertyChanged();
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