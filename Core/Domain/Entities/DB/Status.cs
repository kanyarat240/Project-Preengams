using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DB
{
    public class Status : EntityBase
    {
        public int Id { get; set; }
        public string StatusName { get; set; }
        public string StausGroup { get; set; }
        public ICollection<Bill> Bills { get; set; }
    }
}
