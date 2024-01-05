using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Entities.EntityObject;

namespace Domain.DTOs
{
    public class UserAbilityDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Score { get; set; }
        public string Group { get; set; }
        public Guid? ProfileId { get; set; }

        public ObjectState ObjectState { get; set; }
        

    }
}
