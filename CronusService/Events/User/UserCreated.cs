using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CronusService.Entities;
using CronusService.Identifications;
using Elders.Cronus;

namespace CronusService.Events.User
{
    [DataContract(Name = "35EF3955-80C4-482F-94C8-B3E09FA3F604")]
    public class UserCreated : IEvent
    {
        UserCreated() { }

        public UserCreated(UserId id, string name, string email, DateTimeOffset timestamp)
        {
            Id = id;
            Name = name;
            Email = email;
            Timestamp = timestamp;

        }

        [DataMember(Order = 1)]
        public UserId Id { get; private set; }


        [DataMember(Order = 2)]
        public string Name { get; private set; }


        [DataMember(Order = 3)]
        public string Email { get; private set; }


        [DataMember(Order = 4)]
        public DateTimeOffset Timestamp { get; private set; }
    }
}
