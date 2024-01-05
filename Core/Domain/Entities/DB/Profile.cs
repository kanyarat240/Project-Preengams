using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DB
{
    public class Profiles : EntityBase
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string IdCard { get; set; }
        public bool Active { get; set; }
        public virtual UserAccount User { get; set; }
        public virtual Room Rooms { get; set; }
        public ICollection<Bill> Bills { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}

