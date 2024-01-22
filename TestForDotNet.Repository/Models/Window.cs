namespace TestForDotNet.Repository.Models
{
    public class Window
    {
        public int WindowId { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public string Name { get; set; }
        public int QuantityOfWindows { get; set; }
        public int TotalSubElements { get; set; }
        public ICollection<SubElement>? SubElements { get; set; }
    }
}
