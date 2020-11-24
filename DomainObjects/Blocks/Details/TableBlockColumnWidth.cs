using System;

namespace DomainObjects.Blocks.Details
{
    public class TableBlockColumnWidth : Identity
    {
        public TableBlockColumnWidth() { }

        public TableBlockColumnWidth(TableBlockColumnWidth source)
        {
            Value = source.Value;
            Units = source.Units;
            Index = source.Index;
        }

        public int Index { get; set; }

        private int? _value;
        public int? Value
        {
            get
            {
                return (Units == SizeUnits.Auto) ? null : _value;
            }
            set
            {
                _value = value;
            }
        }

        public SizeUnits Units { get; set; }


        public int TableBlockDetailsId { get; set; }
        public TableBlockDetails TableBlockDetails { get; set; }
    }
}
