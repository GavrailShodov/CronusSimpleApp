using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CronusService.Identifications;
using Elders.Cronus;

namespace CronusService.Events
{
    [DataContract(Name = "BCC855DB-30D6-4DC6-B357-3898AB47C1C0")]
    public class RenameTaskEvent : IEvent
    {
        RenameTaskEvent() { }

        public RenameTaskEvent(TaskId id, string name, DateTimeOffset timestamp)
        {
            Id = id;
            Name = name;
            Timestamp = timestamp;
        }


        [DataMember(Order = 1)]
        public TaskId Id { get; private set; }


        [DataMember(Order = 2)]
        public string Name { get; private set; }

        [DataMember(Order = 3)]
        public DateTimeOffset Timestamp { get; private set; }
    }
}
