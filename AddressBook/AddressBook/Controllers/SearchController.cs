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
            var symbols = new List<char>();

            for (int i = 48; i <= 57; i++)
                symbols.Add((char)i);
            for (int i = 65; i <= 90; i++)
                symbols.Add((char)i);
            for (int i = 1040; i <= 1071; i++)
                symbols.Add((char)i);

            var searchList = new SelectList(symbols);

            ViewBag.Symbols = searchList;

            return View();
        }

        public ActionResult Result(string selectSymbols, List<int> searchOption)
        {
            IEnumerable<Contacts> result = null;

            switch (searchOption.First())
            {
                case 1:
                    result = _db.SearchByFirstLetterFirstName(selectSymbols);
                    break;
                case 2:
                    result = _db.SearchByFirstLetterLastName(selectSymbols);
                    break;
                case 3:
                    result = _db.SearchByFirstLetterPhone(selectSymbols);
                    break;
                case 4:
                    result = _db.SearchByFirstLetterEmail(selectSymbols);
                    break;
            }

            // Если result не дало результатов, необходимо передать null для правильного отображения.
            try
            {
                result.First();
            }
            catch (InvalidOperationException)
            {
                result = null;
                return PartialView("ContactList", result);
            }

            return PartialView("ContactList", result);
        }

    }
}
