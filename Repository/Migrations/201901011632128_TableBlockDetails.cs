namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableBlockDetails : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DisplayBlocks", "Details_Id1", "dbo.TextBlockDetails");
            CreateTable(
                "dbo.TableBlockDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FontName = c.String(),
                        FontSize = c.Int(nullable: false),
                        EvenRowDetails_Id = c.Guid(),
                        HeaderDetails_Id = c.Guid(),
                        OddRowDetails_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TableBlockRowDetails", t => t.EvenRowDetails_Id)
                .ForeignKey("dbo.TableBlockRowDetails", t => t.HeaderDetails_Id)
                .ForeignKey("dbo.TableBlockRowDetails", t => t.OddRowDetails_Id)
                .Index(t => t.EvenRowDetails_Id)
                .Index(t => t.HeaderDetails_Id)
                .Index(t => t.OddRowDetails_Id);
            
            CreateTable(
                "dbo.TableCells",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Row = c.Int(nullable: false),
                        Column = c.Int(nullable: false),
                        Value = c.String(),
                        TableBlockDetailsId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TableBlockDetails", t => t.TableBlockDetailsId, cascadeDelete: true)
                .Index(t => t.TableBlockDetailsId);
            
            CreateTable(
                "dbo.TableBlockRowDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BackColor = c.String(),
                        TextColor = c.String(),
                        Align = c.Int(nullable: false),
                        Italic = c.Boolean(nullable: false),
                        Bold = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.DisplayBlocks", "Details_Id2", c => c.Guid());
            CreateIndex("dbo.DisplayBlocks", "Details_Id2");
            AddForeignKey("dbo.DisplayBlocks", "Details_Id1", "dbo.TableBlockDetails", "Id");
            AddForeignKey("dbo.DisplayBlocks", "Details_Id2", "dbo.TextBlockDetails", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DisplayBlocks", "Details_Id2", "dbo.TextBlockDetails");
            DropForeignKey("dbo.DisplayBlocks", "Details_Id1", "dbo.TableBlockDetails");
            DropForeignKey("dbo.TableBlockDetails", "OddRowDetails_Id", "dbo.TableBlockRowDetails");
            DropForeignKey("dbo.TableBlockDetails", "HeaderDetails_Id", "dbo.TableBlockRowDetails");
            DropForeignKey("dbo.TableBlockDetails", "EvenRowDetails_Id", "dbo.TableBlockRowDetails");
            DropForeignKey("dbo.TableCells", "TableBlockDetailsId", "dbo.TableBlockDetails");
            DropIndex("dbo.TableCells", new[] { "TableBlockDetailsId" });
            DropIndex("dbo.TableBlockDetails", new[] { "OddRowDetails_Id" });
            DropIndex("dbo.TableBlockDetails", new[] { "HeaderDetails_Id" });
            DropIndex("dbo.TableBlockDetails", new[] { "EvenRowDetails_Id" });
            DropIndex("dbo.DisplayBlocks", new[] { "Details_Id2" });
            DropColumn("dbo.DisplayBlocks", "Details_Id2");
            DropTable("dbo.TableBlockRowDetails");
            DropTable("dbo.TableCells");
            DropTable("dbo.TableBlockDetails");
            AddForeignKey("dbo.DisplayBlocks", "Details_Id1", "dbo.TextBlockDetails", "Id");
        }
    }
}
