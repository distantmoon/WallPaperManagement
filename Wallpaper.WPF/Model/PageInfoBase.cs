using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Composite.Presentation.Commands;
using Wallpaper.WPF.Annotations;
using Wallpaper.WPF.BLL;

namespace Wallpaper.WPF.Model
{
     public class PageInfoBase<T>:INotifyPropertyChanged
    {
        private List<T> _pageList; 
        private  BLLBase<T> _bll;
        private int _tatalRecord;
        private int _totalPage;
        private int _currentPage;
        private int _recordPerPage;

        public PageInfoBase(BLLBase<T> bllBase)
        {
            this._bll = bllBase;
            this.FirstCommand = new DelegateCommand<Object>(FirstPage, arg => true);
            this.LastCommand = new DelegateCommand<Object>(LastPage, arg => true);
            this.NextCommand = new DelegateCommand<Object>(NextPage, arg => true);
            this.PrevCommand = new DelegateCommand<Object>(PrevPage, arg => true);
            this.RecordPerPage = 23;
            PageList = _bll.GetPage(1,this.RecordPerPage);
            int count = _bll.GetCount();
            this.TotalPage = count/_recordPerPage + ((count%_recordPerPage==0)?0:1);
            this.CurrentPage = 1;
        }

        public List<T> PageList
        {
            get
            {
                return _pageList;
            }
            set
            {
                _pageList = value;
                OnPropertyChanged();
            }
        }
        public int TotalRecord
        {
            get
            {
                return _tatalRecord;
            }
            set
            {
                _tatalRecord =value;
                this.OnPropertyChanged();
            }
        }

        public int TotalPage
        {
            get
            {
               return  _totalPage;
            }
            set
            {
                _totalPage = value;
                this.OnPropertyChanged();
            }
        }

        public int CurrentPage
        {
            get
            {
                return  _currentPage;
            }
            set
            {
                _currentPage =value;
                this.OnPropertyChanged();
            }
        }

        public int RecordPerPage
        {
            get
            {
                return  _recordPerPage;
            }
            set
            {
                _recordPerPage =value;
                this.OnPropertyChanged();
            }
        }

        void NextPage(Object obj)
        {
            if (this.CurrentPage<this.TotalPage)
            {
                this.CurrentPage += 1;
                this.PageList = _bll.GetPage(this.CurrentPage, this.RecordPerPage);
            }
        }
        void PrevPage(Object obj)
        {
            if (this.CurrentPage>1)
            {
                this.CurrentPage -= 1;
                this.PageList = _bll.GetPage(this.CurrentPage, this.RecordPerPage);
            }
        } 
        void FirstPage(Object obj)
        {
                this.CurrentPage = 1;
                this.PageList = _bll.GetPage(this.CurrentPage, this.RecordPerPage);
        } 
        void LastPage(Object obj)
        {
                this.CurrentPage = this.TotalPage;
                this.PageList = _bll.GetPage(this.CurrentPage, this.RecordPerPage);
        }
        public ICommand NextCommand { get; set; }
        public ICommand PrevCommand { get; set; }
        public ICommand FirstCommand { get; set; }
        public ICommand LastCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
