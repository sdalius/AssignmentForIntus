using TestForDotNet.Core.Base;
using UzduotysNet.Repository.Enums;

namespace TestForDotNet.Core.DTOs
{
    public class SubElementDTO : DTOBaseModel
    {
        public int SubElementId { get; set; }
        public SubElementTypeEnum SubElementType { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public int WindowId { get; set; }
        public WindowDTO? Window { get; set; }
        public int Element { get; set; }
    }
}
