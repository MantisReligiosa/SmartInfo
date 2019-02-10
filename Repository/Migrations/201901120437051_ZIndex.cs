namespace Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ZIndex : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DisplayBlocks", "ZIndex", c => c.Int(nullable: false, defaultValue: 0));
        }

        public override void Down()
        {
            DropColumn("dbo.DisplayBlocks", "ZIndex");
        }
    }
}
