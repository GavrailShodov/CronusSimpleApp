using System.Runtime.Serialization;
using Elders.Cronus;

namespace CronusService.Identifications
{
    [DataContract(Name = "2C407369-8E2F-46D9-B00F-877171343100")]
    public class NameId : AggregateRootId
    {
        NameId() { }

        public NameId(string name) : base("tenant", "user", name) { }
    }
}
