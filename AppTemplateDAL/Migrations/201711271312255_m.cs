namespace AppTemplateDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CityCountry",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        cityName = c.String(),
                        country = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CreationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        RevisionDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        RevisedBy = c.String(),
                    })
                .PrimaryKey(t => t.CityId);
            
            CreateTable(
                "dbo.EmployeesBO",
                c => new
                    {
                        EmployeeId = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        StationID = c.Int(nullable: false),
                        Section = c.String(),
                        position = c.String(),
                        PhoneNumber = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CreationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        RevisionDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        RevisedBy = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Stations", t => t.StationID, cascadeDelete: true)
                .Index(t => t.StationID);
            
            CreateTable(
                "dbo.Stations",
                c => new
                    {
                        StationID = c.Int(nullable: false, identity: true),
                        StationName = c.String(),
                        CityID = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CreationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        RevisionDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        RevisedBy = c.String(),
                    })
                .PrimaryKey(t => t.StationID)
                .ForeignKey("dbo.CityCountry", t => t.CityID, cascadeDelete: true)
                .Index(t => t.CityID);
            
            CreateTable(
                "dbo.idealog",
                c => new
                    {
                        LogId = c.Int(nullable: false, identity: true),
                        EmployeeId = c.String(maxLength: 128),
                        idea = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CreationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        RevisionDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        RevisedBy = c.String(),
                    })
                .PrimaryKey(t => t.LogId)
                .ForeignKey("dbo.EmployeesBO", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.idealog", "EmployeeId", "dbo.EmployeesBO");
            DropForeignKey("dbo.EmployeesBO", "StationID", "dbo.Stations");
            DropForeignKey("dbo.Stations", "CityID", "dbo.CityCountry");
            DropIndex("dbo.idealog", new[] { "EmployeeId" });
            DropIndex("dbo.Stations", new[] { "CityID" });
            DropIndex("dbo.EmployeesBO", new[] { "StationID" });
            DropTable("dbo.idealog");
            DropTable("dbo.Stations");
            DropTable("dbo.EmployeesBO");
            DropTable("dbo.CityCountry");
        }
    }
}
