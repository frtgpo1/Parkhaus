namespace Parking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntferneParkzeiten : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customers", "CheckInTime");
            DropColumn("dbo.Customers", "CheckOutTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "CheckOutTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Customers", "CheckInTime", c => c.DateTime(nullable: false));
        }
    }
}
