using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Elders.Cronus;

namespace CronusService.Identifications
{
    [DataContract(Name = "12dc51ee-4f84-494e-9174-f142472b4cc8")]
    public class TaskId : AggregateRootId<TaskId>
    {
        TaskId() { }

        public TaskId(string id) : base("tenant", "task", id) { }

        protected override TaskId Construct(string id, string tenant)
        {
            return new TaskId(id);
        }
    }
}
