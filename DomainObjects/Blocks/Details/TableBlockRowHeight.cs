using System;

namespace DomainObjects.Blocks.Details
{
    public class TableBlockRowHeight : Identity
    {
        public TableBlockRowHeight() { }

        public TableBlockRowHeight(TableBlockRowHeight source)
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
                if (Units == SizeUnits.Auto)
                    _value = null;
                else
                    _value = value ?? 0;
            }
        }

        public SizeUnits Units { get; set; }

        public Guid TableBlockDetailsId { get; set; }
        public TableBlockDetails TableBlockDetails { get; set; }
    }
}
