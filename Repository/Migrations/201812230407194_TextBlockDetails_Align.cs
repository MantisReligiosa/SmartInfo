namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TextBlockDetails_Align : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TextBlockDetails", "Align", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TextBlockDetails", "Align");
        }
    }
}
