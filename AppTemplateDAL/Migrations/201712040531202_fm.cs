namespace AppTemplateDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.idealog", "FollowupKey", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.idealog", "FollowupKey");
        }
    }
}
