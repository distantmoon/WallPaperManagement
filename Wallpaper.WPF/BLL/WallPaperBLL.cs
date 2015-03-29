using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Wallpaper.WPF.Util;

namespace Wallpaper.WPF.BLL
{
    public class WallPaperBLL:BLLBase<WallPaper.DAL.WallPaper>
    {
        public override string GetPageUrl()
        {
            return "http://localhost:8099/WallPaper/GetPage";
        }
        public override string GetCountUrl()
        {
            return "http://localhost:8099/WallPaper/GetCount";
        } 
    }
}