namespace AddressesMap.Models.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [DataContract]
    [Table("Address")]
    public partial class Address
    {
        [DataMember]
        public int AddressId { get; set; }
        [DataMember]
        public int StreetId { get; set; }
        [DataMember]
        public int SubdivisionId { get; set; }
        [DataMember]
        [Required]
        [StringLength(12)]
        public string House { get; set; }
        [DataMember]
        [StringLength(24)]
        public string Serial { get; set; }
        [DataMember]
        public int? СountFloor { get; set; }
        [DataMember]
        public int? СountEntrance { get; set; }
        [DataMember]
        [Column(TypeName = "numeric")]
        public decimal? Latitude { get; set; }
        [DataMember]
        [Column(TypeName = "numeric")]
        public decimal? Longitude { get; set; }

        public virtual Street Street { get; set; }

        public virtual Subdivision Subdivision { get; set; }
    }
}
