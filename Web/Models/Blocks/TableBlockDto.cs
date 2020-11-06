﻿namespace Web.Models.Blocks
{
    public class TableBlockDto : BlockDto
    {
        public string Font { get; set; }
        public int FontSize { get; set; }
        public double FontIndex { get; set; }
        public RowStyleDto HeaderStyle { get; set; }
        public RowStyleDto EvenStyle { get; set; }
        public RowStyleDto OddStyle { get; set; }
        public string[] Header { get; set; }
        public RowDto[] Rows { get; set; }
        public TableBlockRowHeightDto[] RowHeights { get; set; }
        public TableBlockColumnWidthDto[] ColumnWidths { get; set; }
    }
}