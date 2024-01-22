using TestForDotNet.Core.Base;
using TestForDotNet.Core.DTOs;

namespace TestForDotNet.ViewModel
{
    public class WindowViewModel : ViewModelBase
    {
        public int WindowId { get; set; }
        public int QuantityOfWindows { get; set; }
        public int TotalSubElements { get; set; }
        public int OrderId { get; set; }
        public OrderDTO? Order { get; set; }
        public List<SubElementDTO>? SubElements { get; set; }
        public bool isExpanded { get; set; } = false;
        public string Name { get; set; }
    }
}
