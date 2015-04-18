using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Routing;
using Newtonsoft.Json;
using WallPaperManagement.Common;
using WallPaperManagement.Models;
using Webdiyer.WebControls.Mvc;

namespace WallPaperManagement.Controllers
{
    public class WallPaperController : CustomController<WallPaper>
    {
        private WallPagerContext db = new WallPagerContext();
        //
        // GET: /WallPager/
        [Authorize]
        public ActionResult Index(int id = 1)
        {
            ViewBag.Total = db.WallPapgers.Sum(p => p.Amount);
            return View();
        }

        public ActionResult NotEnough()
        {
            List<string> seriesList = db.WallPapgers.Where(p => p.Amount != 0).OrderByDescending(p => p.Amount).Take(100).Select(p=>p.SeriesName).ToList();
                
                //db.WallPapgers.Where(p => p.Amount != 0 && p.Amount < 20).Select(p => p.SeriesName).Distinct().ToList();
                                      ;
                                     

                
                //db.WallPapgers.Where(p => p.Amount != 0).Select(p => p.SeriesName).Distinct().ToList();
            Dictionary<string, int> current = new Dictionary<string, int>();
            foreach (string paperSeries in seriesList)
            {
                int sum = db.WallPapgers.Where(p => p.Amount != 0 && p.SeriesName == paperSeries).Sum(p => p.Amount);
                if (sum < 20)
                {
                    current.Add(paperSeries,sum);
                }
            }
            ViewBag.NotEnough = current;
            return View();
        }

        [Authorize]
        public ActionResult GetAllPaper()
        {
            return new JsonResult {Data = db.WallPapgers.ToList(), JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }

        //
        // GET: /WallPager/Details/5
        [Authorize]
        public ViewResult Details(int id)
        {
            WallPaper wallpapger = db.WallPapgers.Find(id);
            return View(wallpapger);
        }

        [Authorize]
        public ActionResult Add(int id, int addCount)
        {
            WallPaper wallPaper = db.WallPapgers.Find(id);
            wallPaper.Amount += addCount;
            wallPaper.AddDate = DateTime.Now;
            db.SaveChanges();
            return new JsonResult {Data = true};
        }

        //
        // GET: /WallPager/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /WallPager/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(WallPaper wallpaper)
        {
            wallpaper.AddDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.WallPapgers.Add(wallpaper);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wallpaper);
        }

        //
        // GET: /WallPager/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            WallPaper wallpapger = db.WallPapgers.Find(id);
            return View(wallpapger);
        }

        //
        // POST: /WallPager/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(WallPaper wallpaper)
        {
            wallpaper.AddDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(wallpaper).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wallpaper);
        }

        //
        // GET: /WallPager/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            WallPaper wallpapger = db.WallPapgers.Find(id);
            return View(wallpapger);
        }

        //
        // POST: /WallPager/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            WallPaper wallpapger = db.WallPapgers.Find(id);
            db.WallPapgers.Remove(wallpapger);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Search(string seriesName,string name,int id=1)
        {
            if (Request.IsAjaxRequest())
            {
                List<WallPaper> wallPapersAjax = db.WallPapgers.Where(p =>p.Amount!=0 && p.SeriesName.Contains(seriesName)).ToList();

                return PartialView("_WallPaperSearch", wallPapersAjax);
            }

            List<WallPaper> wallPapers;
            if (!string.IsNullOrEmpty(seriesName))
            {
                ViewBag.seriesName = seriesName;
                if (!string.IsNullOrEmpty(name))
                {
                    ViewBag.Name = name;
                    wallPapers = db.WallPapgers.Where(p =>p.Amount!=0 &&  p.Name.Contains(name) && p.SeriesName.Contains(seriesName)).ToList();
                }
                else
                {
                    wallPapers = db.WallPapgers.Where(p => p.Amount != 0 && p.SeriesName.Contains(seriesName)).ToList();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(name))
                {
                    ViewBag.Name = name;
                    wallPapers = db.WallPapgers.Where(p => p.Amount != 0 && p.Name.Contains(name)).ToList();
                }
                else
                {
                    wallPapers = db.WallPapgers.Where(p=>p.Amount!=0 ).ToList();
                }
            }
            
            ViewBag.Total=wallPapers.Sum(p => p.Amount);
            return View("Search", wallPapers);
        }
    }
}