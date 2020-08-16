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
                        ZIndex = c.Int(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Details_Id = c.Guid(),
                        Details_Id1 = c.Guid(),
                        Details_Id2 = c.Guid(),
                        Details_Id3 = c.Guid(),
                        Details_Id4 = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DateTimeBlockDetails", t => t.Details_Id)
                .ForeignKey("dbo.MetaBlockDetails", t => t.Details_Id1)
                .ForeignKey("dbo.PictureBlockDetails", t => t.Details_Id2)
                .ForeignKey("dbo.TableBlockDetails", t => t.Details_Id3)
                .ForeignKey("dbo.TextBlockDetails", t => t.Details_Id4)
                .Index(t => t.Details_Id)
                .Index(t => t.Details_Id1)
                .Index(t => t.Details_Id2)
                .Index(t => t.Details_Id3)
                .Index(t => t.Details_Id4);
            
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
                        Fromat_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DateTimeFormatDetails", t => t.Fromat_Id)
                .Index(t => t.Fromat_Id);
            
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
            
            CreateTable(
                "dbo.MetaBlockDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PictureBlockDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Base64Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TableBlockDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FontName = c.String(),
                        FontSize = c.Int(nullable: false),
                        FontIndex = c.Double(nullable: false),
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
            
            CreateTable(
                "dbo.TextBlockDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Text = c.String(),
                        BackColor = c.String(),
                        TextColor = c.String(),
                        FontName = c.String(),
                        FontSize = c.Int(nullable: false),
                        Align = c.Int(nullable: false),
                        Italic = c.Boolean(nullable: false),
                        Bold = c.Boolean(nullable: false),
                        FontIndex = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Parameters", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Parameters", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Parameters", "Name", c => c.String());
            DropForeignKey("dbo.DisplayBlocks", "Details_Id4", "dbo.TextBlockDetails");
            DropForeignKey("dbo.DisplayBlocks", "Details_Id3", "dbo.TableBlockDetails");
            DropForeignKey("dbo.TableBlockDetails", "OddRowDetails_Id", "dbo.TableBlockRowDetails");
            DropForeignKey("dbo.TableBlockDetails", "HeaderDetails_Id", "dbo.TableBlockRowDetails");
            DropForeignKey("dbo.TableBlockDetails", "EvenRowDetails_Id", "dbo.TableBlockRowDetails");
            DropForeignKey("dbo.TableCells", "TableBlockDetailsId", "dbo.TableBlockDetails");
            DropForeignKey("dbo.DisplayBlocks", "Details_Id2", "dbo.PictureBlockDetails");
            DropForeignKey("dbo.DisplayBlocks", "Details_Id1", "dbo.MetaBlockDetails");
            DropForeignKey("dbo.DisplayBlocks", "Details_Id", "dbo.DateTimeBlockDetails");
            DropForeignKey("dbo.DateTimeBlockDetails", "Fromat_Id", "dbo.DateTimeFormatDetails");
            DropIndex("dbo.TableCells", new[] { "TableBlockDetailsId" });
            DropIndex("dbo.TableBlockDetails", new[] { "OddRowDetails_Id" });
            DropIndex("dbo.TableBlockDetails", new[] { "HeaderDetails_Id" });
            DropIndex("dbo.TableBlockDetails", new[] { "EvenRowDetails_Id" });
            DropIndex("dbo.DateTimeBlockDetails", new[] { "Fromat_Id" });
            DropIndex("dbo.DisplayBlocks", new[] { "Details_Id4" });
            DropIndex("dbo.DisplayBlocks", new[] { "Details_Id3" });
            DropIndex("dbo.DisplayBlocks", new[] { "Details_Id2" });
            DropIndex("dbo.DisplayBlocks", new[] { "Details_Id1" });
            DropIndex("dbo.DisplayBlocks", new[] { "Details_Id" });
            DropColumn("dbo.Parameters", "Discriminator");
            DropTable("dbo.TextBlockDetails");
            DropTable("dbo.TableBlockRowDetails");
            DropTable("dbo.TableCells");
            DropTable("dbo.TableBlockDetails");
            DropTable("dbo.PictureBlockDetails");
            DropTable("dbo.MetaBlockDetails");
            DropTable("dbo.DateTimeFormatDetails");
            DropTable("dbo.DateTimeBlockDetails");
            DropTable("dbo.DisplayBlocks");
        }
    }
}
