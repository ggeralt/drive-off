using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarShared
{
    public class PictureViewModel
    {
        public int Id { get; set; }
        public bool IsProfile { get; set; }
        public byte[] ImageData { get; set; }
        public VehicleViewModel? VehicleViewModel { get; set; }
    }
}
