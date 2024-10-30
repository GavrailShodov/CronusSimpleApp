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

        public void CreateUser(UserId id, WalletId walletId, string name, string email, DateTimeOffset timestamp)
        {
            IEvent @event = new UserCreated(id, name, email, timestamp);
            Apply(@event);
            CreateWallet(walletId, name);
        }

        public void CreateWallet(WalletId walletId, string name)
        {
            IEvent @event = new WalletCreated(walletId, state.Id, name, 0, DateTimeOffset.UtcNow);
            Apply(@event);
        }

        public void AddMoney(WalletId walletId, decimal amount)
        {
            if (state.Wallet.ContainsKey(walletId) == false)
            {
                throw new Exception("Wallet key is missing is this aggregate dictionary");
            }
            state.Wallet[walletId].AddMoney(amount, state.Id);
        }
    }
}
