using alsyedAcademy.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace alsyedAcademy.Controllers
{
    public class FileUploadController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(int id)
        {
            ViewBag.eid = id;
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase[] files, int eid)
        {
            int count = files.Count();

            //Ensure model state is valid  
            if (ModelState.IsValid)
            {   //iterating through multiple file collection   
                foreach (HttpPostedFileBase file in files)
                {
                    try
                    {
                        //Checking file is available to save.  
                        if (file != null)
                        {
                            string InputFileName = Path.GetFileName(file.FileName);
                            string path = Path.Combine(Server.MapPath("~/images/Galleryoriginal/"), InputFileName);

                            //Save file to server folder  
                            file.SaveAs(path);

                            WebImage img = new WebImage(file.InputStream);
                            int w = (int)(img.Width * .20);
                            int h = (int)(img.Height * .20);

                            img.Resize(w, h);
                            string thumbsPath = System.IO.Path.Combine(Server.MapPath("~/images/Gallerythumbs/"), InputFileName);
                            img.Save(thumbsPath);


                            //assigning file uploaded status to ViewBag for showing message to user.  
                            ViewBag.UploadStatus = count.ToString() + " files uploaded successfully.";
                            db.Galleries.Add(new Gallery() { eid = eid, path = InputFileName, VideoPhoto = 0 });
                            db.SaveChanges();
                        }
                    }
                    catch
                    {
                        count -= 1;
                    }

                }
            }
            return Redirect("/Events/");
        }

        public ActionResult FileDelete(int id)
        {
            Gallery g = db.Galleries.Find(id);///db.Galleries.Where(x => x.id == id).SingleOrDefault();

            try
            {
                string InputFileName = Path.GetFileName(g.path);
                string path = Path.Combine(Server.MapPath("~/images/Galleryoriginal/"), InputFileName);
                System.IO.File.Delete(path);

                string thumbsPath = Path.Combine(Server.MapPath("~/images/Gallerythumbs/"), InputFileName);
                System.IO.File.Delete(thumbsPath);

                db.Galleries.Remove(g);
                db.SaveChanges();
            }
            catch
            {
               //Do nothing
            }
            return Redirect("/Events/");
        }
    }
}
