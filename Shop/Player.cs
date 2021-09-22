using System;
using System.Collections.Generic;
using System.Text;

namespace Shop
{
    class Player : Shop
    {
        int _gold;
        item[] _inventory;
        public int Gold;
        public item[] Inventory;


        public Player()
        {
            _gold = 3000;

            _inventory = new item[3];
        }

        public bool Buy(item item, int playerInventory)
        {
            if (_gold >= item.cost)
            {
                _gold -= item.cost;

                _inventory[playerInventory] = item;

                return true;
            }
            return false;
        }
    }
}
