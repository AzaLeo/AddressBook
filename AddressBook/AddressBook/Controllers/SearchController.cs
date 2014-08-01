using AddressBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AddressBook.Controllers
{
    public class SearchController : Controller
    {
        private DBManage _db;

        public SearchController()
        {
            _db = new DBManage();
        }

        public ActionResult Index()
        {
            List<char> alphabet = new List<char>();

            for (int i = 1040; i <= 1071; i++)
                alphabet.Add((char)i);
            ViewBag.Alphabet = alphabet;

            return View();
        }

        public ActionResult SearchResult(int letter)
        {
            return PartialView("ContactList", _db.GetAllContacts());
        }

    }
}
