namespace Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class MetablockDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MetablockFrames",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    MetaBlockDetails_Id = c.Guid(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MetaBlockDetails", t => t.MetaBlockDetails_Id)
                .Index(t => t.MetaBlockDetails_Id);

            AddColumn("dbo.DisplayBlocks", "MetablockFrame_Id", c => c.Guid(nullable: true));
            CreateIndex("dbo.DisplayBlocks", "MetablockFrame_Id");
            AddForeignKey("dbo.DisplayBlocks", "MetablockFrame_Id", "dbo.MetablockFrames", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.MetablockFrames", "MetaBlockDetails_Id", "dbo.MetaBlockDetails");
            DropForeignKey("dbo.DisplayBlocks", "MetablockFrame_Id", "dbo.MetablockFrames");
            DropIndex("dbo.MetablockFrames", new[] { "MetaBlockDetails_Id" });
            DropIndex("dbo.DisplayBlocks", new[] { "MetablockFrame_Id" });
            DropColumn("dbo.DisplayBlocks", "MetablockFrame_Id");
            DropTable("dbo.MetablockFrames");
        }
    }
}
