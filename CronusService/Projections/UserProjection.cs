using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CronusService.Entities;
using CronusService.Events;
using CronusService.Events.User;
using CronusService.Events.Wallet;
using CronusService.Identifications;
using Elders.Cronus;
using Elders.Cronus.Projections;
using static CronusService.Projections.UserProjectionData;

namespace CronusService.Projections
{
    [DataContract(Name = "f124a9b5-3ca0-4407-98d6-8bcc47d3bd12")]
    public class UserProjection : ProjectionDefinition<UserProjectionData, UserId>,
        IEventHandler<UserCreated>,
        IEventHandler<WalletCreated>,
        IEventHandler<AddMoney>
    {

        public UserProjection()
        {
            Subscribe<UserCreated>(x => x.Id);
            Subscribe<AddMoney>(x => x.UserId);
            Subscribe<WalletCreated>(x => x.UserId);
        }


        public Task HandleAsync(UserCreated @event)
        {
            State.Wallet = new Dictionary<WalletId, WalletDto>();

            State.Id = @event.Id;
            State.Name = @event.Name;
            State.Email = @event.Email;
            State.Timestamp = @event.Timestamp;

            return Task.CompletedTask;
        }

        public Task HandleAsync(AddMoney @event)
        {
            State.Wallet[@event.WalletId].Amount += @event.Value;

            return Task.CompletedTask;

        }

        public Task HandleAsync(WalletCreated @event)
        {
            var wallet = new WalletDto();
            wallet.Name = @event.Name;
            wallet.Amount = 0;
            State.Wallet.Add(@event.WalletId, wallet);

            return Task.CompletedTask;

        }
    }

    [DataContract(Name = "3CA13CDD-DB27-479A-BD0F-760DE1ECAA49")]
    public class UserProjectionData
    {
        [DataMember(Order = 1)]
        public UserId Id { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; }

        [DataMember(Order = 3)]
        public string Email { get; set; }

        [DataMember(Order = 4)]
        public Dictionary<WalletId, WalletDto> Wallet { get; set; }

        [DataMember(Order = 5)]
        public DateTimeOffset Timestamp { get; set; }

        [DataContract(Name = "C8753773-662E-4BED-9DDC-C0F77C580744")]
        public class WalletDto
        {

            [DataMember(Order = 1)]
            public string Name { get; set; }

            [DataMember(Order = 2)]
            public decimal Amount { get; set; }
        }


    }
}