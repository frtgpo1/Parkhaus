namespace Parking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MacheAllesRichtig : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Customers");
            AlterColumn("dbo.Customers", "LicensePlate", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Customers", "LicensePlate");
            DropColumn("dbo.Customers", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Customers");
            AlterColumn("dbo.Customers", "LicensePlate", c => c.String());
            AddPrimaryKey("dbo.Customers", "Id");
        }
    }
}
