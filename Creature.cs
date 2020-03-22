using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;

namespace EssenceOfMagic
{
    public class Creature : GameObject
    {
        public Creature()
        {
            isDead = false;
            Vulnarable = true;

            _hpRegen();
            _needs();
            //_mpRegen();

            GameTime.OnSecond += GameTime_OnSecond;
        }

        private void GameTime_OnSecond()
        {
            if ((DateTime.Now - LastSleep).TotalMilliseconds > GameData.DayLength.TotalMilliseconds * 2)
                SlowDeath();
        }

        new public Creature Clone()
        {
            Creature temp = (Creature)this.MemberwiseClone();
            if (Animations != null)
            {
                temp.Animations = new Animations();
                temp.Animations.Count = Animations.Count;
                temp.Animations._anims = new Animation[Animations._anims.Length];
                for (int i = 0; i < Animations._anims.Length; i++)
                    if (Animations._anims[i] != null)
                        temp.Animations._anims[i] = Animations._anims[i].Clone();
            }
            /*if (Armor != null)
                temp.Armor = Armor.Clone();
            temp.DamageType = DamageType;
            temp.Location = new Location(Location.X, Location.Y, Location.Z);
            temp.Resists = Resists;
            if (SelectedWeapon != null)
                temp.SelectedWeapon = SelectedWeapon.Clone();
            if (CurrentWeapons != null)
            {
                temp.CurrentWeapons = new Weapon[CurrentWeapons.Length];
                for (int i = 0; i < CurrentWeapons.Length; i++)
                    if (CurrentWeapons[i] != null)
                        temp.CurrentWeapons[i] = CurrentWeapons[i].Clone();
            }*/
            temp.Size = new Size(Size.Width, Size.Height);
            if (Sprite != null)
                temp.Sprite = Sprite.Clone();
            return temp;
        }

        #region Life (HP, dead, vulnerable)
        private double _hp;
        public double HP
        {
            get { return _hp; }
            set
            {
                if (!Vulnarable) 
                {
                    if (_hp <= 0) _hp = 1;
                    if (_hpmax <= 0) _hpmax = 1;
                    isDead = false;
                    return; 
                }
                if (value < 0)
                    _hp = 0;
                else if (value > _hpmax)
                    _hp = _hpmax;
                else
                    _hp = value;
                if (_hp <= 0 && Vulnarable)
                    isDead = true;
            }
        }
        private double _hpmax;
        public double HPMax
        {
            get { return _hpmax; }
            set
            {
                if (value >= 0)
                    _hpmax = value;
                else
                    _hpmax = 0;
                if (_hpmax == 0) isDead = true;
                if (_hp > _hpmax) _hp = _hpmax;
            }
        }
        private bool _hpregen;
        public bool HPRegen
        {
            get { return _hpregen; }
            set { if (Vulnarable) _hpregen = value; else _hpregen = true; }
        }
        private double _hpregenspeed;
        public double HPRegenSpeed
        {
            get { return _hpregenspeed; }
            set { _hpregenspeed = value; }
        }
        private void _hpRegen()
        {
            Task.Run(() =>
            {
                if (_hpmax > 0 && !_hpregen)
                {
                    _hpregen = true;
                    while (isAlive && _hpregen)
                    {
                        if (!GameTime.isFreezed)
                        {
                            DateTime Old = DateTime.Now;
                            if (HPRegen) HP += (DateTime.Now - Old).TotalSeconds * HPRegenSpeed;
                        }
                        Thread.Sleep(1000);
                    }
                    _hpregen = false;
                }
            });
        }
        private bool _isdead;
        public bool isDead
        {
            get { return _isdead; }
            set
            {
                _isdead = value;
                _isalive = !value;
                if (_isdead)
                {
                    _hp = 0;
                    try { Animations["Death"].Start(false); } catch { }
                }
            }
        }
        private bool _isalive;
        public bool isAlive
        {
            get { return _isalive; }
            set
            {
                _isalive = value;
                _isdead = !value;
                if (_isalive)
                {
                    if (_hpmax < 1) _hpmax = 1;
                    if (_hp < 1) _hp = 1;
                    _hpRegen();
                    _needs();
                    //_mpRegen();
                }
            }
        }
        private bool _vulnarable = true;
        public bool Vulnarable
        {
            get { return _vulnarable; }
            set
            {
                _vulnarable = value;
                if (value)
                {
                    _hpRegen();
                    _needs();
                }
            }
        }
        public void SlowDeath()
        {
            if (Vulnarable)
            {
                _ = Task.Run(() =>
                {
                    _hpregen = false;
                    while (Vulnarable && 
                           (_satiety == 0 || 
                           _water == 0 || 
                           (DateTime.Now - LastSleep).TotalMilliseconds > (GameData.DayLength.TotalMilliseconds * 2)))
                    {
                        if (!GameTime.isFreezed) HP -= 2;
                        Thread.Sleep(1000);
                    }
                });
            }
        }
        public void Death()
        {
            Vulnarable = true;
            isDead = true;
        }
        #endregion

        #region Progress (level, exp, skills)
        private int _level = 1;
        public int Level
        {
            get { return _level; }
            set { if (value > 0) _level = value; }
        }

        private int _exp = 0;
        public int Exp
        {
            get { return _exp; }
            set
            {
                if (value >= 0) _exp = value;
            }
        }
        #endregion

        #region Needs (satiety, water, sleep)
        private int _satiety = 100;
        public int Satiety
        {
            get { return _satiety; }
            set 
            { 
                if (value >= 0 && value < _satietymax) _satiety = value;
                if (value == 0) SlowDeath();
            }
        }

        private int _satietymax = 100;
        public int SatietyMax
        {
            get { return _satietymax; }
            set { if (value > 0) _satietymax = value; }
        }

        private int _water = 100;
        public int Water
        {
            get { return _water; }
            set
            {
                if (value >= 0 && value < _water) _water = value;
                if (value == 0) SlowDeath();
            }
        }

        private int _watermax = 100;
        public int WaterMax
        {
            get { return _watermax; }
            set { if (value > 0) _watermax = value; }
        }

        public DateTime LastSleep { get; set; } = DateTime.Now;

        private bool isNeedsCounting = false;
        private void _needs()
        {
            if (!isNeedsCounting)
            {
                if (Vulnarable)
                {
                    _ = Task.Run(() =>
                    {
                        isNeedsCounting = true;
                        while (Vulnarable)
                        {
                            Thread.Sleep(500);
                            if (!GameTime.isFreezed)
                            {
                                Water--;
                                Satiety--;
                            }
                        }
                        isNeedsCounting = false;
                    });
                }
                else isNeedsCounting = false;
            }
        }
        #endregion

        #region deleted
        /*
        #region Magic (MP)
        private double _mp = 0;
        public double MP
        {
            get { return _mp; }
            set
            {
                if (value < 0)
                    _mp = 0;
                else if (value > MPMax)
                    _mp = MPMax;
                else
                    _mp = value;
            }
        }
        private double _mpmax;
        public double MPMax
        {
            get { return _mpmax; }
            set
            {
                if (value >= 0)
                    _mpmax = value;
                else
                    _mpmax = 0;
            }
        }
        public bool MPRegen { get; set; }
        private bool _mpregen;
        private double _mpregenspeed;
        public double MPRegenSpeed
        {
            get { return _mpregenspeed; }
            set { _mpregenspeed = value; }
        }
        private void _mpRegen()
        {
            Task.Run(() =>
            {
                if (_mpmax > 0 && !_mpregen)
                {
                    _mpregen = true;
                    while (isAlive)
                    {
                        DateTime Old = DateTime.Now;
                        if (MPRegen) MP += (DateTime.Now - Old).TotalSeconds * MPRegenSpeed;
                        Thread.Sleep(1000);
                    }
                    _mpregen = false;
                }
            });
        }
        #endregion

        #region Battle (strength, protection, vitality, willpower
        private double _strength;
        public double Strength
        {
            get { return _strength; }
            set
            {
                if (value < 0)
                    _strength = 0;
                else
                    _strength = value;
            }
        }
        private double _protection;
        public double Protection
        {
            get { return _protection; }
            set
            {
                if (value < 0)
                    _protection = 0;
                else
                    _protection = value;
            }
        }
        private double _vitality;
        public double Vitality
        {
            get { return _vitality; }
            set
            {
                if (value < 0)
                    _vitality = 0;
                else
                    _vitality = value;
            }
        }
        private double _willpower;
        public double Willpower
        {
            get { return _willpower; }
            set
            {
                if (value < 0)
                    _willpower = 0;
                else
                    _willpower = value;
            }
        }

        private double _damage;
        public double Damage
        {
            get { return _damage; }
            set
            {
                if (value < 0)
                    _damage = 0;
                else
                    _damage = value;
            }
        }
        public DamageType DamageType { get; set; }
        public Resists Resists { get; set; } = new Resists();
        public void ToDamage(Creature sender)
        {
            Random rand = new Random();

            double WillModifier = (sender.Willpower - Willpower) / 100.0;
            if (sender.Willpower > Willpower + 50)
                WillModifier *= 2;
            else if (sender.Willpower < Willpower - 50)
                WillModifier *= 0.5;
            else
                WillModifier *= 1;

            double EnemyDamage = (sender.Strength + sender.SelectedWeapon.Damage) * rand.Next(90, 110) / 100.0;
            double resist1 = 1 - Armor.Resists[sender.DamageType] - Resists[sender.DamageType];
            if (resist1 < 0) resist1 = 0;
            double resist2 = 1 - Armor.Resists[sender.SelectedWeapon.DamageType] - Resists[sender.SelectedWeapon.DamageType];
            if (resist2 < 0) resist2 = 0;
            EnemyDamage *= resist1 * resist2;

            HP = HP - EnemyDamage * WillModifier + Armor.Protection + Protection;
        }

        public Armor Armor { get; set; } = new Armor();

        public Weapon SelectedWeapon { get; set; }
        private Weapon[] _weapons;
        public Weapon[] CurrentWeapons
        {
            get { return _weapons; }
            set
            {
                if (value != null)
                    if (value.Length == 5)
                        _weapons = value;
            }
        }
        #endregion
        */
        #endregion
    }

    public static class Creatures
    {
        /// <summary>
        /// Массив зарегистрированных существ
        /// </summary>
        public static Creature[] Index;
        /// <summary>
        /// Инициализировать список существ
        /// </summary>
        public static void Init()
        {
            Index = null;
            FileInfo fi = new FileInfo(GameData.ObjectsFolder + "\\creatures.json");
            using (StreamReader sr = new StreamReader(fi.FullName))
                Index = JsonSerializer.Deserialize<Creature[]>(sr.ReadToEnd());
        }
        /// <summary>
        /// Получить существо по ID
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns>Существо или null</returns>
        public static Creature Get(string ID)
        {
            for (int i = 0; i < Index.Length; i++)
                if (Index[i].ID == ID)
                    return Index[i];
            return null;
        }
    }
}
