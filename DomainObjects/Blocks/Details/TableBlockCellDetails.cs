using System;

namespace DomainObjects.Blocks.Details
{
    public class TableBlockCellDetails : Identity
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public string Value { get; set; }

        public Guid TableBlockDetailsId { get; set; }
        public TableBlockDetails TableBlockDetails { get; set; }
    }
}
