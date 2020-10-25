namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableSizes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TableBlockColumnWidths",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Index = c.Int(nullable: false),
                        Value = c.Int(),
                        Units = c.Int(nullable: false),
                        TableBlockDetailsId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TableBlockDetails", t => t.TableBlockDetailsId, cascadeDelete: true)
                .Index(t => t.TableBlockDetailsId);
            
            CreateTable(
                "dbo.TableBlockRowHeights",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Index = c.Int(nullable: false),
                        Value = c.Int(),
                        Units = c.Int(nullable: false),
                        TableBlockDetailsId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TableBlockDetails", t => t.TableBlockDetailsId, cascadeDelete: true)
                .Index(t => t.TableBlockDetailsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TableBlockRowHeights", "TableBlockDetailsId", "dbo.TableBlockDetails");
            DropForeignKey("dbo.TableBlockColumnWidths", "TableBlockDetailsId", "dbo.TableBlockDetails");
            DropIndex("dbo.TableBlockRowHeights", new[] { "TableBlockDetailsId" });
            DropIndex("dbo.TableBlockColumnWidths", new[] { "TableBlockDetailsId" });
            DropTable("dbo.TableBlockRowHeights");
            DropTable("dbo.TableBlockColumnWidths");
        }
    }
}
