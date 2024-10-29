using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CronusService.Aggregates;
using CronusService.Entities;
using CronusService.Events.User;
using CronusService.Events.Wallet;
using CronusService.Identifications;
using Elders.Cronus;

namespace CronusService.States
{
    public class WalletState : EntityState<WalletId>
    {
        public override WalletId EntityId { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }

        public void When(AddMoney @event)
        {
            Amount += @event.Value;
        }
    }
}
