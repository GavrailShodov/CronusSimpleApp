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


            string id = Guid.NewGuid().ToString();
            var walletid = new WalletId(id, userId);

            CreateUser command = new CreateUser(userId, walletid, request.Name, request.Email, DateTimeOffset.UtcNow);

            if (_publisher.Publish(command) == false)
            {
                return Problem($"Unable to publish command. {command.Id}: {command.Name}");
            };

            return Ok(userId + walletid);
        }

        [HttpPost]
        public IActionResult AddNewWallet(string userid)
        {
            UserId userId = new UserId(userid);

            string id = Guid.NewGuid().ToString();
            WalletId walletId = new WalletId(id, userId);

            CreateWallet command = new CreateWallet(userId, walletId, 0, DateTimeOffset.UtcNow);

            if (_publisher.Publish(command) == false)
            {
                return Problem($"Unable to publish command");
            };

            return Ok(userId + "|  |" + walletId);
        }

        [HttpPost]
        public IActionResult AddMoney(string user, string wallet, decimal amount)
        {
            UserId userId = new UserId(user);
            WalletId walletId = new WalletId(wallet, userId);

            AddMoney command = new AddMoney(userId, walletId, amount, DateTimeOffset.UtcNow);

            if (_publisher.Publish(command) == false)
            {
                return Problem($"Unable to publish command");
            };

            return Ok(userId);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserByUserIdAsync(string userId)
        {
            UserId UserId = new UserId(userId);
            ReadResult<UserProjection> readResult = await _projectionReader.GetAsync<UserProjection>(UserId);

            if (readResult.IsSuccess == false)
                return NotFound();

            var output = new UserOtuput();
            output.Id = readResult.Data.State.Id.ToString();
            output.Name = readResult.Data.State.Name;
            output.Email = readResult.Data.State.Email;
            output.Timestamp = readResult.Data.State.Timestamp;
            foreach (var kvp in readResult.Data.State.Wallet)
            {
                var wallet = new WalletDto();
                wallet.Name = kvp.Value.Name;
                wallet.Amount = kvp.Value.Amount;
                output.Wallet.Add(kvp.Key.ToString(), wallet);
            }

            return Ok(output);

        }
    }
}
