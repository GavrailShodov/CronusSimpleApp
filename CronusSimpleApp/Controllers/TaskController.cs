using Elders.Cronus.Transport.RabbitMQ.Management.Model;
using Elders.Cronus;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CronusService.Commands;
using CronusService.Identifications;
using CronusSimpleApp.Models;
using CronusService;
using Elders.Cronus.Projections;
using System.Runtime.Serialization;

namespace CronusSimpleApp.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class TaskController : ControllerBase
    {
        private readonly IPublisher<ICommand> _publisher;
        private readonly IProjectionReader _projectionReader;

        public TaskController(IPublisher<ICommand> publisher, IProjectionReader projectionReader)
        {
            _publisher = publisher;
            _projectionReader = projectionReader;
        }

        [HttpPost]
        public IActionResult CreateTask(CreateTaskRequest request)
        {
            string id = Guid.NewGuid().ToString();
            string Userid = Guid.NewGuid().ToString();
            TaskId taskId = new TaskId(id);
            UserId userId = new UserId(Userid);

            CreateTask command = new CreateTask(taskId, userId, request.Name, request.Deadline);

            if (_publisher.Publish(command) == false)
            {
                return Problem($"Unable to publish command. {command.Id}: {command.Name}");
            };

            return Ok(id);
        }

        [HttpPost]
        public IActionResult RenameTask(RenameTaskRequest request)
        {
            TaskId taskID = TaskId.Parse(request.Id);

            RenameTask command = new RenameTask(taskID, request.Name, DateTimeOffset.UtcNow);

            if (_publisher.Publish(command) == false)
            {
                return Problem($"Unable to publish command. {command.Id}: {command.Name}");
            };

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetTasksByUserIdAsync(string name)
        {

            ReadResult<TaskProjection> readResult = await _projectionReader.GetAsync<TaskProjection>(new TaskId("tenant"));

            if (readResult.IsSuccess == false)
                return NotFound();

            var gg = readResult.Data.GetTaskByName(name);


            return Ok(readResult.Data.State.Tasks.Select(x => new Data
            {
                CreatedAt = x.CreatedAt,
                Id = x.Id,
                Name = x.Name,
                Timestamp = x.Timestamp,
                UserId = x.UserId
            }));
        }

        public class Data
        {
            public string Id { get; set; }

            public string UserId { get; set; }

            public string Name { get; set; }

            public DateTimeOffset CreatedAt { get; set; }

            public DateTimeOffset Timestamp { get; set; }
        }
    }

}
