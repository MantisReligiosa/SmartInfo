namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TextBlockDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TextBlockDetails", "BackColor", c => c.String());
            AddColumn("dbo.TextBlockDetails", "TextColor", c => c.String());
            AddColumn("dbo.TextBlockDetails", "FontName", c => c.String());
            AddColumn("dbo.TextBlockDetails", "FontSize", c => c.Int(nullable: false));
            AddColumn("dbo.TextBlockDetails", "Align", c => c.Int(nullable: false));
            AddColumn("dbo.TextBlockDetails", "Italic", c => c.Boolean(nullable: false));
            AddColumn("dbo.TextBlockDetails", "Bold", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TextBlockDetails", "Bold");
            DropColumn("dbo.TextBlockDetails", "Italic");
            DropColumn("dbo.TextBlockDetails", "Align");
            DropColumn("dbo.TextBlockDetails", "FontSize");
            DropColumn("dbo.TextBlockDetails", "FontName");
            DropColumn("dbo.TextBlockDetails", "TextColor");
            DropColumn("dbo.TextBlockDetails", "BackColor");
        }
    }
}
