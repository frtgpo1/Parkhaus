namespace Parking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MacheSachen : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Visits", "ArrivalTime", c => c.DateTime());
            AlterColumn("dbo.Visits", "DepartureTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Visits", "DepartureTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Visits", "ArrivalTime", c => c.DateTime(nullable: false));
        }
    }
}
