using System;
using System.Collections.Generic;

namespace Diplom.Models.Tables
{
    public partial class Managers
    {
        public Managers()
        {
            Orders = new HashSet<Orders>();
        }

        public string Id { get; set; }
        public string Education { get; set; }

        public virtual Employees Employees { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
