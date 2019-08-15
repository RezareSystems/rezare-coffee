using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCoffeAPI.Models
{
    [DynamoDBTable("Users")]
    public class User
    {
        [DynamoDBHashKey]
        public string UserId { get; set; }
        [DynamoDBProperty]
        public string UserName { get; set; }
        [DynamoDBProperty]
        public string DrinkCode { get; set; }
        [DynamoDBProperty]
        public string CupSizeCode { get; set; }

        [DynamoDBProperty]
        public string MilkTypeCode { get; set; }

        [DynamoDBProperty]
        public List<DrinkExtra> Extras { get; set; }
    }

    public class DrinkExtra
    {
        public string ExtraCode { get; set; }
        public int ExtraCount { get; set; }
    }
}
