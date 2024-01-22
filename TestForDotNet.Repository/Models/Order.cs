namespace TestForDotNet.Repository.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderName { get; set; }
        public string State { get; set; }
        public ICollection<Window>? Windows { get; set; }
    }
}