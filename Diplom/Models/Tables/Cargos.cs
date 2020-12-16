using System;
using System.Collections.Generic;

namespace Diplom.Models.Tables
{
    public partial class Cargos
    {
        public Cargos()
        {
            Orders = new HashSet<Orders>();
        }
        public int Id { get; set; }
        public int Weight { get; set; }
        public int CargoVolume { get; set; }
        public string TypeOfCargo { get; set; }
        public string CustomersId { get; set; }

        public virtual Customers Customers { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
