using System;
using System.Collections.Generic;
using Wallpaper.WPF.Util;

namespace Wallpaper.WPF.BLL
{
    public abstract class BLLBase<T>
    {
        public abstract string GetPageUrl();

        public abstract string GetCountUrl();

        public List<T> GetPage(int index, int recordPerPage)
        {
            Dictionary<string, string> nameValueCollection = new Dictionary<string, string>
            {
                {"index", Convert.ToString(index)},
                {"recordPerPage", Convert.ToString(recordPerPage)}
            };
            return WebUtil.GetResult<List<T>>(GetPageUrl(), nameValueCollection);
        }

        public int GetCount()
        {
            return WebUtil.GetResult<int>(GetCountUrl());
        }
    }
}