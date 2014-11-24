using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WallPaperManagement.Models;
using Webdiyer.WebControls.Mvc;

namespace WallPaperManagement.Controllers
{
    public class OrderController : Controller
    {
        private WallPagerContext db = new WallPagerContext();

        //
        // GET: /Order/
           [Authorize]
        public ViewResult Index(int id=1)
        {

            ViewBag.Total = db.OrderInfos.Sum(p => p.OrderCount);
            ViewBag.TotalCount = db.OrderInfos.Count();
            ViewBag.TotalPrice = db.OrderInfos.Sum(p => p.OrderCount * p.PriceDecimal);
            PagedList<OrderInfo> orders = db.OrderInfos.OrderByDescending(o => o.CreateDate).ToPagedList(id, 15);
            return View(orders);
        }

        //
        // GET: /Order/Details/5
           [Authorize]
        public ViewResult Details(int id)
        {
            OrderInfo orderinfo = db.OrderInfos.Find(id);
            return View(orderinfo);
        }

        //
        // GET: /Order/Create
        [Authorize]
        public ActionResult Create()
        {
            List<WallPaper> wallPapers = db.WallPapgers.ToList();
            ViewBag.WallPaperId = wallPapers.Select(p => new SelectListItem
            {
                Text = p.Name+":"+p.SeriesName+":"+p.No,
                Value = p.WallPaperId.ToString()
            
            }).ToList();
            List<ClientInfo> clientInfos = db.ClientInfos.ToList();
            ViewData.Add("ClientInfoId",clientInfos.Select(p=>new SelectListItem
            {
                Text = p.Name+":"+p.Mobile,
                Value = p.ClientInfoId.ToString()
            
            }).ToList());
            return View();
        }

        //
        // POST: /Order/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(OrderInfo orderinfo)
        {

            if (orderinfo.ClientInfoId==0 || orderinfo.WallPaperId==0 || orderinfo.OrderCount<=0 || orderinfo.PriceDecimal<=0)
            {
                ModelState.AddModelError("","信息填写错误,请重新填写");
                return View(orderinfo);
            }

            if (!ModelState.IsValid) return View(orderinfo);
            WallPaper wallPaper = db.WallPapgers.Find(orderinfo.WallPaperId);
            if (wallPaper == null) return View(orderinfo);
            if (wallPaper.Amount < orderinfo.OrderCount)
            {
                ModelState.AddModelError("", "库存量为:"+wallPaper.Amount+";订单需求为:"+orderinfo.OrderCount+"库存不足，订单无法创建");
                return View(orderinfo);
            }
            else
            {

                wallPaper.Amount =wallPaper.Amount-orderinfo.OrderCount;

                orderinfo.CreateDate = DateTime.Now;

                string userName = this.User.Identity.Name;
                SystemUser systemUser = db.SystemUsers.FirstOrDefault(p => p.UserName == userName);

                orderinfo.SystemUserId = systemUser.SystemUserId;

                db.OrderInfos.Add(orderinfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Order/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            OrderInfo orderinfo = db.OrderInfos.Find(id);
            return View(orderinfo);
        }

        //
        // POST: /Order/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(OrderInfo orderinfo)
        {

            OrderInfo orderInfoFind=db.OrderInfos.Find(orderinfo.OrderInfoId);
            if (orderinfo.PriceDecimal!=orderInfoFind.PriceDecimal)
            {
                orderInfoFind.PriceDecimal = orderinfo.PriceDecimal;
            }

            WallPaper wallPaper = db.WallPapgers.Find(orderInfoFind.WallPaperId);
            if (wallPaper.Amount+ orderInfoFind.OrderCount <orderinfo.OrderCount)
            {
                ModelState.AddModelError("","库存不足,修改失败");
                return View(orderinfo);
            }

            if (ModelState.IsValid)
            {
                wallPaper.Amount += orderInfoFind.OrderCount;
                wallPaper.Amount -= orderinfo.OrderCount;
                orderInfoFind.OrderCount = orderinfo.OrderCount;
                orderInfoFind.PayStatus = orderinfo.PayStatus;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderinfo);
        }

        //
        // GET: /Order/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            OrderInfo orderinfo = db.OrderInfos.Find(id);
            return View(orderinfo);
        }

        //
        // POST: /Order/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderInfo orderinfo = db.OrderInfos.Find(id);
            db.OrderInfos.Remove(orderinfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

         [Authorize]
        public ActionResult Search()
        {
            return View();
        }

       
        [Authorize]
        public ActionResult SearchByDate( DateTime beginDate, DateTime endDate,string seriesName,string no,string name)
        {
            beginDate = new DateTime(beginDate.Year, beginDate.Month, beginDate.Day, 0, 0, 0);
            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);
            List<OrderInfo> orderInfos = db.OrderInfos.Where(p => p.CreateDate > beginDate && p.CreateDate < endDate).ToList(); ;

            if (!string.IsNullOrEmpty(name) && string.IsNullOrEmpty(seriesName) &&　string.IsNullOrEmpty(no))
            {
                orderInfos = db.OrderInfos.Where(p => p.CreateDate > beginDate && p.CreateDate < endDate && p.WallPaper.Name.Contains(name)).ToList();
            }
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(seriesName) &&　string.IsNullOrEmpty(no))
            {
                orderInfos = db.OrderInfos.Where(p => p.CreateDate > beginDate && p.CreateDate < endDate && p.WallPaper.Name.Contains(name) && p.WallPaper.SeriesName.Contains(seriesName)).ToList();
            }
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(seriesName) &&　!string.IsNullOrEmpty(no))
            {
                orderInfos = db.OrderInfos.Where(p => p.CreateDate > beginDate && p.CreateDate < endDate && p.WallPaper.Name.Contains(name) && p.WallPaper.No.Contains(no)).ToList();
            }


            if (string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(seriesName) &&　string.IsNullOrEmpty(no))
            {
                orderInfos = db.OrderInfos.Where(p => p.CreateDate > beginDate && p.CreateDate < endDate && p.WallPaper.SeriesName.Contains(seriesName)).ToList();
            }
            


            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(seriesName) &&　!string.IsNullOrEmpty(no))
            {
                orderInfos = db.OrderInfos.Where(p => p.CreateDate > beginDate && p.CreateDate < endDate && p.WallPaper.No.Contains(no)).ToList();
            }
            if (string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(seriesName) &&　!string.IsNullOrEmpty(no))
            {
                orderInfos = db.OrderInfos.Where(p => p.CreateDate > beginDate && p.CreateDate < endDate && p.WallPaper.SeriesName.Contains(seriesName) && p.WallPaper.No.Contains(no)).ToList();
            }

            if (orderInfos.Count>0)
            {
                orderInfos.OrderBy(p => p.CreateDate);
            }

            
            return PartialView("_OrderList", orderInfos);
        }
    }
}