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
        }
        
        public override void Down()
        {
            DropColumn("dbo.TextBlockDetails", "TextColor");
            DropColumn("dbo.TextBlockDetails", "BackColor");
        }
    }
}
