namespace ECI.Test.DA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientDogs",
                c => new
                    {
                        ClientId = c.Int(nullable: false),
                        DogId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ClientId, t.DogId })
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Dogs", t => t.DogId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.DogId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Phone = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Dogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Breed = c.String(maxLength: 50),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 255),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true);
            
            CreateTable(
                "dbo.Walks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        DogId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Duration = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Dogs", t => t.DogId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.DogId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Walks", "DogId", "dbo.Dogs");
            DropForeignKey("dbo.Walks", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.ClientDogs", "DogId", "dbo.Dogs");
            DropForeignKey("dbo.ClientDogs", "ClientId", "dbo.Clients");
            DropIndex("dbo.Walks", new[] { "DogId" });
            DropIndex("dbo.Walks", new[] { "ClientId" });
            DropIndex("dbo.Users", new[] { "UserName" });
            DropIndex("dbo.ClientDogs", new[] { "DogId" });
            DropIndex("dbo.ClientDogs", new[] { "ClientId" });
            DropTable("dbo.Walks");
            DropTable("dbo.Users");
            DropTable("dbo.Dogs");
            DropTable("dbo.Clients");
            DropTable("dbo.ClientDogs");
        }
    }
}
