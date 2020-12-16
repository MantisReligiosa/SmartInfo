﻿using DomainObjects;

namespace Web.Models.Blocks
{
    public class TableBlockColumnWidthDto
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public int? Value { get; set; }
        public SizeUnits Units { get; set; }
    }
}