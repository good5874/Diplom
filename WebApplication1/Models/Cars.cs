namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cars
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string StateNamber { get; set; }

        [Required]
        [StringLength(50)]
        public string CarBrand { get; set; }

        public int CarringCapacity { get; set; }

        public int BodyVolume { get; set; }

        public int OrganizationId { get; set; }

        public virtual Organizations Organizations { get; set; }
    }
}
