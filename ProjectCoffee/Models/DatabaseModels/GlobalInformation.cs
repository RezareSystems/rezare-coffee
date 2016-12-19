using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectCoffee.Models.DatabaseModels
{
    /// <summary>
    /// Represents information that is global. There should only be one instance of this in the database
    /// </summary>
    public class GlobalInformation
    {
        /// <summary>
        /// The ID of the record in the database
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The date of the meeting (this is what the users' "WillBeThere" field refers to)
        /// </summary>
        public DateTime MeetingDate { get; set; }
    }
}