namespace Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class fontIndex : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TableBlockDetails", "FontIndex", c => c.Double(nullable: false, defaultValue: 1.5));
            AddColumn("dbo.TextBlockDetails", "FontIndex", c => c.Double(nullable: false, defaultValue: 1.5));
        }

        public override void Down()
        {
            DropColumn("dbo.TextBlockDetails", "FontIndex");
            DropColumn("dbo.TableBlockDetails", "FontIndex");
        }
    }
}
