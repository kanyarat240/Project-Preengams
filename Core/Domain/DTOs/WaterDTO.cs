using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class WaterDTO
    {
        public int Id { get; set; }
        public decimal UnitWater { get; set; }
        public int UnitUsed { get; set; }
        public int RoomId { get; set; }
        public DateOnly WaterMonth { get; set; }


    }
}
