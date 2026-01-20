using System.Net;
using System.Web.Mvc;
using RazorVibes.Web.Data;
using RazorVibes.Web.Models;

namespace RazorVibes.Web.Controllers
{
    public class ContactsController : Controller
    {
        private readonly InMemoryContactRepository _repository = new InMemoryContactRepository();

        public ActionResult Index()
        {
            return View(_repository.GetAll());
        }

        public ActionResult Details(int id)
        {
            var contact = _repository.GetById(id);
            if (contact == null)
            {
                return HttpNotFound();
            }

            return View(contact);
        }

        public ActionResult Create()
        {
            return View(new Contact());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }

            _repository.Add(contact);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var contact = _repository.GetById(id);
            if (contact == null)
            {
                return HttpNotFound();
            }

            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }

            _repository.Update(contact);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var contact = _repository.GetById(id);
            if (contact == null)
            {
                return HttpNotFound();
            }

            return View(contact);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
