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
    [DataContract(Name = "20F2B680-9D65-43D1-8B48-DC41C816BCC6")]
    public class WalletCreated : IEvent
    {
        WalletCreated() { }

        public WalletCreated(WalletId id, UserId userId, string name, decimal value, DateTimeOffset timestamp)
        {
            WalletId = id;
            Name = name;
            UserId = userId;
            Value = value;
            Timestamp = timestamp;

        }


        [DataMember(Order = 1)]
        public WalletId WalletId { get; private set; }

        [DataMember(Order = 2)]
        public UserId UserId { get; private set; }


        [DataMember(Order = 3)]
        public string Name { get; private set; }


        [DataMember(Order = 4)]
        public decimal Value { get; private set; }


        [DataMember(Order = 5)]
        public DateTimeOffset Timestamp { get; private set; }
    }
}
