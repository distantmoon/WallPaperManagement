using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewWallPaper.Controllers
{
    public class CommonController<T> : Controller where T:WallPaper.DAL.IEntity
    {
        public virtual ActionResult GetPage(int index = 1, int sizePerPage = 20)
        {
            using (WallPaper.DAL.WallPaperDbContext dbContext = new WallPaper.DAL.WallPaperDbContext())
            {
                List<T> wallPapers = dbContext.Set<T>()
                      .OrderByDescending(p => p.AddTime)
                      .Skip((index - 1) * sizePerPage)
                      .Take(sizePerPage)
                      .ToList();
                return new JsonResult
                {
                    Data = wallPapers
                };
            }

        }

        public virtual ActionResult GetCount()
        {
            using (WallPaper.DAL.WallPaperDbContext dbContext = new WallPaper.DAL.WallPaperDbContext())
            {
                return new JsonResult
                {
                    Data = dbContext.Set<T>().Count()
                };
            }
        }
	}
}