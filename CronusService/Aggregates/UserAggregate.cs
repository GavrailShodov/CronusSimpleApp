using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CronusService.Entities;
using CronusService.Events;
using CronusService.Events.User;
using CronusService.Events.Wallet;
using CronusService.Identifications;
using CronusService.States;
using Elders.Cronus;

namespace CronusService.Aggregates
{
    public class UserAggregate : AggregateRoot<UserState>
    {
        public UserAggregate() { }

        public void CreateUser(UserId id, string name, string email, DateTimeOffset timestamp)
        {
            IEvent @event = new UserCreated(id, name, email, timestamp);
            Apply(@event);
            CreateWallet(name);
        }

        private void CreateWallet(string name)
        {
            string id = Guid.NewGuid().ToString();
            var walletid = new WalletId(id, state.Id);
            IEvent @event = new WalletCreated(walletid, state.Id, name, 0, DateTimeOffset.UtcNow);
            Apply(@event);
        }

        public void AddMoney(decimal amount)
        {
            state.Wallet.AddMoney(amount, state.Id);
        }
    }
}
