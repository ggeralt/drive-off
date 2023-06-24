using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarShared
{
    public class ReservationView
    {
        public int Id { get; set; }
        [Required]
        public DateTime DateFrom { get; set; }

        [Required]
        public DateTime DateTo { get; set; }
        [Required]
        public int VehicleId { get; set; }
        public string Vehicle { get; set; }
    }
}
