using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DB
{
    public class Room : EntityBase
    {
        public int Id { get; set; }
        public string TypeRoom { get; set; }
        public int PiceRoom { get; set; }
        public bool Active { get; set; }
        public ICollection<Profiles> Profiles { get; set; }
        public ICollection<Electricity> Electricity { get; set; }
        public ICollection<Water> Water { get; set; }
    }
}
