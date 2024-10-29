using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CronusService.Events;
using CronusService.Identifications;
using Elders.Cronus;

namespace CronusService
{
    public class TaskState : AggregateRootState<TaskAggregate, TaskId>
    {
        public override TaskId Id { get; set; }

        public UserId UserId { get; set; }

        public string Name { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset Deadline { get; set; }

        public void When(TaskCreated @event)
        {
            Id = @event.Id;
            UserId = @event.UserId;
            Name = @event.Name;
            CreatedAt = @event.CreatedAt;
            Deadline = @event.Timestamp;
        }

        public void When(RenameTaskEvent @event)
        {
            Name = @event.Name;
        }
    }
}
