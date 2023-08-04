namespace VehiclePositionSearcher.Models
{
    public class VehiclePosition
    {
        public int VehicleId { get; set; }
        public string? VehicleRegistration { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public long RecordedTimeUTC { get; set; }
    }
}
