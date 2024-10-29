using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CronusService.Identifications;
using Elders.Cronus;
using Elders.Cronus.Transport.RabbitMQ.Management.Model;

namespace CronusService.Commands
{
    [DataContract(Name = "857d960c-4b91-49cc-98fd-fa543906c52d")]
    public class CreateTask : ICommand
    {
        public CreateTask() { }

        public CreateTask(TaskId id, UserId userId, string name, DateTimeOffset timestamp)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));
            if (userId is null) throw new ArgumentNullException(nameof(userId));
            if (name is null) throw new ArgumentNullException(nameof(name));
            if (timestamp == default) throw new ArgumentNullException(nameof(timestamp));

            Id = id;
            UserId = userId;
            Name = name;
            Timestamp = timestamp;
        }

        [DataMember(Order = 1)]
        public TaskId Id { get; private set; }

        [DataMember(Order = 2)]
        public UserId UserId { get; private set; }

        [DataMember(Order = 3)]
        public string Name { get; private set; }

        [DataMember(Order = 4)]
        public DateTimeOffset Timestamp { get; private set; }

        public override string ToString()
        {
            return $"Create a task with id '{Id}' and name '{Name}' for user [{UserId}].";
        }
    }
}
