using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Wallpaper.WPF.Annotations;
using Microsoft.Practices.Composite.Presentation.Commands;
using Wallpaper.WPF.BLL;

namespace Wallpaper.WPF.Model
{
    public class WallPaperPageInfo:INotifyPropertyChanged
    {

        private List<WallPaper.DAL.WallPaper> _pageList; 
        private WallPaperBLL _wallPaperBll;
        private int _tatalRecord;
        private int _totalPage;
        private int _currentPage;
        private int _recordPerPage;

        public WallPaperPageInfo()
        {

            this.FirstCommand = new DelegateCommand<Object>(FirstPage, arg => true);
            this.LastCommand = new DelegateCommand<Object>(LastPage, arg => true);
            this.NextCommand = new DelegateCommand<Object>(NextPage, arg => true);
            this.PrevCommand = new DelegateCommand<Object>(PrevPage, arg => true);
            _wallPaperBll=new WallPaperBLL();
            PageList = _wallPaperBll.GetPage(1, 20);
            this.RecordPerPage = 20;
            this.TotalPage = _wallPaperBll.GetCount()/_recordPerPage + 1;
            this.CurrentPage = 1;
        }

        public List<WallPaper.DAL.WallPaper> PageList
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
                this.PageList = _wallPaperBll.GetPage(this.CurrentPage, this.RecordPerPage);
            }
        }
        void PrevPage(Object obj)
        {
            if (this.CurrentPage>1)
            {
                this.CurrentPage -= 1;
                this.PageList = _wallPaperBll.GetPage(this.CurrentPage, this.RecordPerPage);
            }
        } 
        void FirstPage(Object obj)
        {
                this.CurrentPage = 1;
                this.PageList = _wallPaperBll.GetPage(this.CurrentPage, this.RecordPerPage);
        } 
        void LastPage(Object obj)
        {
                this.CurrentPage = this.TotalPage;
                this.PageList = _wallPaperBll.GetPage(this.CurrentPage, this.RecordPerPage);
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