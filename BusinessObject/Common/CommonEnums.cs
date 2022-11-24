using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Common
{
    public static class CommonEnums
    {
        public static class USER_ROLE_ID
        {
            public const string ADMINISTRATOR = "F54ED2C8-2FD6-44E0-AC2F-61FE5A466B0E";
            public const string USER = "09F7343C-67F9-474F-8186-1C9CDC78E1FF";
            public const string HR = "22FC848E-6B93-43D4-A911-05370D8A0DBF";
            public const string INTERVIEWER = "B98D2F1D-A8EA-4FAA-A9EE-267A6EE7F92B";
        }

        public static class USER_ROLE
        {
            public const string ADMINISTRATOR = "ADMINISTRATOR";
            public const string USER = "USER";
            public const string HR = "HR";
            public const string INTERVIEWER = "INTERVIEWER";
        }

        public static class POST_STATUS
        {
            public const int Available = 1;
            public const int Deleted = 0;
            public const int Closed = 2;
            public const int Pending = 3; // stop receiving form
        }

        public static class APOST_STATUS
        {
            public const int Pending = 0;
            public const int Aproved = 1;
            public const int Reject = 2;
            public const int InProgress = 3; // stop receiving form
        }
    }
}
