using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class ElectricityDTO
    {
        public int Id { get; set; }
        public decimal UnitElectricity { get; set; }
        public int UnitUsed { get; set; }
        public int RoomId { get; set; }
        public DateOnly ElectricityMonth { get; set; }


    }
}

