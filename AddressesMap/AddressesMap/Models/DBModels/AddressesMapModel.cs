namespace AddressesMap.Models.DBModels
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AddressesMapModel : DbContext
    {
        public AddressesMapModel()
            : base("name=AddressesMapModel")
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Street> Streets { get; set; }
        public virtual DbSet<Subdivision> Subdivisions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .Property(e => e.Latitude)
                .HasPrecision(8, 6);

            modelBuilder.Entity<Address>()
                .Property(e => e.Longitude)
                .HasPrecision(8, 6);
        }
    }
}
