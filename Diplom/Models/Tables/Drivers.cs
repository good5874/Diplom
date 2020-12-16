using System;
using System.Collections.Generic;

namespace Diplom.Models.Tables
{
    public partial class Drivers
    {
        public Drivers()
        {
            Orders = new HashSet<Orders>();
        }
        public string Id { get; set; }
        public string DrivingLicense { get; set; }
        public string Location { get; set; }
        public int DrivingExperience { get; set; }

        public virtual Employees Employees { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
