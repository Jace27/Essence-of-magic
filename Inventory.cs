using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EssenceOfMagic
{
    public class Inventory
    {
        private Item[] _backpack = new Item[Settings.BackpackCapacity];
        public Item[] Backpack
        {
            get { return _backpack; }
            set { _backpack = value; }
        }
        private Item[] _hotbar = new Item[Settings.HotbarCapacity];
        public Item[] Hotbar
        {
            get { return _hotbar; }
            set { _hotbar = value; }
        }
        private Item[] _equipment = new Item[Settings.EquipmentCapacity];
        public Item[] Equipment
        {
            get { return _equipment; }
            set
            {
                if (value != null)
                    for (int i = 0; i < value.Length; i++)
                        if (value[i] != null)
                            if (value[i].Type != ItemType.Armor)
                                return;
                _equipment = value;
            }
        }

        public int CountAll
        {
            get { return _backpack.Length + _hotbar.Length + _equipment.Length; }
        }
        public int CountBackpack
        {
            get { return _backpack.Length; }
        }
        public int CountHotbar
        {
            get { return _hotbar.Length; }
        }
        public int CountEquipment
        {
            get { return _equipment.Length; }
        }

        private int _money = 0;
        public int Money
        {
            get { return _money; }
            set { if (value >= 0) _money = value; else _money = 0; }
        }

        public void Give(Item Item)
        {
            int freecell = -1;
            for (int i = 0; i < _backpack.Length; i++)
            {
                if (_backpack[i].ID == Item.ID && Item.Stackable && _backpack[i].Stackable)
                {
                    _backpack[i].Count += Item.Count;
                    return;
                }
                if (_backpack[i] == null && freecell == -1) freecell = i;
            }
            if (freecell != -1)
            {
                _backpack[freecell] = Item;
                return;
            }
            GameData.World.Objects.Add(GameData.ItemToObject(Item, GameData.Game.Player.Location));
        }
        public void Set(int Slot, Item Item)
        {
            if (Slot >= _backpack.Length)
            {
                Slot -= _backpack.Length;
                if (Slot >= _hotbar.Length)
                {
                    Slot -= _hotbar.Length;
                    if (Slot < _equipment.Length)
                        _equipment[Slot] = Item;
                }
                else
                {
                    _hotbar[Slot] = Item;
                }
            }
            else
            {
                _backpack[Slot] = Item;
            }
        }
        public void Move(int From, int To)
        {
            Item item = null;
            int _from = From, _to = To;

            if (_from >= _backpack.Length)
            {
                _from -= _backpack.Length;
                if (_from >= _hotbar.Length)
                {
                    _from -= _hotbar.Length;
                    if (_from < _equipment.Length)
                        item = _equipment[_from];
                    else return;
                }
                else item = _hotbar[_from];
            }
            else item = _backpack[_from];

            if (item != null)
            {
                if (_to >= _backpack.Length)
                {
                    _to -= _backpack.Length;
                    if (_to >= _hotbar.Length)
                    {
                        _to -= _hotbar.Length;
                        if (_to < _equipment.Length)
                            _equipment[_to] = item;
                        else return;
                    }
                    else _hotbar[_to] = item;
                }
                else _backpack[_to] = item;
            }

            _from = From; _to = To;
            if (_from >= _backpack.Length)
            {
                _from -= _backpack.Length;
                if (_from >= _hotbar.Length)
                {
                    _from -= _hotbar.Length;
                    if (_from < _equipment.Length)
                        _equipment[_from] = null;
                    else return;
                }
                else _hotbar[_from] = null;
            }
            else _backpack[_from] = null;
        }
    }
}
