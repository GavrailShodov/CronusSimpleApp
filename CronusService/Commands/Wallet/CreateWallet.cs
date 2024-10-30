using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CronusService.Identifications;
using Elders.Cronus;

namespace CronusService.Commands.Wallet
{
    [DataContract(Name = "4b4784d0-e56a-44a7-b202-b53c7a6c9b5b")]
    public class CreateWallet : ICommand
    {
        public CreateWallet() { }

        public CreateWallet(UserId id, WalletId walletId, decimal amount, DateTimeOffset timestamp)
        {

            if (id is null) throw new ArgumentNullException(nameof(id));

            this.Id = id;
            this.Amount = amount;
            this.Timestamp = timestamp;
            WalletId = walletId;
        }

        [DataMember(Order = 1)]
        public UserId Id { get; private set; }

        [DataMember(Order = 2)]
        public WalletId WalletId { get; private set; }

        [DataMember(Order = 3)]
        public decimal Amount { get; private set; }

        [DataMember(Order = 4)]
        public DateTimeOffset Timestamp { get; private set; }
    }
}
