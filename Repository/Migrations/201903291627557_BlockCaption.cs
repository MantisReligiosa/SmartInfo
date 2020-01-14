namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BlockCaption : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DisplayBlocks", "Caption", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DisplayBlocks", "Caption");
        }
    }
}
