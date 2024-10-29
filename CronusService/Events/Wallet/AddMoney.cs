using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CronusService.Identifications;
using Elders.Cronus;

namespace CronusService.Events.Wallet
{
    [DataContract(Name = "4E8CB9EA-73CE-4E9F-8913-AB447B47BFF9")]
    public class AddMoney : IEvent
    {
        AddMoney() { }

        public AddMoney(WalletId id, UserId userId, decimal value, DateTimeOffset timestamp)
        {
            WalletId = id;
            UserId = userId;
            Value = value;
            Timestamp = timestamp;

        }

        [DataMember(Order = 1)]
        public WalletId WalletId { get; private set; }

        [DataMember(Order = 2)]
        public UserId UserId { get; private set; }


        [DataMember(Order = 3)]
        public decimal Value { get; private set; }


        [DataMember(Order = 4)]
        public DateTimeOffset Timestamp { get; private set; }
    }
}
