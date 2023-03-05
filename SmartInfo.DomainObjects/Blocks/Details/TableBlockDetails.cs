namespace DomainObjects.Blocks.Details;

public class TableBlockDetails : Identity, ICopyable<TableBlockDetails>
{
    public string FontName { get; set; }
    public int FontSize { get; set; }
    public double FontIndex { get; set; }
    public TableBlockHeaderDetails HeaderDetails { get; set; }
    public TableBlockEvenRowDetails EvenRowDetails { get; set; }
    public TableBlockOddRowDetails OddRowDetails { get; set; }
    public ICollection<TableBlockCellDetails> Cells { get; set; }
    public ICollection<TableBlockRowHeight> TableBlockRowHeights { get; set; }
    public ICollection<TableBlockColumnWidth> TableBlockColumnWidths { get; set; }

    public TableBlockDetails() { }

    public TableBlockDetails(TableBlockDetails source)
    {
        CopyFrom(source);
    }

    public void CopyFrom(TableBlockDetails source)
    {
        if (HeaderDetails == null)
        {
            HeaderDetails = new TableBlockHeaderDetails();
        }
        if (EvenRowDetails == null)
        {
            EvenRowDetails = new TableBlockEvenRowDetails();
        }
        if (OddRowDetails == null)
        {
            OddRowDetails = new TableBlockOddRowDetails();
        }
        HeaderDetails.CopyFrom(source.HeaderDetails);
        EvenRowDetails.CopyFrom(source.EvenRowDetails);
        OddRowDetails.CopyFrom(source.OddRowDetails);
        FontName = source.FontName;
        FontSize = source.FontSize;
        FontIndex = source.FontIndex;
        Cells = new List<TableBlockCellDetails>();
        foreach (var cell in source.Cells)
        {
            Cells.Add(new TableBlockCellDetails(cell));
        }
        TableBlockRowHeights = new List<TableBlockRowHeight>();
        foreach(var rowHeight in source.TableBlockRowHeights)
        {
            TableBlockRowHeights.Add(new TableBlockRowHeight(rowHeight));
        }
        TableBlockColumnWidths = new List<TableBlockColumnWidth>();
        foreach(var columnWidth in source.TableBlockColumnWidths)
        {
            TableBlockColumnWidths.Add(new TableBlockColumnWidth(columnWidth));
        }
    }
}