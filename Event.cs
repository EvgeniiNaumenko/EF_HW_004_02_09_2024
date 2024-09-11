using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_005_04_09_2024
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<EventGuest> Relations { get; set; }
    }
}
