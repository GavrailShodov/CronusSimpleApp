using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Elders.Cronus;

namespace CronusService.Identifications
{
    [DataContract(Name = "F73F9E0F-C65C-4C02-9F27-621C5888910A")]
    public class WalletId : EntityId<UserId>
    {
        WalletId() { }

        public WalletId(string id, UserId idBase) : base(id, idBase, "wallet") { }
    }
}
