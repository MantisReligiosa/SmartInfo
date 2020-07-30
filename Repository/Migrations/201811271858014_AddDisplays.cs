namespace Repository.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddDisplays : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Displays",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Height = c.Int(nullable: false),
                        Left = c.Int(nullable: false),
                        Top = c.Int(nullable: false),
                        Width = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Displays");
        }
    }
}
