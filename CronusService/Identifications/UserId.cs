using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Elders.Cronus;

namespace CronusService.Identifications
{
    [DataContract(Name = "00f5463f-633a-49f4-9fbe-f98e0911c2f5")]
    public class UserId : AggregateRootId<UserId>
    {
        UserId() { }

        public UserId(string id) : base("tenant", "user", id) { }

        protected override UserId Construct(string id, string tenant)
        {
            return new UserId(id);
        }
    }
}
