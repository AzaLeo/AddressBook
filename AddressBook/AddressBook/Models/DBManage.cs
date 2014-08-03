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
            Contacts addedContact = _db.Contacts.Add(newContact);
            string addedContactName = addedContact.FirstName + " " + addedContact.LastName;

            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                return "Контакт " + addedContactName + " не был добавлен. Причина: " + e.Message;
            }

            return "Контакт " + addedContactName + " был успешно добавлен!";
        }

        public string EditContact(Contacts editContact)
        {
            Contacts oldContact = GetContact(editContact.ContactsId);
            string removedContactName = editContact.FirstName + " " + editContact.LastName;
            oldContact.FirstName = editContact.FirstName;
            oldContact.MiddleName = editContact.MiddleName;
            oldContact.LastName = editContact.LastName;
            oldContact.Phone = editContact.Phone;
            oldContact.Email = editContact.Email;
            oldContact.Addresses = editContact.Addresses;
            oldContact.TypeId = editContact.TypeId;

            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                return "Контакт " + removedContactName + " не был изменен. Причина: " + e.Message;
            }

            return "Контакт " + removedContactName + " был успешно изменен!";
        }

        public string DeleteContact(Contacts delContat)
        {
            _db.Addresses.Remove(delContat.Addresses);
            Contacts removedContact = _db.Contacts.Remove(delContat);
            string removedContactName = removedContact.FirstName + " " + removedContact.LastName;

            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                return "Контакт " + removedContactName + " не был удален. Причина: " + e.Message;
            }

            return "Контакт " + removedContactName + " был успешно удален!";
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