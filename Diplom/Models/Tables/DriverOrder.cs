using System;
using System.Collections.Generic;

namespace Diplom.Models.Tables
{
    public partial class DriverOrder
    {
        public string DriverId { get; set; }
        public int? OrderId { get; set; }

        public virtual Drivers Driver { get; set; }
        public virtual Orders Order { get; set; }
    }
}
