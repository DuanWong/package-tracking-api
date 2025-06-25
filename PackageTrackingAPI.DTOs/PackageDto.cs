using PackageTrackingAPI.DTOs;
using System.ComponentModel.DataAnnotations;

public class PackageDto
{
    public int PackageID { get; set; }

    [Required, StringLength(50, MinimumLength = 5)]
    public string TrackingNumber { get; set; }

    [Required, Range(1, int.MaxValue)]
    public int SenderID { get; set; }

    [Required, StringLength(100)]
    public string ReceiverName { get; set; }

    public string CurrentStatus { get; set; } = "Created";
    public List<TrackingEventDto> TrackingEvents { get; set; } = new();
}