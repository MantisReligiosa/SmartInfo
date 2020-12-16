using DomainObjects;

namespace Web.Models.Blocks
{
    public class TableBlockRowHeightDto
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public int? Value { get; set; }
        public SizeUnits Units { get; set; }
    }
}