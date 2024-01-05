using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DB
{
    public class Payment : EntityBase
    {
        public int Id { get; set; }
        public string BillPicture { get; set; }
        public int ProfileId { get; set; }
        public DateTime DateAndTimeOfPayment { get; set; }
        public virtual Profiles Profiles { get; set; }

    }
}
