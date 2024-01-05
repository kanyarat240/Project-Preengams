using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DB
{
    public class Bill : EntityBase
    {
        public int Id { get; set; }
        public int WaterId { get; set; }
        public int ElectricityId { get; set; }
        public int StatusId { get; set; }
        public string Fine { get; set; }
        public string CommonFee { get; set; }
        public DateTime BillingDate { get; set; }
        public int ProfileId { get; set;}
        public virtual Water Waters { get; set; }
        public virtual Electricity Electricity { get; set; }
        public virtual Status Status { get; set; }
        public virtual Profiles Profiles { get; set; }
    }
}
