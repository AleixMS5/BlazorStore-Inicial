namespace BlazorStore.Shared.Dto
{
    public class NewOrderDto: OrderDto
    {
        public string NameOnCard { get; set; }
        public string CardNumber { get; set; }
        public int ValidUntilMonth { get; set; }
        public int ValidUntilYear { get; set; }
        public string CvcCode { get; set; }
    }
}