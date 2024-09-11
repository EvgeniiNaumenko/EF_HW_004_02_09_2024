using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_005_04_09_2024
{
    public class GuestRole
    {
        public int Id { get; set; }
        public string RoleName { get; set;}

        public ICollection<EventGuest> Relations { get; set; }
    }
}
