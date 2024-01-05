using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class EnumStatus
    {
        public string Active = "Active";
        public string InActive = "InActive";
        public string Incoming = "Incoming";
        public string WaitingForAssigng = "Waiting For Assign";
        public string Reject = "Reject";
        public string Approve = "Approve";
        public string WaitingForResults = "Waiting For Results";
        public string WaitingForInterview = "Waiting For Interview";
        public string Assign = "Assign";
        public string WaitingforConfirmInterview = "Waiting for Confirm Interview";
    }


}
