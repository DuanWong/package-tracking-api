using System.ComponentModel.DataAnnotations;

public class TrackingEventDto
{
    public int EventID { get; set; }

    [Required, Range(1, int.MaxValue)]
    public int PackageID { get; set; }

    [Required, StringLength(20)]
    public string Status { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}