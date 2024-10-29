using Elders.Cronus.Projections;
using Elders.Cronus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CronusService.Commands;
using CronusService.Identifications;
using CronusSimpleApp.Models;
using CronusSimpleApp.Models.User;
using CronusService.Commands.User;
using CronusService.Commands.Wallet;
using Elders.Cronus.Transport.RabbitMQ.Management.Model;
using CronusService;
using CronusService.Projections;

namespace CronusSimpleApp.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IPublisher<ICommand> _publisher;
        private readonly IProjectionReader _projectionReader;

        public UserController(IPublisher<ICommand> publisher, IProjectionReader projectionReader)
        {
            _publisher = publisher;
            _projectionReader = projectionReader;
        }

        [HttpPost]
        public IActionResult CreateUser(CreateUserRequest request)
        {
            string Userid = Guid.NewGuid().ToString();
            UserId userId = new UserId(Userid);

            CreateUser command = new CreateUser(userId, request.Name, request.Email, DateTimeOffset.UtcNow);

            if (_publisher.Publish(command) == false)
            {
                return Problem($"Unable to publish command. {command.Id}: {command.Name}");
            };

            return Ok(userId);
        }

        [HttpPost]
        public IActionResult AddMoney(string user, decimal amount)
        {
            UserId userId = new UserId(user);

            AddMoney command = new AddMoney(userId, amount, DateTimeOffset.UtcNow);

            if (_publisher.Publish(command) == false)
            {
                return Problem($"Unable to publish command");
            };

            return Ok(userId);
        }

        [HttpGet]
        public async Task<IActionResult> GetTasksByUserIdAsync(string userId)
        {
            UserId UserId = new UserId(userId);
            ReadResult<UserProjection> readResult = await _projectionReader.GetAsync<UserProjection>(UserId);

            if (readResult.IsSuccess == false)
                return NotFound();



            return Ok(readResult.Data.State);
        }
    }
}
