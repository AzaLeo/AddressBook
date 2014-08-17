using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AddressBook.Models;
using WebMatrix.WebData;
using System.Web.Security;
using AddressBook.Repositories;

namespace AddressBook.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _db;

        public HomeController()
        {
            _db = new AddressBookRepository();
        }

        public HomeController(IRepository repository)
        {
            _db = repository;
        }

        public ActionResult Index()
        {
            return View(_db.GetAllContacts());
        }

        public ActionResult SortingList(string sort)
        {
            IEnumerable<Contacts> modelForSort = (IEnumerable<Contacts>)Session["ModelForSort"];

            switch (sort)
            {
                case "firstname":
                    modelForSort = modelForSort.OrderBy(f => f.FirstName);
                    break;
                case "lastname":
                    modelForSort = modelForSort.OrderBy(l => l.LastName);
                    break;
                case "phone":
                    modelForSort = modelForSort.OrderBy(p => p.Phone);
                    break;
                case "email":
                    modelForSort = modelForSort.OrderBy(e => e.Email);
                    break;
            }
            return PartialView("ContactList", modelForSort);
        }

        public ActionResult Details(int id)
        {
            return PartialView(_db.GetContactById(id));
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.TypeList = GetDropDownList();
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(Contacts newContact)
        {
            newContact.UserId = WebSecurity.CurrentUserId;
            TempData["ChangeDBInfo"] = _db.AddContact(newContact);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            if (WebSecurity.CurrentUserId != _db.GetContactById(id).UserId && !User.IsInRole("Admin"))
            {
                TempData["ChangeDBInfo"] = Resources.Controllers.СanEditOnlyTheirContact;
                return RedirectToAction("Index");
            }
            ViewBag.TypeList = GetDropDownList();
            return View(_db.GetContactById(id));
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Contacts editContact)
        {
            TempData["ChangeDBInfo"] = _db.EditContact(editContact);
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            if (WebSecurity.CurrentUserId != _db.GetContactById(id).UserId && !User.IsInRole("Admin"))
            {
                TempData["ChangeDBInfo"] = Resources.Controllers.CanDeleteOnlyTheirContact;
                return RedirectToAction("Index");
            }
            TempData["ChangeDBInfo"] = _db.DeleteContact(_db.GetContactById(id));
            return RedirectToAction("Index");
        }

        public ActionResult ChangeLanguage(string lang, string returnUrl)
        {
            Session["Culture"] = new System.Globalization.CultureInfo(lang);
            return Redirect(returnUrl);
        }

        [Authorize]
        public ActionResult MyContacts()
        {
            var result = _db.GetAllContacts().Where(u => u.UserId == WebSecurity.CurrentUserId);

            // Если result не дало результатов, необходимо передать null для правильного отображения.
            if (result.Count() == 0)
            {
                result = null;
            }

            return View(result);
        }

        private IEnumerable<SelectListItem> GetDropDownList()
        {
            return from c in _db.GetTypeList()
                   select new SelectListItem
                     {
                         Value = c.TypeId.ToString(),
                         Text = c.Name
                     };
        }
    }
}
