namespace OrderManager.Business.OrderQueuing
{
    public interface IOrderQueuingResult
    {
        string Message { get; set; }
        OrderQueuingStatus QueuingStatus { get; set; }
    }
}