namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatetimeFormat : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DateTimeFormatDetails", newName: "DateTimeFormats");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.DateTimeFormats", newName: "DateTimeFormatDetails");
        }
    }
}
