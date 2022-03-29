namespace LocationCapture.Core.Domain
{
    public class Placement
    {
        public int Id { get; set; }
        public string? Vehicle { get; set; }
        public DateTime TimeStamp { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
