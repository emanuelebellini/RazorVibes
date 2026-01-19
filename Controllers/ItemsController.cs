using System.Net;
using System.Web.Mvc;
using RazorVibes.Models;

namespace RazorVibes.Controllers
{
    public class ItemsController : Controller
    {
        public ActionResult Index()
        {
            return View(InMemoryItemRepository.GetAll());
        }

        public ActionResult Details(int id)
        {
            var item = InMemoryItemRepository.GetById(id);
            if (item == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(item);
        }

        public ActionResult Create()
        {
            return View(new Item());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Item item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

            InMemoryItemRepository.Add(item);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var item = InMemoryItemRepository.GetById(id);
            if (item == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Item item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

            InMemoryItemRepository.Update(item);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var item = InMemoryItemRepository.GetById(id);
            if (item == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(item);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InMemoryItemRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
