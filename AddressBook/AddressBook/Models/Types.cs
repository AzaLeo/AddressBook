//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AddressBook.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Types
    {
        public Types()
        {
            this.Contacts = new HashSet<Contacts>();
        }
    
        public int TypeId { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Contacts> Contacts { get; set; }
    }
}
