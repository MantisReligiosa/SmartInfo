namespace DomainObjects
{
    public class DateTimeFormatDetails : Identity
    {
        public string Denomination { get; set; }
        public string ShowtimeFormat { get; set; }
        public string DesigntimeFormat { get; set; }
        public bool IsDateFormat { get; set; }
    }
}
