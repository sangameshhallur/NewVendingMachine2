using System;
using System.Collections.Generic;

namespace VendingMachine
{
    public class CoinCollection
    {
        public CoinCollection()
        {
            Coins = new Dictionary<Coin, int>();
        }


        public int Value
        {
            get
            {
                return
                    Count(Coin.QUARTER) * CoinValue(Coin.QUARTER) +
                    Count(Coin.DIME) * CoinValue(Coin.DIME) +
                    Count(Coin.NICKEL) * CoinValue(Coin.NICKEL) +
                    Count(Coin.PENNIE) * CoinValue(Coin.PENNIE);
            }
        }

        private Dictionary<Coin, int> Coins { get; set; }


        internal void Insert(Coin coin, int num)
        {
            if(Coins.ContainsKey(coin))
            {
                Coins[coin] += num;
            }
            else
            {
                Coins.Add(coin, num);
            }
        }
        private void Remove(Coin coin, int num)
        {
            if(Coins.ContainsKey(coin))
            {
                Coins[coin] -= Math.Min(num, Coins[coin]);
            }
        }
        internal void EmptyInto(CoinCollection collection)
        {
            foreach(var kvp in Coins)
            {
                collection.Insert(kvp.Key, kvp.Value);
            }
            Coins.Clear();
        }

        internal void DispenseInto(CoinCollection collection, int amount)
        {
            int numQuartersDispensed = DispenseCoinInto(collection, Coin.QUARTER, amount);
            amount -= numQuartersDispensed * CoinValue(Coin.QUARTER);

            int numDimesDispensed = DispenseCoinInto(collection, Coin.DIME, amount);
            amount -= numDimesDispensed * CoinValue(Coin.DIME);

            int numNickelsDispensed = DispenseCoinInto(collection, Coin.NICKEL, amount);
            amount -= numNickelsDispensed * CoinValue(Coin.NICKEL);

            int numPenniesDispensed = DispenseCoinInto(collection, Coin.PENNIE, amount);
            amount -= numPenniesDispensed * CoinValue(Coin.PENNIE);
        }

        public int Count(Coin coin)
        {
            int count = 0;
            if(Coins.ContainsKey(coin))
            {
                count = Coins[coin];
            }

            return count;
        }

        public int CoinValue(Coin coin)
        {
            int value = 0;

            if (coin == Coin.QUARTER)
            {
                value = 25;
            }
            else if (coin == Coin.DIME)
            {
                value = 10;
            }
            else if (coin == Coin.NICKEL)
            {
                value = 5;
            }
            else if (coin == Coin.PENNIE)
            {
                value = 100;
            }

            return value;
        }
        private int DispenseCoinInto(CoinCollection collection, Coin coin, int amount)
        {
            int numDispensed = Math.Min(amount / CoinValue(coin), Count(coin));
            collection.Insert(coin, numDispensed);
            Remove(coin, numDispensed);

            return numDispensed;
        }
    }
}
