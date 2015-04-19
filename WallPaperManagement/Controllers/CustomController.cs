using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using WallPaperManagement.Common;
using WallPaperManagement.Models;

namespace WallPaperManagement.Controllers
{
    public class CustomController<T> : Controller  where T:CommonModel<T,long>,new ()
    {
        //
        // GET: /Custom/

        public ActionResult AjaxPage(int page = 1, int rows = 10)
        {
            T t =new T();
            WallPagerContext dataContext = new WallPagerContext();
            int amount = dataContext.Set<T>().Count(t.GetWhereCondition());  
            List<T> wallPapers =
                dataContext.Set<T>().Where(t.GetWhereCondition())  
                    .OrderByDescending<T,long>(t.GetSortCondition())
                    .Skip((page - 1) * rows)
                    .Take(rows)
                    .ToList();
            PageResult<T> wallPageResult = new PageResult<T>
            {
                rows = wallPapers,
                total = amount
            };
            var timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm" };
            string serializeObject1 = JsonConvert.SerializeObject(wallPageResult, timeConverter);
            var serializeObject = serializeObject1;
            return Content(serializeObject);
        }
     
    }
}