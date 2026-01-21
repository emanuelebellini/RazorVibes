using System.Web.Mvc;
using RazorVibes2.Models;

namespace RazorVibes2.Controllers
{
    public class AlbumsController : Controller
    {
        public ActionResult Index()
        {
            var albums = InMemoryAlbumStore.GetAll();
            return View(albums);
        }

        public ActionResult Details(int id)
        {
            var album = InMemoryAlbumStore.GetById(id);
            if (album == null)
            {
                return HttpNotFound();
            }

            return View(album);
        }

        public ActionResult Create()
        {
            return View(new Album());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Album album)
        {
            if (!ModelState.IsValid)
            {
                return View(album);
            }

            InMemoryAlbumStore.Add(album);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var album = InMemoryAlbumStore.GetById(id);
            if (album == null)
            {
                return HttpNotFound();
            }

            return View(album);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Album album)
        {
            if (!ModelState.IsValid)
            {
                return View(album);
            }

            if (!InMemoryAlbumStore.Update(album))
            {
                return HttpNotFound();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var album = InMemoryAlbumStore.GetById(id);
            if (album == null)
            {
                return HttpNotFound();
            }

            return View(album);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!InMemoryAlbumStore.Remove(id))
            {
                return HttpNotFound();
            }

            return RedirectToAction("Index");
        }
    }
}
