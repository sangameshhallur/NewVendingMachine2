using NUnit.Framework;
using VendingMachine;

namespace VendingMachineTest
{
    [TestFixture]
    class VendingMachineTests
    {
        VendingMachineClass vendingMachine;

        [SetUp]
        public void Init()
        {
            vendingMachine = new VendingMachineClass();
        }

        [Test]
        public void AcceptCoins_QUARTERsAreAcceptedIntoCoinSlot()
        {
            vendingMachine.Insert(Coin.QUARTER, 3);
            Assert.AreEqual(3, vendingMachine.CoinSlot.Count(Coin.QUARTER));
        }

        [Test]
        public void AcceptCoins_DIMEsAreAcceptedInCoinSlot()
        {
            vendingMachine.Insert(Coin.DIME, 1);
            Assert.AreEqual(1, vendingMachine.CoinSlot.Count(Coin.DIME));
        }

        [Test]
        public void AcceptCoins_NICKELsAreAcceptedInCoinSlot()
        {
            vendingMachine.Insert(Coin.NICKEL, 10);
            Assert.AreEqual(10, vendingMachine.CoinSlot.Count(Coin.NICKEL));
        }

        [Test]
        public void AcceptCoins_PenniesAreRejectedInCoinSlot()
        {
            vendingMachine.Insert(Coin.PENNIE, 1);
            Assert.AreEqual(1, vendingMachine.CoinReturn.Count(Coin.PENNIE));
        }

        [Test]
        public void AcceptCoins_DisplayMessageWhenCoinSlotIsEmpty()
        {
            vendingMachine.AddToBank(Coin.QUARTER, 1);
            Assert.AreEqual("INSERT COINS", vendingMachine.Display);
        }

        [Test]
        public void AcceptCoins_DisplayValueWhenCoinSlotIsNotEmpty_Display25CentsWhenQUARTERIsInserted()
        {
            vendingMachine.Insert(Coin.QUARTER, 1);
            Assert.AreEqual("$0.25", vendingMachine.Display);
        }

        [Test]
        public void Product_DispenseInStockItem_ItemCountIsDecreasedWhenDispensed()
        {
            vendingMachine.Insert(Product.COLA, 2);
            vendingMachine.Insert(Coin.QUARTER, 4);
            vendingMachine.Dispense(Product.COLA);
            Assert.AreEqual(1, vendingMachine.Inventory.Count(Product.COLA));
        }

        [Test]
        public void Product_DoNotDispenseIfThereIsntEnoughMoney_ItemCountDoesntDecreaseIfNotEnoughMoney()
        {
            vendingMachine.Insert(Product.CANDY, 1);
            Assert.AreEqual(1, vendingMachine.Inventory.Count(Product.CANDY));
        }

        [Test]
        public void Product_DisplayTHANKYOUAfterDispensingItem_DisplayWillReadTHANKYOU()
        {
            vendingMachine.Insert(Product.CHIPS, 1);
            vendingMachine.Insert(Coin.QUARTER, 5);
            vendingMachine.Dispense(Product.CHIPS);
            string display = vendingMachine.Display;
            Assert.AreEqual("THANK YOU", display);
        }
        [Test]
        public void Product_DisplayReturnsToPreviousBehaviorAfterThankYouIsSeen_DisplayWillShowInsertCoinsAfterThankYou()
        {
            vendingMachine.Insert(Product.CHIPS, 1);
            vendingMachine.Insert(Coin.QUARTER, 2);
            vendingMachine.Dispense(Product.CHIPS);
            string display = vendingMachine.Display;
            display = vendingMachine.Display;
            Assert.AreEqual("INSERT COINS", display);
        }

        [Test]
        public void Product_DisplayReturnsToPreviousBehaviorAfterPriceIsDisplayed_DisplayWillShowAmountInCoinSlotAfterPrice()
        {
            vendingMachine.Insert(Product.COLA, 1);
            vendingMachine.Insert(Coin.QUARTER, 2);
            string display = vendingMachine.Display;
            display = vendingMachine.Display;
            Assert.AreEqual("$0.50", display);
        }

        [Test]
        public void MakeChange_BalanceDueAfterPurchase_DIMEIsPlacedInCoinReturn()
        {
            vendingMachine.AddToBank(Coin.DIME, 1);
            vendingMachine.Insert(Product.CANDY, 1);
            vendingMachine.Insert(Coin.QUARTER, 3);
            vendingMachine.Dispense(Product.CANDY);

            Assert.AreEqual(1, vendingMachine.CoinReturn.Count(Coin.DIME));
        }

        [Test]
        public void ReturnCoins_ReturnCoinsIsSelected_ContentsOfCoinSlotEmptyIntoCoinReturn()
        {
            vendingMachine.Insert(Coin.QUARTER, 4);
            vendingMachine.ReturnCoins();
            Assert.AreEqual(4, vendingMachine.CoinReturn.Count(Coin.QUARTER));
        }

        [Test]
        public void ReturnCoins_CoinSlotEmptied_DisplayShowsInsertCoins()
        {
            vendingMachine.AddToBank(Coin.DIME, 1);
            vendingMachine.Insert(Coin.QUARTER, 4);
            vendingMachine.ReturnCoins();
            string display = vendingMachine.Display;
            Assert.AreEqual("INSERT COINS", display);
        }

        [Test]
        public void ExactChangeOnly_HasNoMoneyToMakeChange_DisplayExactChangeOnly()
        {
            string display = vendingMachine.Display;
            Assert.AreEqual("EXACT CHANGE ONLY", display);
        }
    }
}
