using System;
using System.Collections.Generic;

namespace CappyPop_Full_HTML.Models.Tables
{
    public partial class Account
    {
        public Account()
        {
            Adminsellers = new HashSet<Adminseller>();
            Customers = new HashSet<Customer>();
        }

        public int AccountId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int? RoleId { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<Adminseller> Adminsellers { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
