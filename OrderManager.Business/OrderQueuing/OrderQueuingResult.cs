namespace OrderManager.Business.OrderQueuing
{
    public class OrderQueuingResult : IOrderQueuingResult
    {
        public OrderQueuingStatus QueuingStatus { get; set; }
        public string Message { get; set; }
    }
}
