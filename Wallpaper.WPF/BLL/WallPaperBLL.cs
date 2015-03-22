using System.Collections.Generic;
using System.Linq;
using WallPaper.DAL;

namespace Wallpaper.WPF.BLL
{
    public class WallPaperBLL
    {
        public List<WallPaper.DAL.WallPaper> GetPage(int index, int recordPerPage)
        {
            using (var db = new WallPaperString())
            {
                return db.Set<WallPaper.DAL.WallPaper>().OrderByDescending(p=>p.AddTime).Skip((index - 1)*recordPerPage).Take(20).ToList();
            }
        }

        public int GetCount()
        {
            using (var db = new WallPaperString())
            {
                return db.Set<WallPaper.DAL.WallPaper>().Count();
            }
        }
    }
}