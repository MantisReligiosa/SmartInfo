namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PictureBlockDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PictureBlockDetails", "Base64Image", c => c.String());
            DropColumn("dbo.PictureBlockDetails", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PictureBlockDetails", "Image", c => c.Binary());
            DropColumn("dbo.PictureBlockDetails", "Base64Image");
        }
    }
}
