using System.Runtime.Serialization;
using CronusService.Entities;
using CronusService.Events.User;
using CronusService.Events.Wallet;
using CronusService.Identifications;

namespace CronusSimpleApp.Models.User
{
    public class UserOtuput
    {
        public UserOtuput()
        {
            Wallet = new Dictionary<string, WalletDto>();
        }
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public Dictionary<string, WalletDto> Wallet { get; set; }

        public DateTimeOffset Timestamp { get; set; }
    }
    public class WalletDto
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
}
