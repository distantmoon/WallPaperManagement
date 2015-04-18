using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using WallPaperManagement.Models;
using Webdiyer.WebControls.Mvc;

namespace WallPaperManagement.Controllers
{
    public class ClientController : CustomController<ClientInfo>
    {
        private WallPagerContext db = new WallPagerContext();

        //
        // GET: /Client/

        [Authorize]
        public ViewResult Index(int id=1)
        {
            return View();
        }

        //
        // GET: /Client/Details/5
        [Authorize]
        public ViewResult Details(int id)
        {
            ClientInfo clientinfo = db.ClientInfos.Find(id);
            return View(clientinfo.OrderInfos);
        }



         [Authorize]
        public ActionResult SearchByDate(int clientInfoId,DateTime beginDate, DateTime endDate,string name,string seriesName,string no)
         {
             beginDate = new DateTime(beginDate.Year, beginDate.Month, beginDate.Day, 0, 0, 0);
             endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);
             List<OrderInfo> orderInfos = db.OrderInfos.Where(p => p.CreateDate > beginDate && p.CreateDate < endDate).ToList(); ;

             if (!string.IsNullOrEmpty(name) && string.IsNullOrEmpty(seriesName) && string.IsNullOrEmpty(no))
             {
                 orderInfos = db.OrderInfos.Where(p => p.CreateDate > beginDate && p.CreateDate < endDate && p.WallPaper.Name.Contains(name)).ToList();
             }
             if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(seriesName) && string.IsNullOrEmpty(no))
             {
                 orderInfos = db.OrderInfos.Where(p => p.CreateDate > beginDate && p.CreateDate < endDate && p.WallPaper.Name.Contains(name) && p.WallPaper.SeriesName.Contains(seriesName)).ToList();
             }
             if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(seriesName) && !string.IsNullOrEmpty(no))
             {
                 orderInfos = db.OrderInfos.Where(p => p.CreateDate > beginDate && p.CreateDate < endDate && p.WallPaper.Name.Contains(name) && p.WallPaper.No.Contains(no)).ToList();
             }


             if (string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(seriesName) && string.IsNullOrEmpty(no))
             {
                 orderInfos = db.OrderInfos.Where(p => p.CreateDate > beginDate && p.CreateDate < endDate && p.WallPaper.SeriesName.Contains(seriesName)).ToList();
             }



             if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(seriesName) && !string.IsNullOrEmpty(no))
             {
                 orderInfos = db.OrderInfos.Where(p => p.CreateDate > beginDate && p.CreateDate < endDate && p.WallPaper.No.Contains(no)).ToList();
             }
             if (string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(seriesName) && !string.IsNullOrEmpty(no))
             {
                 orderInfos = db.OrderInfos.Where(p => p.CreateDate > beginDate && p.CreateDate < endDate && p.WallPaper.SeriesName.Contains(seriesName) && p.WallPaper.No.Contains(no)).ToList();
             }

             

             if (orderInfos.Count > 0)
             {
                 orderInfos = orderInfos.FindAll(p => p.ClientInfoId == clientInfoId);
                 orderInfos.OrderBy(p => p.CreateDate);
             }
             return PartialView("_OrderList", orderInfos);
        }
        //
        // GET: /Client/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Client/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(ClientInfo clientinfo)
        {

            clientinfo.AddDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.ClientInfos.Add(clientinfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clientinfo);
        }

        //
        // GET: /Client/Edit/5
       [Authorize]
        public ActionResult Edit(int id)
        {
            ClientInfo clientinfo = db.ClientInfos.Find(id);
            return View(clientinfo);
        }

        //
        // POST: /Client/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(ClientInfo clientinfo)
        {
            clientinfo.AddDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(clientinfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clientinfo);
        }

        //
        // GET: /Client/Delete/5
         [Authorize]
        public ActionResult Delete(int id)
        {
            ClientInfo clientinfo = db.ClientInfos.Find(id);
            return View(clientinfo);
        }

        //
        // POST: /Client/Delete/5
           [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ClientInfo clientinfo = db.ClientInfos.Find(id);
            db.ClientInfos.Remove(clientinfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
           [Authorize]
        public ActionResult GetAllClient()
           {
               var clientInfos = db.ClientInfos.Select(p=>new {Name=p.Name,Mobile=p.Mobile,ClientInfoId=p.Id}).ToList();
               return new JsonResult {Data = clientInfos, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
           }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Search(string clientName)
        {
            List<ClientInfo> clientInfos = db.ClientInfos.Where(p => p.Name.Contains(clientName)).ToList();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ClientList", clientInfos);
            }
            ViewBag.clientName = clientName;
            return View(clientInfos);

        }
    }
}