namespace AddressesMap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAddressTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Address", "StreetId", "dbo.Street");
            DropForeignKey("dbo.Address", "SubdivisionId", "dbo.Subdivision");
            DropIndex("dbo.Address", new[] { "StreetId" });
            DropIndex("dbo.Address", new[] { "SubdivisionId" });
            AlterColumn("dbo.Address", "StreetId", c => c.Int());
            AlterColumn("dbo.Address", "SubdivisionId", c => c.Int());
            CreateIndex("dbo.Address", "StreetId");
            CreateIndex("dbo.Address", "SubdivisionId");
            AddForeignKey("dbo.Address", "StreetId", "dbo.Street", "StreetId");
            AddForeignKey("dbo.Address", "SubdivisionId", "dbo.Subdivision", "SubdivisionId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Address", "SubdivisionId", "dbo.Subdivision");
            DropForeignKey("dbo.Address", "StreetId", "dbo.Street");
            DropIndex("dbo.Address", new[] { "SubdivisionId" });
            DropIndex("dbo.Address", new[] { "StreetId" });
            AlterColumn("dbo.Address", "SubdivisionId", c => c.Int(nullable: false));
            AlterColumn("dbo.Address", "StreetId", c => c.Int(nullable: false));
            CreateIndex("dbo.Address", "SubdivisionId");
            CreateIndex("dbo.Address", "StreetId");
            AddForeignKey("dbo.Address", "SubdivisionId", "dbo.Subdivision", "SubdivisionId", cascadeDelete: true);
            AddForeignKey("dbo.Address", "StreetId", "dbo.Street", "StreetId", cascadeDelete: true);
        }
    }
}
