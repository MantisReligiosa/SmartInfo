namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MetablockFrameParams : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MetablockFrames", "Index", c => c.Int(nullable: false));
            AddColumn("dbo.MetablockFrames", "Duration", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MetablockFrames", "Duration");
            DropColumn("dbo.MetablockFrames", "Index");
        }
    }
}
