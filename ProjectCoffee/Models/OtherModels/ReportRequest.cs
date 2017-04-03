using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectCoffee.Models.OtherModels
{
    /// <summary>
    /// This is the model passed in by the Admin Page to generate the report
    /// </summary>
    public class ReportRequest
    {
        /// <summary>
        /// The date to generate the report for
        /// </summary>
        public DateTime forDate { get; set; }

        /// <summary>
        /// The date to set the next meeting to
        /// </summary>
        public DateTime nextMeeting { get; set; }
        
        /// <summary>
        /// The IDs of the users who should be in the report
        /// </summary>
        public List<int> userIds { get; set; }
    }
}