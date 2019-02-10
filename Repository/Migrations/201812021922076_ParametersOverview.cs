namespace Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ParametersOverview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Parameters", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Parameters", "Name");
        }

        public override void Down()
        {
            AddColumn("dbo.Parameters", "Name", c => c.String());
            DropColumn("dbo.Parameters", "Discriminator");
        }
    }
}
