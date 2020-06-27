using System;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EssenceOfMagic
{
    public class GameObject
    {
        public GameObject()
        {

        }

        [System.Text.Json.Serialization.JsonIgnore]
        public ObjectAction ActionLeft { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public ObjectAction ActionRight { get; set; }

        private string _gid;
        [System.Text.Json.Serialization.JsonIgnore]
        public string gID
        {
            get { return _gid; }
            set { _gid = value; }
        }

        private Texture _sprite;
        /// <summary>
        /// Текстура объекта
        /// </summary>
        public Texture Sprite
        {
            get
            {
                if (_sprite == null) _sprite = Textures.Get(ID);
                return _sprite;
            }
            set { _sprite = value; }
        }

        #region Animations
        public Animation CurrentAnim { get; set; }
        /// <summary>
        /// Анимации объекта
        /// </summary>
        public Animations Animations { get; set; } = new Animations();
        /// <summary>
        /// Инициализировать анимации
        /// </summary>
        /*public void InitAnims()
        {
            Animations = new Animations();
            FileInfo[] fi;
            DirectoryInfo di = new DirectoryInfo(GameData.AnimationFolder);
            fi = di.GetFiles(Sprite.ID + ".*.gif");
            for (int i = 0; i < fi.Length; i++)
            {
                string animName = fi[i].Name.Substring(Sprite.ID.Length + 1, fi[i].Name.Length - 5);
                if (fi[i].Exists)
                    Animations.Add(animName, new Animation() { SpriteFile = fi[i].FullName });
            }
        }*/
        public void BeginAnim(string ID, bool isRepeat)
        {
            Animations[ID].Start(isRepeat);
            CurrentAnim = Animations[ID];
        }
        #endregion

        private string _id;
        /// <summary>
        /// Идентификатор
        /// </summary>
        public string ID
        {
            get { return _id; }
            set
            {
                if (!String.IsNullOrEmpty(value) && !String.IsNullOrWhiteSpace(value))
                    _id = value;

                if (_id == "shop")
                {
                    ActionRight = (GameObject go, int i) =>
                    {
                        Interface.Page = InterfacePages.Shop;
                    };
                }
            }
        }

        private string _name;
        /// <summary>
        /// Словесное название объекта
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (!String.IsNullOrEmpty(value) && !String.IsNullOrWhiteSpace(value))
                    _name = value;
            }
        }

        private Location _location;
        /// <summary>
        /// Координаты объекта в мире
        /// </summary>
        public Location Location
        {
            get { return _location; }
            set
            {
                if (value.X == 512 && value.Y == 320)
                {

                }
                _location = value;
            }
        }

        private Size _size = new Size(64, 64);
        /// <summary>
        /// Размер объекта
        /// </summary>
        public Size Size { get { return _size; } set { _size = value; } }

        public GLGDIPlus.GLMultiImage GetIMG()
        {
            if (CurrentAnim != null)
                return CurrentAnim.Sprite;
            else if (Sprite != null)
                return Sprite.IMG;
            else
                return null;
        }
        public Bitmap GetBMP()
        {
            if (CurrentAnim != null)
                return CurrentAnim.Sprite.bitmap;
            else if (Sprite != null)
                return Sprite.IMG.bitmap;
            else
                return null;
        }

        private Rectangle _hitbox = new Rectangle(0, 0, 64, 64);
        public Rectangle Hitbox
        {
            get { return _hitbox; }
            set { _hitbox = value; }
        }

        private double _movespeed = 0;
        /// <summary>
        /// Скорость движения
        /// </summary>
        public double MoveSpeed
        {
            get { return _movespeed; }
            set { if (value >= 0) _movespeed = value; }
        }
        static bool isMove = false;
        public void Move(Direction Direction)
        {
            if (Direction == Direction.Null) return;
            if (!isMove)
            {
                isMove = true;
                Location oldl = new Location(_location.X, _location.Y, _location.Z);
                Location newl;
                Rectangle oldhb = GameData.HitboxConvert(_location, Size, GetBMP().Size, this.Hitbox);
                double diff;

                GameObject barrier = FindBarrier(Direction);
                Rectangle bHitbox = Rectangle.Empty;
                if (barrier != null) bHitbox = GameData.HitboxConvert(barrier.Location, barrier.Size, barrier.GetBMP().Size, barrier.Hitbox);

                if (Direction == Direction.Down || Direction == Direction.DownLeft || Direction == Direction.DownRight)
                {
                    diff = oldhb.Bottom - _location.Y;
                    newl = new Location(oldl.X, oldl.Y + MoveSpeed, oldl.Z);
                    Rectangle tHitbox = GameData.HitboxConvert(newl, Size, GetBMP().Size, this.Hitbox);
                    while (_location.Y < newl.Y)
                    {
                        if ((barrier == null || (barrier != null && bHitbox.Top > _location.Y + diff + 1) || ID == "Camera" || Hitbox == null) && Game.S)
                        {
                            _location.Y += 1;
                            OnMove?.Invoke(new Vector(_location.X - oldl.X, _location.Y - oldl.Y));
                            Thread.Sleep((int)Math.Round(1000.0 / MoveSpeed, 0));
                        }
                        else break;
                    }
                }
                if (Direction == Direction.Up || Direction == Direction.UpLeft || Direction == Direction.UpRight)
                {
                    diff = oldhb.Top - _location.Y;
                    newl = new Location(oldl.X, oldl.Y - MoveSpeed, oldl.Z);
                    Rectangle tHitbox = GameData.HitboxConvert(newl, Size, GetBMP().Size, this.Hitbox);
                    while (_location.Y > newl.Y)
                    {
                        if ((barrier == null || (barrier != null && bHitbox.Bottom < _location.Y + diff - 1) || ID == "Camera" || Hitbox == null) && Game.W)
                        {
                            _location.Y -= 1;
                            OnMove?.Invoke(new Vector(_location.X - oldl.X, _location.Y - oldl.Y));
                            Thread.Sleep((int)Math.Round(1000.0 / MoveSpeed, 0));
                        }
                        else break;
                    }
                }
                if (Direction == Direction.Right || Direction == Direction.DownRight || Direction == Direction.UpRight)
                {
                    diff = oldhb.Right - _location.X;
                    newl = new Location(oldl.X + MoveSpeed, oldl.Y, oldl.Z);
                    Rectangle tHitbox = GameData.HitboxConvert(newl, Size, GetBMP().Size, this.Hitbox);
                    while (_location.X < newl.X)
                    {
                        if ((barrier == null || (barrier != null && bHitbox.Left > _location.X + diff + 1) || ID == "Camera" || Hitbox == null) && Game.D)
                        {
                            _location.X += 1;
                            OnMove?.Invoke(new Vector(_location.X - oldl.X, _location.Y - oldl.Y));
                            Thread.Sleep((int)Math.Round(1000.0 / MoveSpeed, 0));
                        }
                        else break;
                    }
                }
                if (Direction == Direction.Left || Direction == Direction.DownLeft || Direction == Direction.UpLeft)
                {
                    diff = oldhb.Left - _location.X;
                    newl = new Location(oldl.X - MoveSpeed, oldl.Y, oldl.Z);
                    Rectangle tHitbox = GameData.HitboxConvert(newl, Size, GetBMP().Size, this.Hitbox);
                    while (_location.X > newl.X)
                    {
                        if ((barrier == null || (barrier != null && bHitbox.Right < _location.X + diff - 1) || ID == "Camera" || Hitbox == null) && Game.A)
                        {
                            _location.X -= 1;
                            OnMove?.Invoke(new Vector(_location.X - oldl.X, _location.Y - oldl.Y));
                            Thread.Sleep((int)Math.Round(1000.0 / MoveSpeed, 0));
                        }
                        else break;
                    }
                }
                isMove = false;
            }
        }
        public void Move(Vector Vector)
        {
            if (!isMove)
            {
                isMove = true;
                if (Vector.dY > 0)
                {
                    if (CheckDown()) _location.Y += Vector.dY;
                }
                else if (Vector.dY < 0)
                {
                    if (CheckUp()) _location.Y += Vector.dY;
                }
                if (Vector.dX > 0)
                {
                    if (CheckRight()) _location.X += Vector.dX;
                }
                else if (Vector.dX < 0)
                {
                    if (CheckLeft()) _location.X += Vector.dX;
                }
                Location old = new Location(_location.X, _location.Y, _location.Z);
                OnMove?.Invoke(new Vector(_location.X - old.X, _location.Y - old.Y));
                isMove = false;
            }
        }
        public delegate void ObjectMoveEventHandler(Vector Vector);
        public event ObjectMoveEventHandler OnMove;

        #region Checking path
        private GameObject FindBarrier(Direction Direction)
        {
            lock (new object())
            {
                GameObject[] barriers = new GameObject[0];
                Rectangle[] barriershb = new Rectangle[0];

                Bitmap texture;
                if (CurrentAnim != null)
                    texture = CurrentAnim.Sprite.bitmap;
                else if (Sprite != null)
                    texture = Sprite.IMG.bitmap;
                else
                    texture = new Bitmap(Size.Width, Size.Height);
                Rectangle Hitbox = GameData.HitboxConvert(Location, Size, texture.Size, this.Hitbox);

                for (int i = 0; i < GameData.World.Objects.CountAll; i++)
                {
                    lock (new object())
                    {
                        GameObject obj = GameData.World.Objects.Get(i);
                        if (gID != obj.gID)
                        {
                            Rectangle objhb = GameData.HitboxConvert(obj.Location, obj.Size, obj.Sprite.GetIMG().bitmap.Size, obj.Hitbox);
                            if (Direction == Direction.Down || Direction == Direction.DownLeft || Direction == Direction.DownRight)
                            {
                                if (Hitbox.Bottom < objhb.Top && Hitbox.Right >= objhb.Left && Hitbox.Left <= objhb.Right)
                                {
                                    Array.Resize<GameObject>(ref barriers, barriers.Length + 1);
                                    barriers[barriers.Length - 1] = obj;
                                    Array.Resize<Rectangle>(ref barriershb, barriershb.Length + 1);
                                    barriershb[barriershb.Length - 1] = objhb;
                                }
                            }
                            if (Direction == Direction.Up || Direction == Direction.UpLeft || Direction == Direction.UpRight)
                            {
                                if (Hitbox.Top > objhb.Bottom && Hitbox.Right >= objhb.Left && Hitbox.Left <= objhb.Right)
                                {
                                    Array.Resize<GameObject>(ref barriers, barriers.Length + 1);
                                    barriers[barriers.Length - 1] = obj;
                                    Array.Resize<Rectangle>(ref barriershb, barriershb.Length + 1);
                                    barriershb[barriershb.Length - 1] = objhb;
                                }
                            }
                            if (Direction == Direction.Right || Direction == Direction.DownRight || Direction == Direction.UpRight)
                            {
                                if (Hitbox.Right < objhb.Left && Hitbox.Bottom >= objhb.Top && Hitbox.Top <= objhb.Bottom)
                                {
                                    Array.Resize<GameObject>(ref barriers, barriers.Length + 1);
                                    barriers[barriers.Length - 1] = obj;
                                    Array.Resize<Rectangle>(ref barriershb, barriershb.Length + 1);
                                    barriershb[barriershb.Length - 1] = objhb;
                                }
                            }
                            if (Direction == Direction.Left || Direction == Direction.DownLeft || Direction == Direction.UpLeft)
                            {
                                if (Hitbox.Left > objhb.Right && Hitbox.Bottom >= objhb.Top && Hitbox.Top <= objhb.Bottom)
                                {
                                    Array.Resize<GameObject>(ref barriers, barriers.Length + 1);
                                    barriers[barriers.Length - 1] = obj;
                                    Array.Resize<Rectangle>(ref barriershb, barriershb.Length + 1);
                                    barriershb[barriershb.Length - 1] = objhb;
                                }
                            }
                        }
                    }
                }

                if (barriers.Length == 0 || barriers.Length != barriershb.Length) return null;

                GameObject ret = barriers[0];
                Rectangle rethb = barriershb[0];
                for (int i = 1; i < barriers.Length; i++)
                {
                    if (Direction == Direction.Down || Direction == Direction.DownLeft || Direction == Direction.DownRight)
                    {
                        if (barriershb[i].Top < rethb.Top)
                        {
                            ret = barriers[i];
                            rethb = barriershb[i];
                        }
                    }
                    if (Direction == Direction.Up || Direction == Direction.UpLeft || Direction == Direction.UpRight)
                    {
                        if (barriershb[i].Bottom > rethb.Bottom)
                        {
                            ret = barriers[i];
                            rethb = barriershb[i];
                        }
                    }
                    if (Direction == Direction.Right || Direction == Direction.DownRight || Direction == Direction.UpRight)
                    {
                        if (barriershb[i].Left < rethb.Left)
                        {
                            ret = barriers[i];
                            rethb = barriershb[i];
                        }
                    }
                    if (Direction == Direction.Left || Direction == Direction.DownLeft || Direction == Direction.UpLeft)
                    {
                        if (barriershb[i].Right > rethb.Right)
                        {
                            ret = barriers[i];
                            rethb = barriershb[i];
                        }
                    }
                }
                return ret;
            }
        }
        private bool CheckDown()
        {
            if (ID == "Camera" || this.Hitbox == null) return true;
            Bitmap texture;
            if (CurrentAnim != null)
                texture = CurrentAnim.Sprite.bitmap;
            else if (Sprite != null)
                texture = Sprite.IMG.bitmap;
            else
                texture = new Bitmap(Size.Width, Size.Height);
            Rectangle Hitbox = GameData.HitboxConvert(Location, Size, texture.Size, this.Hitbox);
            for (int o = 0; o < GameData.World.Objects.CountAll; o++)
            {
                GameObject obj = GameData.World.Objects.Get(o);
                if (gID != obj.gID)
                {
                    Rectangle objhb = GameData.HitboxConvert(obj.Location, obj.Size, obj.Sprite.GetIMG().bitmap.Size, obj.Hitbox);
                    if ((Hitbox.Bottom == objhb.Top || Hitbox.Bottom + 1 >= objhb.Top) && Hitbox.Bottom < objhb.Top &&
                        (Hitbox.Right >= objhb.Left && Hitbox.Left <= objhb.Right)) return false;
                }
            }
            return true;
        }
        private bool CheckUp()
        {
            if (ID == "Camera" || this.Hitbox == null) return true;
            Bitmap texture;
            if (CurrentAnim != null)
                texture = CurrentAnim.Sprite.bitmap;
            else if (Sprite != null)
                texture = Sprite.IMG.bitmap;
            else
                texture = new Bitmap(Size.Width, Size.Height);
            Rectangle Hitbox = GameData.HitboxConvert(Location, Size, texture.Size, this.Hitbox);
            for (int o = 0; o < GameData.World.Objects.CountAll; o++)
            {
                GameObject obj = GameData.World.Objects.Get(o);
                if (gID != obj.gID)
                {
                    Rectangle objhb = GameData.HitboxConvert(obj.Location, obj.Size, obj.Sprite.GetIMG().bitmap.Size, obj.Hitbox);
                    if ((Hitbox.Top == objhb.Bottom || Hitbox.Top - 1 <= objhb.Bottom) && Hitbox.Top > objhb.Bottom &&
                        (Hitbox.Right >= objhb.Left && Hitbox.Left <= objhb.Right)) return false;
                }
            }
            return true;
        }
        private bool CheckRight()
        {
            if (ID == "Camera" || this.Hitbox == null) return true;
            Bitmap texture;
            if (CurrentAnim != null)
                texture = CurrentAnim.Sprite.bitmap;
            else if (Sprite != null)
                texture = Sprite.IMG.bitmap;
            else
                texture = new Bitmap(Size.Width, Size.Height);
            Rectangle Hitbox = GameData.HitboxConvert(Location, Size, texture.Size, this.Hitbox);
            for (int o = 0; o < GameData.World.Objects.CountAll; o++)
            {
                GameObject obj = GameData.World.Objects.Get(o);
                if (gID != obj.gID)
                {
                    Rectangle objhb = GameData.HitboxConvert(obj.Location, obj.Size, obj.Sprite.GetIMG().bitmap.Size, obj.Hitbox);
                    if ((Hitbox.Right == objhb.Left || Hitbox.Right + 1 >= objhb.Left) && Hitbox.Right < objhb.Left &&
                        (Hitbox.Bottom >= objhb.Top && Hitbox.Top <= objhb.Bottom)) return false;
                }
            }
            return true;
        }
        private bool CheckLeft()
        {
            if (ID == "Camera" || this.Hitbox == null) return true;
            Bitmap texture;
            if (CurrentAnim != null)
                texture = CurrentAnim.Sprite.bitmap;
            else if (Sprite != null)
                texture = Sprite.IMG.bitmap;
            else
                texture = new Bitmap(Size.Width, Size.Height);
            Rectangle Hitbox = GameData.HitboxConvert(Location, Size, texture.Size, this.Hitbox);
            for (int o = 0; o < GameData.World.Objects.CountAll; o++)
            {
                GameObject obj = GameData.World.Objects.Get(o);
                if (gID != obj.gID)
                {
                    Rectangle objhb = GameData.HitboxConvert(obj.Location, obj.Size, obj.Sprite.GetIMG().bitmap.Size, obj.Hitbox);
                    if ((Hitbox.Left == objhb.Right || Hitbox.Left - 1 <= objhb.Right) && Hitbox.Left > objhb.Right &&
                        (Hitbox.Bottom >= objhb.Top && Hitbox.Top <= objhb.Bottom)) return false;
                }
            }
            return true;
        }
        #endregion

        public GameObject Clone()
        {
            GameObject temp = (GameObject)this.MemberwiseClone();
            if (Animations != null)
                temp.Animations = Animations;
            temp.Location = new Location(Location.X, Location.Y, Location.Z);
            temp.Size = new Size(Size.Width, Size.Height);
            if (Sprite != null)
                temp.Sprite = Sprite.Clone();
            return temp;
        }
    }

    public static class GameObjects
    {
        /// <summary>
        /// Массив зарегистрированных объектов
        /// </summary>
        public static GameObject[] Index;
        /// <summary>
        /// Инициализировать объекты
        /// </summary>
        public static void Init()
        {
            Index = null;
            FileInfo fi = new FileInfo(GameData.ObjectsFolder + "\\objects.json");
            using (StreamReader sr = new StreamReader(fi.FullName))
                Index = JsonSerializer.Deserialize<GameObject[]>(sr.ReadToEnd());
        }
        /// <summary>
        /// Получить объект по ID
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns>Объект или null</returns>
        public static GameObject Get(string ID)
        {
            for (int i = 0; i < Index.Length; i++)
                if (Index[i].ID == ID)
                    return Index[i];
            return null;
        }
    }

    public struct Location
    {
        public Location(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        /// <summary>
        /// Горизонтальная координата
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// Вертикальная координата
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// Высота
        /// </summary>
        public double Z { get; set; }
    }

    public struct Vector
    {
        public Vector(double dx, double dy)
        {
            dX = dx;
            dY = dy;
        }
        /// <summary>
        /// Разница по горизонтали
        /// </summary>
        public double dX { get; set; }
        /// <summary>
        /// Разница по вертикали
        /// </summary>
        public double dY { get; set; }
    }
}
