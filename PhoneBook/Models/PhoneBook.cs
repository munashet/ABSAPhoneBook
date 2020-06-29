using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PhoneBook.Models
{
    public class PhoneBook
    {
        public PhoneBook()
        {
            this.Name = string.Empty;
            this.Entries = new List<Entry>();
        }

        public int PhoneBookID { get; set; }
        public string Name { get; set; }
        public List<Entry> Entries { get; set; }
    }
}