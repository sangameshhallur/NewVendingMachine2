using NUnit.Framework;
using VendingMachine;

namespace VendingMachineTest
{
    [TestFixture]
    class ProductTests
    {
        VendingMachineClass vendingMachine;
        [SetUp]
        public void Init()
        {
            vendingMachine = new VendingMachineClass();
        }

        [Test]
        public void Product_ValidateColaCost_ColaCosts100()
        {
            Assert.AreEqual(100, vendingMachine.GetPrice(Product.COLA));
        }

        [Test]
        public void Product_ValidChipsCost_ChipsCost50()
        {
            Assert.AreEqual(50, vendingMachine.GetPrice(Product.CHIPS));
        }

        [Test]
        public void Product_ValidateCandyCost_CandyCosts65()
        {
            Assert.AreEqual(65, vendingMachine.GetPrice(Product.CANDY));
        }
    }
}
