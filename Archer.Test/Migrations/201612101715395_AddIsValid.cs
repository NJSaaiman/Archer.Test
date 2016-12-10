namespace Archer.Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsValid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClientData", "IsValid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClientData", "IsValid");
        }
    }
}
