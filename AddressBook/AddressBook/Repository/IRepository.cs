using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Models
{
    public interface IRepository
    {
        IEnumerable<Contacts> GetAllContacts();
        Contacts GetContact(int id);
        IEnumerable<Types> GetTypeList();
        string AddContact(Contacts newContact);
        string EditContact(Contacts edit);
        string DeleteContact(Contacts delContat);
        IEnumerable<Contacts> SearchByFirstLetterFirstName(string symbol);
        IEnumerable<Contacts> SearchByFirstLetterLastName(string symbol);
        IEnumerable<Contacts> SearchByFirstLetterPhone(string symbol);
        IEnumerable<Contacts> SearchByFirstLetterEmail(string symbol);
    }
}
