using Newtonsoft.Json;
using ProjectCoffee.Models.OtherModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectCoffee.Models.DatabaseModels
{
    /// <summary>
    /// This represents a coffee report generated at a fixed point in time
    /// </summary>
    public class CoffeeReport
    {
        /// <summary>
        /// The Id of the report
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The user who generated the report
        /// </summary>
        public virtual User GeneratedBy { get; set; }

        [ForeignKey("GeneratedBy")]
        public int GeneratedBy_Id { get; set; }

        /// <summary>
        /// The time the report was generated
        /// </summary>
        public DateTime GeneratedOn { get; set; }

        /// <summary>
        /// The date the meeting was scheduled for
        /// </summary>
        public DateTime GeneratedFor { get; set; }

        /// <summary>
        /// The report itself
        /// </summary>
        [NotMapped]
        public List<ReportStruct> Report
        {
            get
            {
                if (ReportJson == null) return new List<ReportStruct>();
                return JsonConvert.DeserializeObject<List<ReportStruct>>(ReportJson);
            }
            set
            {
                ReportJson = JsonConvert.SerializeObject(value);
            }
        }

        /// <summary>
        /// The JSON blob that represents the report in the database
        /// </summary>
        public string ReportJson { get; set; }
    }
}