using System.Web.Mvc;
using RazorVibes.Models;

namespace RazorVibes.Controllers;

public class SongsController : Controller
{
    public ActionResult Index()
    {
        var songs = SongStore.GetAll();
        return View(songs);
    }

    public ActionResult Details(int id)
    {
        var song = SongStore.GetById(id);
        if (song is null)
        {
            return HttpNotFound();
        }

        return View(song);
    }

    public ActionResult Create()
    {
        return View(new Song());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Song song)
    {
        if (!ModelState.IsValid)
        {
            return View(song);
        }

        SongStore.Add(song);
        return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
        var song = SongStore.GetById(id);
        if (song is null)
        {
            return HttpNotFound();
        }

        return View(song);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(Song song)
    {
        if (!ModelState.IsValid)
        {
            return View(song);
        }

        SongStore.Update(song);
        return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
        var song = SongStore.GetById(id);
        if (song is null)
        {
            return HttpNotFound();
        }

        return View(song);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        SongStore.Delete(id);
        return RedirectToAction("Index");
    }
}
