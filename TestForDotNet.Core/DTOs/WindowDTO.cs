using TestForDotNet.Core.Base;

namespace TestForDotNet.Core.DTOs
{
    public class WindowDTO : DTOBaseModel
    {
        public int WindowId { get; set; }
        public int QuantityOfWindows { get; set; }
        public int TotalSubElements { get; set; }
        public int OrderId { get; set; }
        public string Name { get; set; }
        public OrderDTO? Order { get; set; }
        public List<SubElementDTO>? SubElements { get; set; }
    }
}