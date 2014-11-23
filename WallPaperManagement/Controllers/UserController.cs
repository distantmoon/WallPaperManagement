using System.Data;
using System.Linq;
using System.Web.Mvc;
using WallPaperManagement.Models;

namespace WallPaperManagement.Controllers
{
    public class UserController : Controller
    {
        private WallPagerContext db = new WallPagerContext();

        //
        // GET: /User/
        [Authorize]
        public ViewResult Index()
        {
            return View(db.SystemUsers.ToList());
        }

        //
        // GET: /User/Details/5
        [Authorize]
        public ViewResult Details(int id)
        {
            SystemUser systemuser = db.SystemUsers.Find(id);
            return View(systemuser);
        }

        //
        // GET: /User/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(SystemUser systemuser)
        {
            if (ModelState.IsValid)
            {
                db.SystemUsers.Add(systemuser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(systemuser);
        }

        //
        // GET: /User/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            SystemUser systemuser = db.SystemUsers.Find(id);
            return View(systemuser);
        }

        //
        // POST: /User/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(SystemUser systemuser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(systemuser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(systemuser);
        }

        //
        // GET: /User/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            SystemUser systemuser = db.SystemUsers.Find(id);
            return View(systemuser);
        }

        //
        // POST: /User/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SystemUser systemuser = db.SystemUsers.Find(id);
            db.SystemUsers.Remove(systemuser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}