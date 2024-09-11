using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_005_04_09_2024
{
    public class EventGuest
    {
        public int EventId { get; set; }
        public Event Event { get; set; }

        public int GuestId { get; set; }
        public Guest Guest { get; set; }

        public int GuestRoleId { get; set; }
        public GuestRole Role { get; set; }
    }
}
