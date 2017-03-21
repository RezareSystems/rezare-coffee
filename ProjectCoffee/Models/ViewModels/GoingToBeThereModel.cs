using ProjectCoffee.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectCoffee.Models.ViewModels
{
    /// <summary>
    /// The model backing the GoingToBeThere page
    /// </summary>
    public class GoingToBeThereModel
    {
        /// <summary>
        /// The user who the reminder was for
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// The time of the next meeting
        /// </summary>
        public DateTime NextMeeting { get; set; }
    }
}