using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Public
{
    public class Preengam : EntityBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Facebook { get; set; }
        public string Line { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
