namespace DataExchange.Responces
{
    public class GetVersionResponce : IResponce
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Build { get; set; }
    }
}
