using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using alsyedAcademy.Models;
using System.Web.Helpers;
using System.IO;

namespace alsyedAcademy.Controllers
{
    public class FreeStudentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FreeStudents
        public async Task<ActionResult> Index()
        {
            return View(await db.FreeStudents.ToListAsync());
        }

        // GET: FreeStudents/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FreeStudents freeStudents = await db.FreeStudents.FindAsync(id);
            if (freeStudents == null)
            {
                return HttpNotFound();
            }
            return View(freeStudents);
        }

        // GET: FreeStudents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FreeStudents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,name,workingAt,photo,Designation")] FreeStudentsVM freeStudents)
        {
            string path = "";
            if (freeStudents.photo != null)
            {
                string InputFileName = Path.GetFileName(freeStudents.photo.FileName);
                WebImage img = new WebImage(freeStudents.photo.InputStream);
                int w = (int)(img.Width * .20);
                int h = (int)(img.Height * .20);
                img.Resize(w, h);
                string thumbsPath = System.IO.Path.Combine(Server.MapPath("~/images/freestudents/"), InputFileName);
                img.Save(thumbsPath);
                path = freeStudents.photo.FileName;
            }
            FreeStudents fs = new FreeStudents() { Designation = freeStudents.Designation, name = freeStudents.name, photo = path, workingAt = freeStudents.workingAt };
            if (ModelState.IsValid)
            {
                db.FreeStudents.Add(fs);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(freeStudents);
        }

        // GET: FreeStudents/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FreeStudents freeStudents = await db.FreeStudents.FindAsync(id);
            if (freeStudents == null)
            {
                return HttpNotFound();
            }
            return View(freeStudents);
        }

        // POST: FreeStudents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,name,workingAt,photo,Designation")] FreeStudents freeStudents)
        {
            if (ModelState.IsValid)
            {
                db.Entry(freeStudents).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(freeStudents);
        }

        // GET: FreeStudents/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FreeStudents freeStudents = await db.FreeStudents.FindAsync(id);
            if (freeStudents == null)
            {
                return HttpNotFound();
            }
            return View(freeStudents);
        }

        // POST: FreeStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FreeStudents freeStudents = await db.FreeStudents.FindAsync(id);
            try
            {
                string InputFileName = freeStudents.photo;//Path.GetFileName(nw.photo.FileName);
                string path = Path.Combine(Server.MapPath("~/images/freestudents/"), InputFileName);
                System.IO.File.Delete(path);
            }
            catch
            {

            }
            db.FreeStudents.Remove(freeStudents);
            await db.SaveChangesAsync();
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
