using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CronusService.Commands;
using CronusService.Events;
using CronusService.Identifications;
using Elders.Cronus;
using Elders.Cronus.Transport.RabbitMQ.Management.Model;

namespace CronusService
{
    public class TaskAggregate : AggregateRoot<TaskState>
    {
        public TaskAggregate() { }

        public void CreateTask(TaskId id, UserId userId, string name, DateTimeOffset deadline)
        {
            IEvent @event = new TaskCreated(id, userId, name, deadline);
            Apply(@event);
        }

        public void RenameTaskMethod(string name)
        {
            if (state.Name.Equals(name))
            {
                return;
            }
            IEvent @event = new RenameTaskEvent(state.Id, name, DateTimeOffset.UtcNow);
            Apply(@event);
        }
    }
}
