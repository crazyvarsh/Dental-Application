namespace SampleApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dentists",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false),
                        EmergencyContactName = c.String(nullable: false, maxLength: 100),
                        EmergencyContactPhone = c.String(nullable: false),
                        DentistID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Dentists", t => t.DentistID, cascadeDelete: true)
                .Index(t => t.DentistID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patients", "DentistID", "dbo.Dentists");
            DropIndex("dbo.Patients", new[] { "DentistID" });
            DropTable("dbo.Users");
            DropTable("dbo.Patients");
            DropTable("dbo.Dentists");
        }
    }
}
