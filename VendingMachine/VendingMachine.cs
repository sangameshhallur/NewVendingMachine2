
using System;

namespace VendingMachine
{
    public class VendingMachineClass
    {
        public VendingMachineClass()
        {
            CoinSlot = new CoinCollection();
            CoinReturn = new CoinCollection();
            CoinBank = new CoinCollection();
            Inventory = new ProductCollection();
        }

        
        public CoinCollection CoinSlot { get; private set; }

        public CoinCollection CoinReturn { get; private set; }

        public CoinCollection CoinBank { get; private set; }

        public ProductCollection Inventory { get; private set; }

        public string Display 
        { 
            get
            {
                string display;

                if (!string.IsNullOrEmpty(TemporaryDisplay))
                {
                    display = TemporaryDisplay;
                    TemporaryDisplay = string.Empty;
                }
                else if(CoinSlot.Value == 0)
                {
                    if(CoinBank.Value == 0)
                    {
                        display = "EXACT CHANGE ONLY";
                    }
                    else
                    {
                        display = "INSERT COINS";
                    }
                }
                else
                {
                    display = CreateCurrencyString(CoinSlot.Value);
                }

                return display;
            }
        }
        private string TemporaryDisplay { get; set; }

        public void Insert(Coin coin, int num)
        {
            if (coin == Coin.PENNIE)
            {
                CoinReturn.Insert(coin, num);
            }
            else
            {
                CoinSlot.Insert(coin, num);
            }
        }
        public void Insert(Product product, int num)
        {
            Inventory.Insert(product, num);
        }
        public void AddToBank(Coin coin, int num)
        {
            CoinBank.Insert(coin, num);
        }
        public void Dispense(Product product)
        {
            int price = GetPrice(product);

             if(CoinSlot.Value >= price)
            {
                Inventory.Dispense(product);
                TemporaryDisplay = "Please Collect Your Product: " + product;
                Console.WriteLine(TemporaryDisplay);
                int changeDue = CoinSlot.Value - price;
                if(changeDue > 0)
                {                    
                    CoinBank.DispenseInto(CoinReturn, changeDue);
                    ReturnCoins(changeDue);
                }
                CoinSlot.EmptyInto(CoinBank);
            }
            else
            {                
                Console.WriteLine("Invalid Coid or Insufficient Coins, Please try again with different coins");
            }
            Console.WriteLine("Thank You....");
        }

        public void ReturnCoins(double changeDue)
        {
            Console.WriteLine("Please collect your change: " + changeDue);
            CoinSlot.EmptyInto(CoinReturn);
        }

        public int GetPrice(Product product)
        {
            int price = 0;
            if (product == Product.COLA)
            {
                price = 100;
            }
            else if(product == Product.CHIPS)
            {
                price = 50;
            }
            else if(product == Product.CANDY)
            {
                price = 65;
            }

            return price;
        }

        private string CreateCurrencyString(int amount)
        {
            return string.Format("{0:C}", amount / 100.0);
        }
    }
}
