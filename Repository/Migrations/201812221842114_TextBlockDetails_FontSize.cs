namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TextBlockDetails_FontSize : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TextBlockDetails", "FontSize", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TextBlockDetails", "FontSize");
        }
    }
}
