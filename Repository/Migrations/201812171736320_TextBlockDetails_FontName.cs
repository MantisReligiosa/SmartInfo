namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TextBlockDetails_FontName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TextBlockDetails", "FontName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TextBlockDetails", "FontName");
        }
    }
}
