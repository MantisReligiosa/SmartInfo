namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageOptions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PictureBlockDetails", "ImageMode", c => c.Int(nullable: false));
            AddColumn("dbo.PictureBlockDetails", "SaveProportions", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PictureBlockDetails", "SaveProportions");
            DropColumn("dbo.PictureBlockDetails", "ImageMode");
        }
    }
}
