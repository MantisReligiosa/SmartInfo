namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimeBlockDetails : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.DateTimeBlockDetails", name: "Fromat_Id", newName: "Format_Id");
            RenameIndex(table: "dbo.DateTimeBlockDetails", name: "IX_Fromat_Id", newName: "IX_Format_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.DateTimeBlockDetails", name: "IX_Format_Id", newName: "IX_Fromat_Id");
            RenameColumn(table: "dbo.DateTimeBlockDetails", name: "Format_Id", newName: "Fromat_Id");
        }
    }
}
