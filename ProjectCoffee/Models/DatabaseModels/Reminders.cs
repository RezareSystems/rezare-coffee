using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectCoffee.Models.DatabaseModels
{
    /// <summary>
    /// This is a reminder. It's a link emailed to users who specified they "WillBeThere" for the last meeting.
    /// </summary>
    public class Reminder
    {
        public Guid Id { get; set; }

        public virtual User User { get; set; }

        public DateTime CreatedOn { get; set; }

    }
}