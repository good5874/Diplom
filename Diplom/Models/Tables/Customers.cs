using System;
using System.Collections.Generic;

namespace Diplom.Models.Tables
{
    public partial class Customers
    {
        public Customers()
        {
            Cargos = new HashSet<Cargos>();
            Orders = new HashSet<Orders>();
        }

        public string Id { get; set; }
        public string PasportData { get; set; }

        public virtual Users IdNavigation { get; set; }
        public virtual ICollection<Cargos> Cargos { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
