using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CronusService.Aggregates;
using CronusService.Entities;
using CronusService.Events;
using CronusService.Events.User;
using CronusService.Events.Wallet;
using CronusService.Identifications;
using Elders.Cronus;

namespace CronusService.States
{
    public class UserState : AggregateRootState<UserAggregate, UserId>
    {
        public override UserId Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public Wallet Wallet { get; set; }

        public DateTimeOffset Timestamp { get; private set; }

        public void When(UserCreated @event)
        {
            Id = @event.Id;
            Name = @event.Name;
            Email = @event.Email;
            Timestamp = @event.Timestamp;
        }

        public void When(WalletCreated @event)
        {
            Wallet = new Wallet(Root, @event.WalletId, @event.Name, @event.Value);
        }
    }
}
