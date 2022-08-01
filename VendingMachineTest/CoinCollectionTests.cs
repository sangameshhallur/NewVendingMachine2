using NUnit.Framework;
using VendingMachine;

namespace VendingMachineTest
{
    [TestFixture]
    class CoinCollectionTests
    {
        VendingMachineClass vendingMachine;

        [SetUp]
        public void Init()
        {
            vendingMachine = new VendingMachineClass();
        }

        [Test]
        public void Coin_InsertQuarter()
        {
            vendingMachine.Insert(Coin.QUARTER, 1);
            Assert.AreEqual(25, vendingMachine.CoinSlot.Value);
        }

        [Test]
        public void Coin_InsertDime()
        {
            vendingMachine.Insert(Coin.DIME, 1);
            Assert.AreEqual(10, vendingMachine.CoinSlot.Value);
        }

        [Test]
        public void Coin_InsertNickel()
        {
            vendingMachine.Insert(Coin.NICKEL, 1);
            Assert.AreEqual(5, vendingMachine.CoinSlot.Value);
        }

        [Test]
        public void Coin_InsertPenny()
        {
            vendingMachine.Insert(Coin.PENNIE, 1);
            Assert.AreEqual(100, vendingMachine.CoinReturn.Value);
        }

        [Test]
        public void Coin_StartsEmpty()
        {
            Assert.AreEqual(0, vendingMachine.CoinSlot.Value);
        }

        [Test]
        public void Coin_AddingCoinsAccumulatesValue()
        {
            vendingMachine.Insert(Coin.QUARTER, 1);
            vendingMachine.Insert(Coin.NICKEL, 1);
            vendingMachine.Insert(Coin.DIME, 1);
            vendingMachine.Insert(Coin.PENNIE, 1);
            Assert.AreEqual(40, vendingMachine.CoinSlot.Value);
        }

        [Test]
        public void Coin_EmptyingIntoAnotherCollectionAddsCoinsToTheOtherCollection()
        {
            vendingMachine.Insert(Product.CHIPS, 1);
            vendingMachine.Insert(Coin.QUARTER, 2);
            vendingMachine.Dispense(Product.CHIPS);
            Assert.AreEqual(2, vendingMachine.CoinBank.Count(Coin.QUARTER));
        }
    }
}
