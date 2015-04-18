using System.Collections.Generic;

namespace WallPaperManagement.Models
{
    public class PageResult<T> where T : CommonModel
    {
        public int total { get; set; }
        public List<T> rows { get; set; }
    }
}