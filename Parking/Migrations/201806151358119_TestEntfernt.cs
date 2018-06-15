namespace Parking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestEntfernt : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customers", "Test");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "Test", c => c.Int(nullable: false));
        }
    }
}
