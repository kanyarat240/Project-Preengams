using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DB
{
    public class Electricity : EntityBase
    {
        public int Id { get; set; }
        public decimal UnitElectricity { get; set; }
        public int UnitUsed { get; set; }
        public int RoomId { get; set; }
        public virtual Room Rooms { get; set; }
        public DateOnly ElectricityMonth { get; set; }
        public ICollection<Bill> Bills { get; set; }
    }
}
