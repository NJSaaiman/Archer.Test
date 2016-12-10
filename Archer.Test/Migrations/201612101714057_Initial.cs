namespace Archer.Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 255),
                    IOWorkerName = c.String(maxLength: 255),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.ClientData",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 255),
                    CellNumber = c.String(maxLength: 255),
                    EmailAddress = c.String(maxLength: 255),
                    ClientID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientID, cascadeDelete: true)
                .Index(t => t.ClientID);

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClientData", "ClientID", "dbo.Clients");
            DropIndex("dbo.ClientData", new[] { "ClientID" });
            DropTable("dbo.ClientData");
            DropTable("dbo.Clients");
        }
    }
}
