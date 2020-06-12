using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DemoGestioneNews.Models;

namespace DemoGestioneNews.Controllers
{
    public class NewsController : Controller
    {
        private NewsContext db = new NewsContext();

        // GET: News
        public ActionResult Index()
        {
            var news = db.News.Include(n => n.Author);
            return View(news.ToList());
        }

        // POST: News with Filters
        [HttpPost]
        public ActionResult Index(string Name, string Type, DateTime? DataMin, DateTime? DataMax, string Author)
        {
            var news = db.News.Include(n => n.Author)
                .Where(n => Name != "" ? n.Name.Contains(Name) : true &&
                            Type != "" ? n.Type.Contains(Type) : true &&
                            DataMin != null ? n.PublishDate > DataMin : true &&
                            DataMax != null ? n.PublishDate < DataMax : true &&
                            Author != "" ? n.Author.Name.Contains(Author) : true);
            return View(news.ToList());
        }

        public ActionResult DeleteNewsWithDate(DateTime? DateDel)
        {
            if(DateDel != null)
            {
                var news = db.News.Where(n => n.PublishDate == DateDel);
                if (news != null)
                {
                    db.News.RemoveRange(news);
                    db.SaveChanges();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult IndexGroupByNews()
        {
            var news = db.News.Include(n => n.Author)
                .GroupBy(n => n.Type)
                .Select(n => new { Tipo = n.Key, Count = n.Count() })
                .OrderByDescending(n => n.Count)
                .ToDictionary(t => t.Tipo, t => t.Count);
            ViewData["ListaAutori"] = news;
            return View();
        }

        // GET: News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: News/Create
        public ActionResult Create()
        {
            ViewBag.FKAuthor = new SelectList(db.Authors, "PKAuthor", "Name");
            return View();
        }

        // POST: News/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PKNews,Name,Type,PublishDate,FKAuthor")] News news)
        {
            if (ModelState.IsValid)
            {
                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FKAuthor = new SelectList(db.Authors, "PKAuthor", "Name", news.FKAuthor);
            return View(news);
        }

        // GET: News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            ViewBag.FKAuthor = new SelectList(db.Authors, "PKAuthor", "Name", news.FKAuthor);
            return View(news);
        }

        // POST: News/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PKNews,Name,Type,PublishDate,FKAuthor")] News news)
        {
            if (ModelState.IsValid)
            {
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FKAuthor = new SelectList(db.Authors, "PKAuthor", "Name", news.FKAuthor);
            return View(news);
        }

        // GET: News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
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
