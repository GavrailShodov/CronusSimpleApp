using System.Runtime.Serialization;
using System.Threading.Tasks;
using CronusService.Commands;
using Elders.Cronus;

namespace CronusService
{
    [DataContract(Name = "ef669879-5d35-4cb7-baea-39a7c46c9e13")]
    public class TaskAppService : ApplicationService<TaskAggregate>,
    ICommandHandler<CreateTask>,
    ICommandHandler<RenameTask>
    {
        public TaskAppService(IAggregateRepository repository) : base(repository) { }

        public async Task HandleAsync(CreateTask command)
        {
            ReadResult<TaskAggregate> taskResult = await repository.LoadAsync<TaskAggregate>(command.Id).ConfigureAwait(false);
            if (taskResult.NotFound)
            {
                var task = new TaskAggregate();
                task.CreateTask(command.Id, command.UserId, command.Name, DateTimeOffset.UtcNow);
                await repository.SaveAsync(task).ConfigureAwait(false);
            }
        }

        public async Task HandleAsync(RenameTask command)
        {
            ReadResult<TaskAggregate> taskResult = await repository.LoadAsync<TaskAggregate>(command.Id).ConfigureAwait(false);
            if (taskResult.IsSuccess)
            {
                var task = taskResult.Data;
                task.RenameTaskMethod(command.Name);
                await repository.SaveAsync(task).ConfigureAwait(false);
            }
        }
    }
}
