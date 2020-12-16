using System;
using System.Collections.Generic;

namespace Diplom.Models.Tables
{
    public partial class Orders
    {
        public int Id { get; set; }
        public string DateOfTheContract { get; set; }
        public DateTime DateLoading { get; set; }
        public DateTime DateUnloading { get; set; }
        public string LocationLoading { get; set; }
        public string LocationUnloading { get; set; }
        public int? Cost { get; set; }
        public bool WasPaid { get; set; }
        public bool WasDelivery { get; set; }
        public string CustomerId { get; set; }
        public int? CarId { get; set; }
        public string ManagerId { get; set; }
        public int CargoId { get; set; }
        public string DriversId { get; set; }
        public string Status { get; set; }


        public virtual Cars Car { get; set; }
        public virtual Expenses Expenses { get; set; }
        public virtual Customers Customer { get; set; }
        public virtual Drivers Driver { get; set; }
        public virtual Managers Manager { get; set; }
        public virtual Cargos Cargo { get; set; }
    }
}
