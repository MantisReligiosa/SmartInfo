namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NameOfFrame : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MetablockFrames", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MetablockFrames", "Name");
        }
    }
}
