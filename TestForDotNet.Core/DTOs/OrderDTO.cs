using TestForDotNet.Core.Base;

namespace TestForDotNet.Core.DTOs
{
    public class OrderDTO : DTOBaseModel
    {
        public int OrderId { get; set; }
        public string? OrderName { get; set; }
        public string? State { get; set; }
        public List<WindowDTO>? Windows { get; set; }
    }
}