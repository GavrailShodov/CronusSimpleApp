using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using CronusService.Aggregates;
using CronusService.Commands.Wallet;
using CronusService.Events.User;
using CronusService.Events.Wallet;
using CronusService.Identifications;
using Elders.Cronus;
using Elders.Cronus.Testing;
using Elders.Cronus.Transport.RabbitMQ.Management.Model;
using Machine.Specifications;

namespace Test
{
    [Subject("Adding Money to Wallet")]
    public class Test
    {
        static UserAggregate aggregate;
        static WalletId walletId;
        static UserId userId;
        static decimal addedAmount = 50;

        Establish context = () =>
        {
            var id = Guid.NewGuid().ToString();
            var id2 = Guid.NewGuid().ToString();
            userId = new UserId(id);
            walletId = new WalletId(id, userId);

            aggregate = Aggregate<UserAggregate>
            .FromHistory(stream => stream
            .AddEvent(new UserCreated(userId, "Test", "Test@mail", DateTimeOffset.UtcNow))
            .AddEvent(new WalletCreated(walletId, userId, "TestWallet", 100, DateTimeOffset.UtcNow)));
        };

        public class When_addinmg_money_to_existing_wallet
        {
            Because of = () => aggregate.AddMoney(walletId, addedAmount);

            It should_create_the_event = () => aggregate.IsEventPublished<CronusService.Events.Wallet.AddMoney>().ShouldBeTrue();

            It should_add_right_amount = () => aggregate.RootState().Wallet.Where(x => x.Key == walletId).First().Value.State().Amount.ShouldBeLike(150m);
        }

        public class When_creating_a_new_wallet
        {
            Establish context = () =>
            {
                var id3 = Guid.NewGuid().ToString();
                walletId = new WalletId(id3, userId);

            };

            Because of = () => aggregate.CreateWallet(walletId, userId);

            It should_create_new_event = () => aggregate.IsEventPublished<WalletCreated>().ShouldBeTrue();

            It should_contains_the_new_wallet_in_the_aggregate = () => aggregate.RootState().Wallet.Count().ShouldBeGreaterThan(1);
        }



    }
}
