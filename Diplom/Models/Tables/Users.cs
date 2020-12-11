using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Diplom.Models.Tables
{
    public partial class Users : IdentityUser
    {
        public Users()
        {

        }

        public virtual Customers Customers { get; set; }
        public virtual Employees Employees { get; set; }

    }
}
