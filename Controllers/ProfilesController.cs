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
using Microsoft.AspNet.Identity;
using System.IO;
using System.Web.Helpers;

namespace alsyedAcademy.Controllers
{
    public class ProfilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        // GET: Profiles
        public ActionResult Index()
        {
            string id = User.Identity.GetUserId();
            var usr = db.Users.Where(x => x.Id == id).SingleOrDefault();
            Profile prof = db.Profiles.Where(x => x.uid == id).SingleOrDefault();
            ProfileViewModel profile = new ProfileViewModel();
            try
            {
                profile = new ProfileViewModel() { fullName = prof.fullName, phone = prof.phone, fatherName = prof.fatherName, uid = User.Identity.GetUserId(), email = usr.Email, profession = prof.profession, id = prof.id, address = prof.address, dob = prof.dob, education = prof.education, photo = prof.photo };
                return View(profile);
            }
            catch
            {
                profile = new ProfileViewModel() { uid = User.Identity.GetUserId(), email = usr.Email};
                return View(profile);
            }
           
        }

        

        // GET: Profiles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = await db.Profiles.FindAsync(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // GET: Profiles/Create
        public ActionResult Create(int? id)
        {
            return View();
        }

        // POST: Profiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,uid,education,dob,address,profession,photo")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                db.Profiles.Add(profile);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(profile);
        }

        // GET: Profiles/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = await db.Profiles.FindAsync(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(HttpPostedFileBase photo, string name, string fatherName, string uid, string phone,  string dob, string profession, string education, string address)
        {
            uid = User.Identity.GetUserId();
            if (uid != "")
            {
                string path = "";
                if (photo != null)
                {
                    string pic = System.IO.Path.GetFileName(photo.FileName);
                    path = Path.Combine(Server.MapPath("~/images/profile original"), name+uid+".jpg");

                    // file is uploaded
                    photo.SaveAs(path);

                    WebImage img = new WebImage(photo.InputStream);
                    int w = (int) (img.Width * .20);
                    int h = (int) (img.Height * .20);

                    img.Resize(w, h);
                    path = Path.Combine(Server.MapPath("~/images/profile thumbs"), name + uid + ".jpg");
                    img.Save(path);



                }

                Profile profile = new Profile()
                {
                    uid = uid,
                    fullName = name,
                    address = address,
                    education = education,
                    dob = DateTime.Parse(dob),
                    phone = phone,
                    profession = profession,
                    photo = name + uid + ".jpg"

                };
                try
                {
                   List<Profile> prof = db.Profiles.Where(x => x.uid == uid).ToList();
                    if (prof.Count() > 0)
                    {
                        db.Profiles.Remove(prof[0]);
                        db.Profiles.Add(profile);
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        db.Entry(profile).State = EntityState.Added;
                        await db.SaveChangesAsync();
                    }
                }
                catch
                {
                    db.Entry(profile).State = EntityState.Added;
                    await db.SaveChangesAsync();
                }
               
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // GET: Profiles/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = await db.Profiles.FindAsync(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Profile profile = await db.Profiles.FindAsync(id);
            db.Profiles.Remove(profile);
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
