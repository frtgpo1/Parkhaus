namespace Parking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "IsInCarPark", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customers", "FirstName", c => c.String());
            AddColumn("dbo.Customers", "Lastname", c => c.String());
            AddColumn("dbo.Customers", "EMail", c => c.String());
            DropColumn("dbo.Customers", "IsInParkhouse");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "IsInParkhouse", c => c.Boolean(nullable: false));
            DropColumn("dbo.Customers", "EMail");
            DropColumn("dbo.Customers", "Lastname");
            DropColumn("dbo.Customers", "FirstName");
            DropColumn("dbo.Customers", "IsInCarPark");
        }
    }
}
