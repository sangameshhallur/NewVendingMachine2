using System;

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMachineClass vm = new VendingMachineClass();
            Console.WriteLine("Please select the coins");
            CoinCollection coinCollection = new CoinCollection();
            foreach (var item in Enum.GetNames(typeof(Coin)))
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("----------------------------------------");
            string value = Console.ReadLine();
            Console.WriteLine("Please enter the number of coins");
            int numberOfCoins = Convert.ToInt32(Console.ReadLine());
            Coin coin;
            foreach (var item in Enum.GetNames(typeof(Coin)))
            {
                if (Enum.TryParse<Coin>(value.ToUpper(), out coin))
                {
                    vm.Insert(coin, numberOfCoins);
                    vm.AddToBank(coin, numberOfCoins);
                    break;
                }
            }
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Please select the product");
            foreach (var item in Enum.GetNames(typeof(Product)))
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("----------------------------------------");
            string prod = Console.ReadLine();
            Product product;
            foreach (var item in Enum.GetNames(typeof(Product)))
            {
                if (Enum.TryParse<Product>(prod.ToUpper(), out product))
                {
                    vm.Dispense(product);
                    break;
                }
            }
            Console.ReadLine();
        }
    }
}
