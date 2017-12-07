namespace AppTemplateDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.idealog", "EmployeeId", "dbo.EmployeesBO");
            DropIndex("dbo.idealog", new[] { "EmployeeId" });
            AddColumn("dbo.idealog", "Title", c => c.String(nullable: false));
            AddColumn("dbo.idealog", "FocusDivisions", c => c.String(nullable: false));
            AddColumn("dbo.idealog", "ProjectFinance", c => c.String());
            AddColumn("dbo.idealog", "TechnicalDetail", c => c.String());
            AddColumn("dbo.idealog", "StakeHolder", c => c.String());
            AlterColumn("dbo.idealog", "EmployeeId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.idealog", "EmployeeId");
            AddForeignKey("dbo.idealog", "EmployeeId", "dbo.EmployeesBO", "EmployeeId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.idealog", "EmployeeId", "dbo.EmployeesBO");
            DropIndex("dbo.idealog", new[] { "EmployeeId" });
            AlterColumn("dbo.idealog", "EmployeeId", c => c.String(maxLength: 128));
            DropColumn("dbo.idealog", "StakeHolder");
            DropColumn("dbo.idealog", "TechnicalDetail");
            DropColumn("dbo.idealog", "ProjectFinance");
            DropColumn("dbo.idealog", "FocusDivisions");
            DropColumn("dbo.idealog", "Title");
            CreateIndex("dbo.idealog", "EmployeeId");
            AddForeignKey("dbo.idealog", "EmployeeId", "dbo.EmployeesBO", "EmployeeId");
        }
    }
}
