using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StatusEnum
{
    
    public class StatusName
    {
        public const string WaitingForAssign = "WaitingForAssign";
        public const string Assigned = "Assigned";
        public const string WaitingforConfirmInterview = "WaitingforConfirmInterview";
        public const string WaitingForInterview = "WaitingForInterview";
        public const string UnderConsideration = "UnderConsideration";
        public const string Reject = "Reject";
        public const string Approved = "Approved";
        public const string Cancel = "Cancel";
        public const string Active = "Active";
        public const string InActive = "InActive";
        public const string Incoming = "Incoming";
        public const string Complete = "Complete";

    }

    public class StatusCode
    {
        //Status Company applicant
        public const string WaitingForAssign = "01";
        public const string Assigned = "02";
        public const string WaitingforConfirmInterview = "03";
        public const string WaitingForInterview = "04";
        public const string UnderConsideration = "05";
        public const string Reject = "06";
        public const string Approved = "07";
        public const string Cancel = "08";
        //Status Trainee
        public const string Active = "01";
        public const string InActive = "02";
        public const string Incoming = "03";
        public const string Complete = "04";



    }


    public class StatusGroupCode
    {
        //Status Company applicant
        public const string CompanyApplicant = "001";
        public const string Trainee = "002";

    }


}
