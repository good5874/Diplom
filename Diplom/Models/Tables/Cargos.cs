using System;
using System.Collections.Generic;

namespace Diplom.Models.Tables
{
    public partial class Cargos
    {
        public int Id { get; set; }
        public string LocationLoading { get; set; }
        public string LocationUnloading { get; set; }
        public int Weight { get; set; }
        public int CargoVolum { get; set; }
        public string Status { get; set; }
        public string CustomersId { get; set; }
        public int? OrderId { get; set; }

        public virtual Customers Customers { get; set; }
        public virtual Orders Order { get; set; }
    }
}
