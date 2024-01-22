namespace TestForDotNet.ViewModel
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string? OrderName { get; set; }
        public string? State { get; set; }
        public List<WindowViewModel>? Windows { get; set; }
        public bool isExpanded { get; set; } = false;
        public bool IsChanged { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsNew { get; set; }
    }
}
