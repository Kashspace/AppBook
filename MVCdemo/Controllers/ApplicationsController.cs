﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCdemo.Models;

namespace MVCdemo.Controllers
{
    public class ApplicationsController : Controller
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        // GET: Applications
        public ActionResult Index()
        {
            return View(db.Applications.ToList());
        }
            

        // GET: Applications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // GET: Applications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppId,AppName,AppOwner,AppDesc,DateCreated,DateModified")] Application application)
        {
            if (ModelState.IsValid)
            {
                using (ApplicationDBContext context=new ApplicationDBContext())
                {
                    application.DateCreated= DateTime.Now;
                    application.DateModified = DateTime.Now;
                    context.SaveChanges();
                }

                db.Applications.Add(application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(application);
        }

        // GET: Applications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppId,AppName,AppOwner,AppDesc,DateCreated,DateModified")] Application application)
        {
            if (ModelState.IsValid)
            {
                using (ApplicationDBContext context = new ApplicationDBContext())
                {
                    application.DateCreated = application.DateCreated;
                    application.DateModified = DateTime.Now;
                    context.SaveChanges();
                }
                db.Entry(application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(application);
        }

        // GET: Applications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Application application = db.Applications.Find(id);
            db.Applications.Remove(application);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Dev(int? id)
        {
            return RedirectToAction("Index", "Environments", new { app_id = id,env = "Dev"  });  
        }

        public ActionResult ITG(int? id)
        {
            return RedirectToAction("Index", "Environments", new { app_id = id, env = "ITG" });
        }
        public ActionResult Prod(int? id)
        {
            return RedirectToAction("Index", "Environments", new { app_id = id, env = "Prod" });
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
