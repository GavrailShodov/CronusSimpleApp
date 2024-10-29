using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CronusService.Identifications;
using System.Xml.Linq;
using Elders.Cronus;

namespace CronusService.Commands.Wallet
{
    [DataContract(Name = "3CAB86AB-0B47-4603-8162-850126240F95")]
    public class AddMoney : ICommand
    {
        public AddMoney() { }

        public AddMoney(UserId id, decimal amount, DateTimeOffset timestamp)
        {

            if (id is null) throw new ArgumentNullException(nameof(id));

            this.Id = id;
            this.Amount = amount;
            this.Timestamp = timestamp;
        }

        [DataMember(Order = 1)]
        public UserId Id { get; private set; }

        [DataMember(Order = 2)]
        public decimal Amount { get; private set; }

        [DataMember(Order = 3)]
        public DateTimeOffset Timestamp { get; private set; }
    }
}
