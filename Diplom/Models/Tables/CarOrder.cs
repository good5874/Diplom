using System;
using System.Collections.Generic;

namespace Diplom.Models.Tables
{
    public partial class CarOrder
    {
        public int? CarId { get; set; }
        public int? OrderId { get; set; }

        public virtual Cars Car { get; set; }
        public virtual Orders Order { get; set; }
    }
}
