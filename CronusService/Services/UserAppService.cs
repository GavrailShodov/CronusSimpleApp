using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CronusService.Aggregates;
using CronusService.Commands;
using CronusService.Commands.User;
using CronusService.Commands.Wallet;
using CronusService.Events.User;
using Elders.Cronus;
using Elders.Cronus.Transport.RabbitMQ.Management.Model;

namespace CronusService.Services
{
    [DataContract(Name = "1A8A3BC4-831E-4674-A77E-F0F83BA7D436")]
    public class UserAppService : ApplicationService<UserAggregate>,
    ICommandHandler<CreateUser>,
    ICommandHandler<AddMoney>,
    ICommandHandler<CreateWallet>
    {
        public UserAppService(IAggregateRepository repository) : base(repository) { }

        public async Task HandleAsync(CreateUser command)
        {
            ReadResult<UserAggregate> UserResult = await repository.LoadAsync<UserAggregate>(command.Id).ConfigureAwait(false);
            if (UserResult.NotFound)
            {
                var user = new UserAggregate();

                user.CreateUser(command.Id, command.WalletId, command.Name, command.Email, DateTimeOffset.UtcNow);
                await repository.SaveAsync(user).ConfigureAwait(false);
            }
        }

        public async Task HandleAsync(AddMoney command)
        {
            ReadResult<UserAggregate> UserResult = await repository.LoadAsync<UserAggregate>(command.Id).ConfigureAwait(false);

            if (UserResult.IsSuccess)
            {
                var aggregate = UserResult.Data;
                aggregate.AddMoney(command.WalletId, command.Amount);
                await repository.SaveAsync(aggregate).ConfigureAwait(false);
            }
        }

        public async Task HandleAsync(CreateWallet command)
        {
            ReadResult<UserAggregate> UserResult = await repository.LoadAsync<UserAggregate>(command.Id).ConfigureAwait(false);

            if (UserResult.IsSuccess)
            {
                var aggregate = UserResult.Data;
                aggregate.CreateWallet(command.WalletId, command.WalletId.ToString());
                await repository.SaveAsync(aggregate).ConfigureAwait(false);
            }
        }
    }
}
