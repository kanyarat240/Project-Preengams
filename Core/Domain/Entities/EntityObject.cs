using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EntityObject
    {
        public enum ObjectState
        {
            Normal = 1,
            Added = 2,
            Modified = 3,
            Deleted = 4
        }
    }
}
