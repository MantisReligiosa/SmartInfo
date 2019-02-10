namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DisplayBlocksDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PictureBlockDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TextBlockDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.DisplayBlocks", "Details_Id", c => c.Guid());
            AddColumn("dbo.DisplayBlocks", "Details_Id1", c => c.Guid());
            CreateIndex("dbo.DisplayBlocks", "Details_Id");
            CreateIndex("dbo.DisplayBlocks", "Details_Id1");
            AddForeignKey("dbo.DisplayBlocks", "Details_Id", "dbo.PictureBlockDetails", "Id");
            AddForeignKey("dbo.DisplayBlocks", "Details_Id1", "dbo.TextBlockDetails", "Id");
            DropColumn("dbo.DisplayBlocks", "Image");
            DropColumn("dbo.DisplayBlocks", "Text");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DisplayBlocks", "Text", c => c.String());
            AddColumn("dbo.DisplayBlocks", "Image", c => c.Binary());
            DropForeignKey("dbo.DisplayBlocks", "Details_Id1", "dbo.TextBlockDetails");
            DropForeignKey("dbo.DisplayBlocks", "Details_Id", "dbo.PictureBlockDetails");
            DropIndex("dbo.DisplayBlocks", new[] { "Details_Id1" });
            DropIndex("dbo.DisplayBlocks", new[] { "Details_Id" });
            DropColumn("dbo.DisplayBlocks", "Details_Id1");
            DropColumn("dbo.DisplayBlocks", "Details_Id");
            DropTable("dbo.TextBlockDetails");
            DropTable("dbo.PictureBlockDetails");
        }
    }
}
