using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CronusService.Entities;
using CronusService.Identifications;
using Elders.Cronus;
using Elders.Cronus.Transport.RabbitMQ.Management.Model;

namespace CronusService.Commands.User
{
    [DataContract(Name = "5853700F-9C76-4565-B21B-61CB3C547F8D")]
    public class CreateUser : ICommand
    {
        public CreateUser() { }

        public CreateUser(UserId id, WalletId walletId, string name, string email, DateTimeOffset timestamp)
        {

            if (id is null) throw new ArgumentNullException(nameof(id));
            if (name is null) throw new ArgumentNullException(nameof(name));
            if (email is null) throw new ArgumentNullException(nameof(email));
            if (timestamp == default) throw new ArgumentNullException(nameof(timestamp));

            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Timestamp = timestamp;
            WalletId = walletId;
        }

        [DataMember(Order = 1)]
        public UserId Id { get; private set; }


        [DataMember(Order = 2)]
        public WalletId WalletId { get; private set; }

        [DataMember(Order = 3)]
        public string Name { get; private set; }


        [DataMember(Order = 4)]
        public string Email { get; private set; }

        [DataMember(Order = 5)]
        public DateTimeOffset Timestamp { get; private set; }

        public override string ToString()
        {
            return $"Create a user with id '{Id}' and name '{Name}'.";
        }
    }
}
