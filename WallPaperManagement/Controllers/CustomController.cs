using System.Web.Mvc;
using WallPaperManagement.Common;
using WallPaperManagement.Models;

namespace WallPaperManagement.Controllers
{
    public class CustomController<T> : Controller  where T:CommonModel
    {
        //
        // GET: /Custom/

        public ActionResult AjaxPage(int page = 1, int rows = 10)
        {
            var serializeObject = DataUtil.GetPageJsonString<T>(page, rows, p => p.IsEnable == 1,p=>p.Id);
            return Content(serializeObject);
        }
    }
}