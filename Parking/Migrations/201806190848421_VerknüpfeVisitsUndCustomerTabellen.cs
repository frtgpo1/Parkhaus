namespace Parking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VerknüpfeVisitsUndCustomerTabellen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Visits", "Customer_LicensePlate", c => c.String(maxLength: 128));
            CreateIndex("dbo.Visits", "Customer_LicensePlate");
            AddForeignKey("dbo.Visits", "Customer_LicensePlate", "dbo.Customers", "LicensePlate");
            DropColumn("dbo.Visits", "CustomerLicensePlate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Visits", "CustomerLicensePlate", c => c.String());
            DropForeignKey("dbo.Visits", "Customer_LicensePlate", "dbo.Customers");
            DropIndex("dbo.Visits", new[] { "Customer_LicensePlate" });
            DropColumn("dbo.Visits", "Customer_LicensePlate");
        }
    }
}
