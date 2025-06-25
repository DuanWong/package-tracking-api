using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageTrackingAPI.DTOs
{
    public class AlertDto
    {
        public int AlertID { get; set; }

        [Required(ErrorMessage = "UserID is required.")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "PackageID is required.")]
        public int PackageID { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        [StringLength(500, ErrorMessage = "Message cannot exceed 500 characters.")]
        public string Message { get; set; }

        [Required(ErrorMessage = "Timestamp is required.")]
        public DateTime Timestamp { get; set; }
    }
}
