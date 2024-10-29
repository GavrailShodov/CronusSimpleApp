using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CronusService.Aggregates;
using CronusService.Events.User;
using CronusService.Events.Wallet;
using CronusService.Identifications;
using CronusService.States;
using Elders.Cronus;

namespace CronusService.Entities
{
    public class Wallet : Entity<UserAggregate, WalletState>
    {

        public Wallet(UserAggregate root, WalletId entityId, string name, decimal amount) : base(root, entityId)
        {
            state.EntityId = entityId;
            state.Name = name;
            state.Amount = amount;
        }

        public void AddMoney(decimal value, UserId userId)
        {

            if (value > 0)
            {
                IEvent @event = new AddMoney(state.EntityId, userId, value, DateTimeOffset.UtcNow);
                Apply(@event);
            }


        }
    }
}
