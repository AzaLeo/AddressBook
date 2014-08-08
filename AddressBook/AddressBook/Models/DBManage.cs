using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AddressBook.Models
{
    public class DBManage
    {
        private AddressBookEntities _db;

        public DBManage()
        {
            _db = new AddressBookEntities();
        }

        public IEnumerable<Contacts> GetAllContacts()
        {
            return _db.Contacts;
        }

        public Contacts GetContact(int id)
        {
            return _db.Contacts.SingleOrDefault(i => i.ContactsId == id);
        }

        public IEnumerable<Types> GetTypeList()
        {
            return _db.Types;
        }

        public string AddContact(Contacts newContact)
        {
            _db.Contacts.Add(newContact);

            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                return Resources.Models.AddContactFailed + " " + e.Message;
            }

            return Resources.Models.AddContactSuccess;
        }

        public string EditContact(Contacts edit)
        {
            Contacts original = GetContact(edit.ContactsId);
            original.FirstName = edit.FirstName;
            original.MiddleName = edit.MiddleName;
            original.LastName = edit.LastName;
            original.Phone = edit.Phone;
            original.Email = edit.Email;
            original.TypeId = edit.TypeId;
            UpdateAddress(original.Addresses, edit.Addresses);

            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                return Resources.Models.EditContactFailed + " " + e.Message;
            }

            return Resources.Models.EditContactSuccess;
        }

        private void UpdateAddress(Addresses original, Addresses edit)
        {
            original.Country = edit.Country;
            original.City = edit.City;
            original.Street = edit.Street;
            original.House = edit.House;
            original.Room = edit.Room;
        }

        public string DeleteContact(Contacts delContat)
        {
            _db.Addresses.Remove(delContat.Addresses);
            _db.Contacts.Remove(delContat);

            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                return Resources.Models.DeleteContactFailed + " " + e.Message;
            }

            return Resources.Models.DeleteContactSuccess;
        }

        public IEnumerable<Contacts> SearchByFirstLetterFirstName(string symbol)
        {
            return _db.Contacts.Where(s => s.FirstName.Substring(0, 1) == symbol);
        }

        public IEnumerable<Contacts> SearchByFirstLetterLastName(string symbol)
        {
            return _db.Contacts.Where(s => s.LastName.Substring(0, 1) == symbol);
        }

        public IEnumerable<Contacts> SearchByFirstLetterPhone(string symbol)
        {
            return _db.Contacts.Where(s => s.Phone.Substring(0, 1) == symbol);
        }

        public IEnumerable<Contacts> SearchByFirstLetterEmail(string symbol)
        {
            return _db.Contacts.Where(s => s.Email.Substring(0, 1) == symbol);
        }
    }
}