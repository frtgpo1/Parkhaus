namespace Parking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FügeVisitHinzu : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Visits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerLicensePlate = c.String(),
                        ArrivalTime = c.DateTime(nullable: false),
                        DepartureTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Visits");
        }
    }
}
