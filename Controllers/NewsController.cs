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
using System.IO;
using System.Web.Helpers;

namespace alsyedAcademy.Controllers
{
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: News
        public ActionResult Index()
        {
            List<News> allNews = db.News.ToList();
            List<NewsVMTwo> newsVM = new List<NewsVMTwo>();

            foreach(News nw in allNews )
            {
                List<Tag> tags = db.Tags.Where(x => x.fid == nw.id).ToList();
                string stringTags = "";
                int a = 1;
                foreach(Tag tag in tags )
                {
                    if (a < tags.Count - 1) {
                        stringTags += tag.text + ", ";
                    }
                    else
                    {
                        stringTags += tag.text;
                    }
                }
                NewsVMTwo nvm = new NewsVMTwo() { id = nw.id, tags = stringTags, title=nw.title, status=nw.status, dated = nw.dated, description = nw.description, photo = nw.photo };
                newsVM.Add(nvm);
            }


            return View(newsVM);
        }

        // GET: News/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = await db.News.FindAsync(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: News/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,photo,title, status,dated,description,tags")] NewsVM nw)
        {
            string path = "";
            if (nw.photo != null)
            {
                string InputFileName = Path.GetFileName(nw.photo.FileName);
                path = Path.Combine(Server.MapPath("~/images/Newsoriginal/"), InputFileName);
                nw.photo.SaveAs(path);
                WebImage img = new WebImage(nw.photo.InputStream);
                int w = (int)(img.Width * .20);
                int h = (int)(img.Height * .20);

                img.Resize(w, h);
                string thumbsPath = System.IO.Path.Combine(Server.MapPath("~/images/Newsthumbs/"), InputFileName);
                img.Save(thumbsPath);


               
                path = nw.photo.FileName;
            }

            News news = new News() { dated = nw.dated.Date, title = nw.title, description = nw.description, status = nw.status, photo = path };
            if (ModelState.IsValid)
            {
                db.News.Add(news);
                db.SaveChanges();
            }

            News nn = db.News.Where(x => x.dated == nw.dated && x.title == nw.title && x.photo == path).OrderByDescending(x => x.dated).SingleOrDefault();                      
                       
            if(nw.tags != null)
            {
                char[] delimiters = new char[] { ',' };
                String [] theTags = nw.tags.Split(delimiters);
                foreach(string s in theTags)
                {
                    Tag tag = new Tag()
                    {
                        dealsWith = 0, //means news tag
                        fid = nn.id,
                        text = s
                    };
                    db.Tags.Add(tag);
                    db.SaveChanges();
                }               
            }
            
            return RedirectToAction("Index");
            //return View(news);
        }

        // GET: News/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = await db.News.FindAsync(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            
                List<Tag> tags = db.Tags.Where(x => x.fid == news.id).ToList();
                string stringTags = "";
                int a = 1;
                foreach (Tag tag in tags)
                {
                    if (a < tags.Count - 1)
                    {
                        stringTags += tag.text + ", ";
                    }
                    else
                    {
                        stringTags += tag.text;
                    }
                }
                NewsVMTwo nvm = new NewsVMTwo() { id = news.id, title = news.title, tags = stringTags, status = news.status, dated = news.dated, description = news.description, photo = news.photo };
                
            
            return View(nvm);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id, title, photo,status,dated,description,tags")] NewsVMTwo news)
        {
            if (ModelState.IsValid)
            {
                News nw = await db.News.FindAsync(news.id);
                string photo = nw.photo;
                db.News.Remove(nw);

                nw = new News() { id = news.id, title=news.title, status = news.status, dated = news.dated, description = news.description, photo = photo };
                db.News.Add(nw);
                db.SaveChanges();

                News nn = db.News.Where(x => x.dated == nw.dated && x.title == nw.title && x.photo == photo).OrderByDescending(x => x.dated).SingleOrDefault();
                List<Tag> tagsToDelete = db.Tags.Where(x => x.fid == news.id).ToList();
                foreach(Tag t in tagsToDelete)
                {
                    db.Tags.Remove(t);
                    db.SaveChanges();
                }
                 
                if (news.tags != null)
                {
                    char[] delimiters = new char[] { ',' };
                    String[] theTags = news.tags.Split(delimiters);
                    foreach (string s in theTags)
                    {
                        Tag tag = new Tag()
                        {
                            dealsWith = 0, //means news tag
                            fid = nn.id,
                            text = s
                        };
                        db.Tags.Add(tag);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: News/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = await db.News.FindAsync(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            News news = await db.News.FindAsync(id);
            try
            {               
                string InputFileName = news.photo;//Path.GetFileName(nw.photo.FileName);
                string path = Path.Combine(Server.MapPath("~/images/Newsoriginal/"), InputFileName);
                System.IO.File.Delete(path);

                path = Path.Combine(Server.MapPath("~/images/Newsthumbs/"), InputFileName);
                System.IO.File.Delete(path);
            }
            catch
            {

            }
            db.News.Remove(news);
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
