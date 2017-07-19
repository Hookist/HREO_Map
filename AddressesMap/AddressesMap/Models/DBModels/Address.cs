namespace AddressesMap.Models.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [Table("Address")]
    public partial class Address
    {
        public int AddressId { get; set; }
        public int StreetId { get; set; }
        public int SubdivisionId { get; set; }
        [Required]
        [StringLength(12)]
        public string House { get; set; }
        [StringLength(24)]
        public string Serial { get; set; }
        public int? СountFloor { get; set; }
        public int? СountEntrance { get; set; }
        [Column(TypeName = "numeric")]
        public decimal? Latitude { get; set; }
        [Column(TypeName = "numeric")]
        public decimal? Longitude { get; set; }

        public virtual Street Street { get; set; }

        public virtual Subdivision Subdivision { get; set; }


    }
}
