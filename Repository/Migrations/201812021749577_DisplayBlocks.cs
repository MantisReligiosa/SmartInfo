namespace Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DisplayBlocks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DisplayBlocks",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Left = c.Int(nullable: false),
                    Top = c.Int(nullable: false),
                    Height = c.Int(nullable: false),
                    Width = c.Int(nullable: false),
                    Image = c.Binary(),
                    Text = c.String(),
                    Discriminator = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.DisplayBlocks");
        }
    }
}
