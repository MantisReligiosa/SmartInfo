namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DisplayBlocksToMetaFrames : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DisplayBlocks", "MetablockFrame_Id", "dbo.MetablockFrames");
            DropIndex("dbo.DisplayBlocks", new[] { "MetablockFrame_Id" });
            RenameColumn(table: "dbo.DisplayBlocks", name: "MetablockFrame_Id", newName: "MetablockFrameId");
            AlterColumn("dbo.DisplayBlocks", "MetablockFrameId", c => c.Guid(nullable: true));
            CreateIndex("dbo.DisplayBlocks", "MetablockFrameId");
            AddForeignKey("dbo.DisplayBlocks", "MetablockFrameId", "dbo.MetablockFrames", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DisplayBlocks", "MetablockFrameId", "dbo.MetablockFrames");
            DropIndex("dbo.DisplayBlocks", new[] { "MetablockFrameId" });
            AlterColumn("dbo.DisplayBlocks", "MetablockFrameId", c => c.Guid());
            RenameColumn(table: "dbo.DisplayBlocks", name: "MetablockFrameId", newName: "MetablockFrame_Id");
            CreateIndex("dbo.DisplayBlocks", "MetablockFrame_Id");
            AddForeignKey("dbo.DisplayBlocks", "MetablockFrame_Id", "dbo.MetablockFrames", "Id");
        }
    }
}
