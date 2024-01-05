using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string TypeRoom { get; set; }
        public int PiceRoom { get; set; }
        public bool Active { get; set; }

    }
}
