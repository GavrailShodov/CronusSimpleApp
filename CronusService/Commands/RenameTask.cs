using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CronusService.Identifications;
using Elders.Cronus;

namespace CronusService.Commands
{
    [DataContract(Name = "B72C41A8-0E1C-40CE-8315-5D88ADC88662")]
    public class RenameTask : ICommand
    {
        public RenameTask() { }

        public RenameTask(TaskId id, string name, DateTimeOffset timestamp)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));
            if (name is null) throw new ArgumentNullException(nameof(name));
            if (timestamp == default) throw new ArgumentNullException(nameof(timestamp));

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
