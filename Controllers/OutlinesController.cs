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

namespace alsyedAcademy.Controllers
{
    public class OutlinesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Outlines
        public async Task<ActionResult> Index()
        {
            return View(await db.Outlines.ToListAsync());
        }

        // GET: Outlines/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outline outline = await db.Outlines.FindAsync(id);
            if (outline == null)
            {
                return HttpNotFound();
            }
            return View(outline);
        }

        // GET: Outlines/Create
        public ActionResult Create()
        {
            List<Course> courses = db.Courses.ToList();
            ViewBag.courses = courses;
            return View();
        }

        // POST: Outlines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,cid,description")] Outline outline)
        {
            if (ModelState.IsValid)
            {
                db.Outlines.Add(outline);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(outline);
        }

        // GET: Outlines/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outline outline = await db.Outlines.FindAsync(id);
            if (outline == null)
            {
                return HttpNotFound();
            }
            return View(outline);
        }

        // POST: Outlines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,cid,description")] Outline outline)
        {
            if (ModelState.IsValid)
            {
                db.Entry(outline).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(outline);
        }

        // GET: Outlines/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outline outline = await db.Outlines.FindAsync(id);
            if (outline == null)
            {
                return HttpNotFound();
            }
            return View(outline);
        }

        // POST: Outlines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Outline outline = await db.Outlines.FindAsync(id);
            db.Outlines.Remove(outline);
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
