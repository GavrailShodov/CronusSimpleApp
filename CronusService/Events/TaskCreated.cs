using System.Runtime.Serialization;
using CronusService.Identifications;
using Elders.Cronus;

namespace CronusService.Events
{
    [DataContract(Name = "728fc4e7-628b-4962-bd68-97c98aa05694")]
    public class TaskCreated : IEvent
    {
        TaskCreated() { }

        public TaskCreated(TaskId id, UserId userId, string name, DateTimeOffset timestamp)
        {
            Id = id;
            UserId = userId;
            Name = name;
            CreatedAt = DateTimeOffset.UtcNow;
            Timestamp = timestamp;
        }

        [DataMember(Order = 1)]
        public TaskId Id { get; private set; }

        [DataMember(Order = 2)]
        public UserId UserId { get; private set; }

        [DataMember(Order = 3)]
        public string Name { get; private set; }

        [DataMember(Order = 4)]
        public DateTimeOffset CreatedAt { get; private set; }

        [DataMember(Order = 5)]
        public DateTimeOffset Timestamp { get; private set; }

        public override string ToString()
        {
            return $"Task with id '{Id}' and name '{Name}' for user [{UserId}] at {CreatedAt} has been created.";
        }
    }
}