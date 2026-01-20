using System.Net;
using System.Web.Mvc;
using RazorVibes.Data;
using RazorVibes.Models;

namespace RazorVibes.Controllers
{
    public class ProductsController : Controller
    {
        private readonly InMemoryProductStore _store = new InMemoryProductStore();

        public ActionResult Index()
        {
            var products = _store.GetAll();
            return View(products);
        }

        public ActionResult Details(int id)
        {
            var product = _store.GetById(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        public ActionResult Create()
        {
            return View(new Product());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            _store.Add(product);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var product = _store.GetById(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            var updated = _store.Update(product);
            if (!updated)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var product = _store.GetById(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _store.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
