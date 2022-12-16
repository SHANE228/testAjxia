using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using txtAjaxyy.Models;

namespace txtAjaxyy.Controllers
{
    public class staffsController : Controller
    {
        private staff db = new staff();

        // GET: staffs
        public ActionResult Index(int? i)
        {
            return View(db.staffs.ToList().ToPagedList(i ?? 1, 3));
        }
        public JsonResult CheckJobID(string jobID)
        {
            return Json(!db.staffs.Any(x => x.jobID.ToLower() == jobID.ToLower()), JsonRequestBehavior.AllowGet);
        }
        // GET: staffs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            staffs staffs = db.staffs.Find(id);
            if (staffs == null)
            {
                return HttpNotFound();
            }
            return View(staffs);
        }

        // GET: staffs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: staffs/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,jobID,department,state,position,name,email,level,sex,CrePerson,revPerson,dueDate,quitDate,SID,phone")] staffs staffs)
        {
            
            if (ModelState.IsValid)
            {
                db.staffs.Add(staffs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(staffs);
        }

        // GET: staffs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            staffs staffs = db.staffs.Find(id);
            if (staffs == null)
            {
                return HttpNotFound();
            }
            return View(staffs);
        }

        // POST: staffs/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,jobID,department,state,position,name,email,level,sex,CrePerson,revPerson,dueDate,quitDate,SID,phone")] staffs staffs)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(staffs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staffs);
        }

        // GET: staffs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            staffs staffs = db.staffs.Find(id);
            if (staffs == null)
            {
                return HttpNotFound();
            }
            return View(staffs);
        }

        // POST: staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            staffs staffs = db.staffs.Find(id);
            db.staffs.Remove(staffs);
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
