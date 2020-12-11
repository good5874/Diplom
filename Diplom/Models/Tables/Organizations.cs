using System;
using System.Collections.Generic;

namespace Diplom.Models.Tables
{
    public partial class Organizations
    {
        public Organizations()
        {
            Cars = new HashSet<Cars>();
            Employees = new HashSet<Employees>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNamber { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Cars> Cars { get; set; }
        public virtual ICollection<Employees> Employees { get; set; }
    }
}
