using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CronusService.Events;
using CronusService.Identifications;
using Elders.Cronus.Projections;
using Elders.Cronus;
using static CronusService.TaskProjectionData;
using CronusService.Commands;

namespace CronusService
{
    [DataContract(Name = "c94513d1-e5ee-4aae-8c0f-6e85b63a4e03")]
    public class TaskProjection : ProjectionDefinition<TaskProjectionData, TaskId>,
        IEventHandler<TaskCreated>,
        IEventHandler<RenameTaskEvent>
    {

        public TaskProjection()
        {
            Subscribe<TaskCreated>(x => new TaskId(x.Id.NID));
            Subscribe<RenameTaskEvent>(x => new TaskId(x.Id.NID));

        }

        public Task HandleAsync(TaskCreated @event)
        {
            Data task = new Data();

            task.Id = @event.Id;
            task.UserId = @event.UserId;
            task.Name = @event.Name;
            task.Timestamp = @event.Timestamp;

            State.Tasks.Add(task);

            return Task.CompletedTask;
        }

        public Task HandleAsync(RenameTaskEvent @event)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Data> TaskIdsAfter(DateTimeOffset timestamp)
        {
            return State.Tasks.Where(x => x.Timestamp > timestamp);
        }

        public IEnumerable<Data> GetTaskByName(string name)
        {
            return State.Tasks.Where(x => x.Name.Equals(name));
        }
    }

    [DataContract(Name = "481DA491-AE23-474E-A1B6-2B3E48711B25")]
    public class TaskProjectionData
    {
        public TaskProjectionData()
        {
            Tasks = new List<Data>();
        }

        [DataMember(Order = 1)]
        public List<Data> Tasks { get; set; }

        [DataContract(Name = "60942D4C-0256-4952-B152-FB2EEAC5FF78")]
        public class Data
        {
            [DataMember(Order = 1)]
            public TaskId Id { get; set; }

            [DataMember(Order = 2)]
            public UserId UserId { get; set; }

            [DataMember(Order = 3)]
            public string Name { get; set; }

            [DataMember(Order = 4)]
            public DateTimeOffset CreatedAt { get; set; }

            [DataMember(Order = 5)]
            public DateTimeOffset Timestamp { get; set; }
        }

    }
}
