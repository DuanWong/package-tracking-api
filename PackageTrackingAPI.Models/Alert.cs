using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageTrackingAPI.Models
{
    public class Alert
    {
        public int AlertID { get; set; }
        public int UserID { get; set; }
        public int PackageID { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public User User { get; set; }
    }
}
