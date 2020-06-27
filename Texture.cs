using System;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GLGDIPlus;

namespace EssenceOfMagic
{
    public class Texture : IDisposable
    {
        /// <summary>
        /// Где используется текстура. Нужно для редактора мира
        /// </summary>
        public TextureTypes Type { get; set; }
        /// <summary>
        /// Имя файла изображения, в котором находится текстура. Путь не полный. Разрешение изображения должно быть 192 пикселя на дюйм!
        /// </summary>
        public string File { get; set; }
        /// <summary>
        /// Часть изображения, являющаяся текстурой
        /// </summary>
        public Rectangle srcRect { get; set; }
        /// <summary>
        /// Размер текстуры в игре в пикселях
        /// </summary>
        public Size Size { get; set; }
        /// <summary>
        /// Идентификатор текстуры
        /// </summary>
        public string ID { get; set; }

        private GLMultiImage _img;
        [JsonIgnore]
        public GLMultiImage IMG
        {
            get
            {
                if (!isInitialized) Init();
                return _img;
            }
            set { _img = value; }
        }

        private Animation _animation;
        public Animation Animation
        {
            get { return _animation; }
            set { _animation = value; }
        }

        private bool _isanimated = false;
        public bool isAnimated
        {
            get { return _isanimated; }
            set 
            { 
                _isanimated = value;
                if (value) IMG = null;
                else Animation = null;
            }
        }

        public GLMultiImage GetIMG()
        {
            if (Animation != null)
                return Animation.Sprites[Animation.CurrentFrame];
            else
                return IMG;
        }

        private Bitmap _bmp;
        [JsonIgnore]
        public Bitmap BMP { get { return _bmp; } set { _bmp = value; } }

        [JsonIgnore]
        public bool isInitialized { get; private set; } = false;
        /// <summary>
        /// Инициализировать текстуру
        /// </summary>
        public void Init()
        {
            FileInfo FI = new FileInfo(GameData.TextureFolder + "\\" + File);
            if (FI.Exists)
            {
                Image img = Image.FromFile(GameData.TextureFolder + "\\" + File);
                _bmp = new Bitmap(Size.Width, Size.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                using (Graphics gr1 = Graphics.FromImage(_bmp))
                {
                    Image temp = new Bitmap(srcRect.Size.Width, srcRect.Size.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    ((Bitmap)temp).SetResolution(img.HorizontalResolution, img.VerticalResolution);
                    using (Graphics gr2 = Graphics.FromImage(temp))
                        gr2.DrawImage(img, 0, 0, srcRect, GraphicsUnit.Pixel);
                    gr1.DrawImage(temp, 0, 0, Size.Width, Size.Height);
                    temp.Dispose();
                }
                img.Dispose();
                _img = new GLMultiImage();
                try { _img.FromBitmap(_bmp); } catch { }
            }
            else if (new FileInfo(GameData.AnimationFolder + "\\" + File).Exists)
            {
                FI = new FileInfo(GameData.AnimationFolder + "\\" + File);
                Animation = new Animation();
                Animation.SpriteFile = FI.Name;
                Animation.Init();
                Animation.Start(true);
            }
            else
            {
                _bmp = new Bitmap(Size.Width, Size.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                _img = new GLMultiImage();
                _img.FromBitmap(_bmp);
            }
            isInitialized = true;
        }

        public void Dispose()
        {
            if (_bmp != null) _bmp.Dispose();
        }

        public Texture Clone()
        {
            Texture temp = (Texture)this.MemberwiseClone();
            if (BMP != null)
                temp.BMP = (Bitmap)BMP.Clone();
            temp.Size = new Size(Size.Width, Size.Height);
            temp.srcRect = new Rectangle(srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height);
            if (isInitialized) temp.Init();
            return temp;
        }
    }
    public static class Textures
    {
        public static Texture[] Index;
        /// <summary>
        /// Инициализировать массив зарегистрированных текстур игры
        /// </summary>
        public static void Init()
        {
            Index = null;
            FileInfo fi = new FileInfo(GameData.TextureFolder + "\\index.json");
            using (StreamReader sr = new StreamReader(fi.FullName))
                Index = JsonSerializer.Deserialize<Texture[]>(sr.ReadToEnd());
        }
        /// <summary>
        /// Получить объект текстуры
        /// </summary>
        /// <param name="ID">Идентификатор текстуры</param>
        /// <returns>Объект текстуры. Если ничего не найдено - null</returns>
        public static Texture Get(string ID)
        {
            for (int i = 0; i < Index.Length; i++)
            {
                if (Index[i].ID == ID)
                {
                    if (!Index[i].isInitialized) Index[i].Init();
                    return Index[i];
                }
            }
            return null;
        }
    }

    public enum TextureTypes
    {
        Background,
        Layer,
        Thing,
        Object,
        Creature,
        Player,
        MoveVariant
    }
}
