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
    public class TutorialsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tutorials
        public async Task<ActionResult> Index()
        {
            return View(await db.Tutorials.ToListAsync());
        }

        // GET: Tutorials/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tutorial tutorial = await db.Tutorials.FindAsync(id);
            if (tutorial == null)
            {
                return HttpNotFound();
            }
            return View(tutorial);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CommentsCreate([Bind(Include = "id,tId,uId,text,dated")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        // GET: Tutorials/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tutorials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,Title,link,description,tags")] Tutorial tutorial)
        {
            if (ModelState.IsValid)
            {
                db.Tutorials.Add(tutorial);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tutorial);
        }

        // GET: Tutorials/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tutorial tutorial = await db.Tutorials.FindAsync(id);
            if (tutorial == null)
            {
                return HttpNotFound();
            }
            return View(tutorial);
        }

        // POST: Tutorials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,Title,link,description,tags")] Tutorial tutorial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tutorial).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tutorial);
        }

        // GET: Tutorials/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tutorial tutorial = await db.Tutorials.FindAsync(id);
            if (tutorial == null)
            {
                return HttpNotFound();
            }
            return View(tutorial);
        }

        // POST: Tutorials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Tutorial tutorial = await db.Tutorials.FindAsync(id);
            db.Tutorials.Remove(tutorial);
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
