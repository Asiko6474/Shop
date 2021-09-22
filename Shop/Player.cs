using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Shop
{
    class Player
    {
        int _gold;
        item[] _inventory;
        public int Gold()
        {
            return _gold;
        }


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
        public item[] GetInventory()
        {
            return _inventory;
        }
        public virtual void Save(StreamWriter writer)
        {
            writer.WriteLine(_gold);
            writer.WriteLine(_inventory);
        }
    }
}
