using PackageTrackingAPI.Models;

public class Package
{
    public int PackageID { get; set; }
    public string TrackingNumber { get; set; }
    public int SenderID { get; set; }
    public string ReceiverName { get; set; }
    public string ReceiverAddress { get; set; }
    public string CurrentStatus { get; set; } = "Created";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public User Sender { get; set; }
    public List<TrackingEvent> TrackingEvents { get; set; } = new();
}