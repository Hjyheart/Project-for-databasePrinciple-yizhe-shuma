using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TPDigital.Models;

namespace TPDigital.Controllers
{
    public class TP_USERController : Controller
    {
        private OracleDbContext db = new OracleDbContext();

        // GET: TP_USER
        public ActionResult Index()
        {
            var tP_USER = db.TP_USER.Include(t => t.TP_IMAGE).Include(t => t.TP_RECEIVER1);
            return View(tP_USER.ToList());
        }

        // GET: TP_USER/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TP_USER tP_USER = db.TP_USER.Find(id);
            if (tP_USER == null)
            {
                return HttpNotFound();
            }
            return View(tP_USER);
        }

        // GET: TP_USER/Create
        public ActionResult Create()
        {
            ViewBag.ICON_ID = new SelectList(db.TP_IMAGE, "ID", "URL");
            ViewBag.DEFAULT_RECEIVER_ID = new SelectList(db.TP_RECEIVER, "ID", "NAME");
            return View();
        }

        // POST: TP_USER/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NAME,SALT,PASSWORD,PHONE_NUMBER,EMAIL,ICON_ID,DEFAULT_RECEIVER_ID")] TP_USER tP_USER)
        {
            if (ModelState.IsValid)
            {
                db.TP_USER.Add(tP_USER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ICON_ID = new SelectList(db.TP_IMAGE, "ID", "URL", tP_USER.ICON_ID);
            ViewBag.DEFAULT_RECEIVER_ID = new SelectList(db.TP_RECEIVER, "ID", "NAME", tP_USER.DEFAULT_RECEIVER_ID);
            return View(tP_USER);
        }

        // GET: TP_USER/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TP_USER tP_USER = db.TP_USER.Find(id);
            if (tP_USER == null)
            {
                return HttpNotFound();
            }
            ViewBag.ICON_ID = new SelectList(db.TP_IMAGE, "ID", "URL", tP_USER.ICON_ID);
            ViewBag.DEFAULT_RECEIVER_ID = new SelectList(db.TP_RECEIVER, "ID", "NAME", tP_USER.DEFAULT_RECEIVER_ID);
            return View(tP_USER);
        }

        // POST: TP_USER/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NAME,SALT,PASSWORD,PHONE_NUMBER,EMAIL,ICON_ID,DEFAULT_RECEIVER_ID")] TP_USER tP_USER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tP_USER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ICON_ID = new SelectList(db.TP_IMAGE, "ID", "URL", tP_USER.ICON_ID);
            ViewBag.DEFAULT_RECEIVER_ID = new SelectList(db.TP_RECEIVER, "ID", "NAME", tP_USER.DEFAULT_RECEIVER_ID);
            return View(tP_USER);
        }

        // GET: TP_USER/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TP_USER tP_USER = db.TP_USER.Find(id);
            if (tP_USER == null)
            {
                return HttpNotFound();
            }
            return View(tP_USER);
        }

        // POST: TP_USER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TP_USER tP_USER = db.TP_USER.Find(id);
            db.TP_USER.Remove(tP_USER);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
