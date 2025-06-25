public class TrackingEvent
{
    public int EventID { get; set; }
    public int PackageID { get; set; }
    public string Status { get; set; }
    public string Location { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    // Navigation property
    public Package Package { get; set; }
}