namespace AppTemplateDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.idealog", "idea", c => c.String(nullable: false));
            DropColumn("dbo.idealog", "TechnicalDetail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.idealog", "TechnicalDetail", c => c.String());
            AlterColumn("dbo.idealog", "idea", c => c.String());
        }
    }
}
