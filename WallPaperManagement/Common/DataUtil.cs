using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;
using WallPaperManagement.Models;

namespace WallPaperManagement.Common
{
    public class DataUtil
    {
        public static string GetPageJsonString<T>(int page, int rows,
                    Expression<Func<T, bool>> whereCondition,
             Expression<Func<T, long>> keySelector) where T : CommonModel
        {
            WallPagerContext dataContext = new WallPagerContext();
            int amount = dataContext.Set<T>().Count(p => p.IsEnable ==1);      //    Count(p => p.IsEnable == 1);
            List<T> wallPapers =
                dataContext.Set<T>().Where(whereCondition)
                    .OrderByDescending(keySelector)
                    .Skip((page - 1) * rows)
                    .Take(rows)
                    .ToList();
            PageResult<T> wallPageResult = new PageResult<T>
            {
                rows = wallPapers,
                total = amount
            };
            var timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm" };
            string serializeObject = JsonConvert.SerializeObject(wallPageResult, timeConverter);
            return serializeObject;
        }

    }
}