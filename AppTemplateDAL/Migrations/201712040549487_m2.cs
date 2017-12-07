namespace AppTemplateDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.idealog", "status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.idealog", "status");
        }
    }
}
