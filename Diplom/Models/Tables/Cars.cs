using System;
using System.Collections.Generic;

namespace Diplom.Models.Tables
{
    public partial class Cars
    {
        public Cars()
        {
            Orders = new HashSet<Orders>();
        }
        public int Id { get; set; }
        public string StateNamber { get; set; }
        public string CarBrand { get; set; }
        public int CarringCapacity { get; set; }
        public int BodyVolume { get; set; }
        public int OrganizationId { get; set; }

        public virtual Organizations Organization { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
