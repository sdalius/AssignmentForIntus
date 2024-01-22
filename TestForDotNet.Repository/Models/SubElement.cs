namespace TestForDotNet.Repository.Models
{
    public class SubElement
    {
        public int SubElementId { get; set; }
        public int WindowId { get; set; }
        public Window? Window { get; set; }
        public int SubElementType { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public int Element { get; set; }
    }
}