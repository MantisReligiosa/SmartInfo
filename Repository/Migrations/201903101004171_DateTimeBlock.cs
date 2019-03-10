namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimeBlock : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DateTimeBlockDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BackColor = c.String(),
                        TextColor = c.String(),
                        FontName = c.String(),
                        FontSize = c.Int(nullable: false),
                        Align = c.Int(nullable: false),
                        Italic = c.Boolean(nullable: false),
                        Bold = c.Boolean(nullable: false),
                        FontIndex = c.Double(nullable: false),
                        TimeBeforeDate = c.Boolean(nullable: false),
                        DateFromat_Id = c.Guid(),
                        TimeFromat_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DateTimeFormatDetails", t => t.DateFromat_Id)
                .ForeignKey("dbo.DateTimeFormatDetails", t => t.TimeFromat_Id)
                .Index(t => t.DateFromat_Id)
                .Index(t => t.TimeFromat_Id);
            
            CreateTable(
                "dbo.DateTimeFormatDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Denomination = c.String(),
                        ShowtimeFormat = c.String(),
                        DesigntimeFormat = c.String(),
                        IsDateFormat = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.DisplayBlocks", "Details_Id3", c => c.Guid());
            CreateIndex("dbo.DisplayBlocks", "Details_Id3");
            AddForeignKey("dbo.DisplayBlocks", "Details_Id3", "dbo.DateTimeBlockDetails", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DisplayBlocks", "Details_Id3", "dbo.TextBlockDetails");
            DropForeignKey("dbo.DateTimeBlockDetails", "TimeFromat_Id", "dbo.DateTimeFormatDetails");
            DropForeignKey("dbo.DateTimeBlockDetails", "DateFromat_Id", "dbo.DateTimeFormatDetails");
            DropIndex("dbo.DateTimeBlockDetails", new[] { "TimeFromat_Id" });
            DropIndex("dbo.DateTimeBlockDetails", new[] { "DateFromat_Id" });
            DropIndex("dbo.DisplayBlocks", new[] { "Details_Id3" });
            DropColumn("dbo.DisplayBlocks", "Details_Id3");
            DropTable("dbo.DateTimeFormatDetails");
            DropTable("dbo.DateTimeBlockDetails");
        }
    }
}
