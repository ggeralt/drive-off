using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarShared
{
    public class ReviewViewModel
    {
        public int? Id { get; set; }
        public string? User { get; set; }
        public string Description { get; set; }
        public int? VehicleId { get; set; }
    }
}
