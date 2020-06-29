using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PhoneBook.Models
{
    public class Entry
    {
        public Entry()
        {
            this.Name = string.Empty;
            this.PhoneNumber = string.Empty;
        }

        public int EntryID { get; set; }
        public int PhoneBookID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}