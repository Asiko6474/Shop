using System;
using System.Collections.Generic;
using System.Text;

namespace Shop
{
    class Shop 
    {
        int _gold;
        item[] _inventory;

        public Shop()
        {
            _gold = 2000;
            _inventory = new item[3];
        }

        public Shop(item[] items)
        {
            _gold = 30;

            _inventory = items;
        }
        
        public bool Sell(Player player, int Stock, int playerInventory)
        {
            item ItemBuy = _inventory[Stock];

            if(player.Buy(ItemBuy, playerInventory))
            {
                _gold += ItemBuy.cost;
                return true;
            }
            return false;
        }
        string[] GetItemNames()
        {

        }
    }
}
