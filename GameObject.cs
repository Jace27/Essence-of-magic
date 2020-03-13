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
        public GameObject() { }

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
        public Animations Animations { get; set; }
        /// <summary>
        /// Инициализировать анимации
        /// </summary>
        public void InitAnims()
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
        }
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
            GameTime.WaitForTick();
            if (!isMove)
            {
                isMove = true;
                Location old = new Location(_location.X, _location.Y, _location.Z);
                if (Direction == Direction.Down || Direction == Direction.DownLeft || Direction == Direction.DownRight)
                    _location.Y += MoveSpeed;
                if (Direction == Direction.Up || Direction == Direction.UpLeft || Direction == Direction.UpRight)
                    _location.Y -= MoveSpeed;
                if (Direction == Direction.Right || Direction == Direction.DownRight || Direction == Direction.UpRight)
                    _location.X += MoveSpeed;
                if (Direction == Direction.Left || Direction == Direction.DownLeft || Direction == Direction.UpLeft)
                    _location.X -= MoveSpeed;
                try { _ = Task.Run(() => { try { OnMove(new Vector(_location.X - old.X, _location.Y - old.Y)); } catch { } }); } catch { }
                isMove = false;
            }
        }
        public void Move(Vector Vector)
        {
            GameTime.WaitForTick();
            if (!isMove)
            {
                isMove = true;
                Location old = new Location(_location.X, _location.Y, _location.Z);
                _location.X += Vector.dX;
                _location.Y += Vector.dY;
                try { Task.Run(() => { try { OnMove(new Vector(_location.X - old.X, _location.Y - old.Y)); } catch { } }); } catch { }
                isMove = false;
            }
        }
        public delegate void ObjectMoveEventHandler(Vector Vector);
        public event ObjectMoveEventHandler OnMove;



        /*public static bool operator ==(GameObject A, GameObject B)
        {
            if (A == null && B == null)
                return true;
            else if (A == null || B == null)
                return false;
            if (A.ID == B.ID &&
                A.Location.X == B.Location.X && A.Location.Y == B.Location.Y && A.Location.Z == B.Location.Z &&
                A.MoveSpeed == B.MoveSpeed &&
                A.Name == B.Name &&
                A.Size == B.Size &&
                A.Sprite.ID == B.Sprite.ID)
                return true;
            else
                return false;
        }
        public static bool operator !=(GameObject A, GameObject B)
        {
            if (A == null && B == null) 
                return false;
            else if (A == null || B == null)
                return true;
            if (A.ID != B.ID ||
                A.Location.X != B.Location.X || A.Location.Y != B.Location.Y || A.Location.Z != B.Location.Z ||
                A.MoveSpeed != B.MoveSpeed ||
                A.Name != B.Name ||
                A.Size != B.Size ||
                A.Sprite.ID != B.Sprite.ID)
                return true;
            else
                return false;
        }*/

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
