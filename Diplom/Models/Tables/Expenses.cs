using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.Tables
{
    public class Expenses
    {
        public int Id { get; set; }
        public int FuelCosts { get; set; }
        public int OtherExpenses { get; set; }


        public virtual Orders Order { get; set; }
    }
}
