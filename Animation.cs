using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using GLGDIPlus;

namespace EssenceOfMagic
{
    public static class Gif
    {
        /// <summary>
        /// Парсер GIF для цикла foreach
        /// </summary>
        /// <param name="gif">GIF изображение</param>
        /// <returns>Перечисление для foreach</returns>
        public static IEnumerable<Image> Frames(Image gif)
        {
            var d = new FrameDimension(gif.FrameDimensionsList[0]);
            for (var i = 0; i < gif.GetFrameCount(d); i++)
            {
                gif.SelectActiveFrame(d, i);
                var ci = new Bitmap(gif.Width, gif.Height);
                using (var g = Graphics.FromImage(ci))
                    g.DrawImageUnscaled(gif, 0, 0);
                yield return ci;
            }
        }
    }

    public class Animation : IDisposable
    {
        public bool isInitialized { get; set; } = false;

        /// <summary>
        /// Инициализация анимации
        /// </summary>
        public void Init()
        {
            FileInfo FI = new FileInfo(GameData.AnimationFolder + "\\" + SpriteFile);
            if (FI.Exists)
            {
                _sprites = new GLMultiImage[0];
                foreach (Bitmap frame in Gif.Frames(Image.FromFile(FI.FullName)))
                {
                    Array.Resize<GLMultiImage>(ref _sprites, _sprites.Length + 1);
                    _sprites[_sprites.Length - 1] = new GLMultiImage();
                    try { _sprites[_sprites.Length - 1].FromBitmap((Bitmap)frame); } catch { }
                }
                FrameCount = _sprites.Length;
                Sprite = _sprites[0];

                ID = FI.Name.Substring(0, FI.Name.IndexOf(".gif"));
                AnimDuration = Animations.Get(ID).AnimDuration;

                isInitialized = true;
            }
        }

        /// <summary>
        /// Идентификатор анимации
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Полный путь к файлу анимации
        /// </summary>
        public string SpriteFile { get; set; }

        /// <summary>
        /// Текущий кадр
        /// </summary>
        [JsonIgnore]
        public GLMultiImage Sprite { get; private set; }

        private GLMultiImage[] _sprites;
        /// <summary>
        /// Массив кадров анимации
        /// </summary>
        [JsonIgnore]
        public GLMultiImage[] Sprites
        {
            get { return _sprites; }
            set { _sprites = value; FrameCount = _sprites.Length; }
        }

        /// <summary>
        /// Количество кадров
        /// </summary>
        [JsonIgnore]
        public int FrameCount { get; private set; } = 0;

        private int _currentframe = 0;
        /// <summary>
        /// Текущий кадр
        /// </summary>
        [JsonIgnore]
        public int CurrentFrame
        {
            get { return _currentframe; }
            set
            {
                if (value < 0)
                {
                    value %= FrameCount;
                    value += FrameCount;
                }
                if (value >= FrameCount)
                {
                    value %= FrameCount;
                }
                _currentframe = value;
                Sprite = Sprites[_currentframe];
            }
        }

        /// <summary>
        /// Продолжительность анимации в секундах
        /// </summary>
        public double AnimDuration { get; set; } = 1; //секунды

        /// <summary>
        /// Начинает анимацию. Анимацию необходимо инициализировать с помощью Init()
        /// </summary>
        /// <param name="isRepeat">true - зациклить анимацию, false - не зацикливать</param>
        public void Start(bool isRepeat)
        {
            double delay = AnimDuration * 1000.0 / FrameCount;
            CurrentPlay = Task.Run(() =>
            {
                while (CurrentFrame < FrameCount)
                {
                    GameTime.WaitForTick();
                    DateTime first = DateTime.Now;
                    CurrentFrame++;
                    while ((DateTime.Now - first).TotalMilliseconds < (delay * (CurrentFrame + 1))) Thread.Sleep(20);
                    if (isRepeat && CurrentFrame == FrameCount) CurrentFrame = 0;
                }
            });
        }

        public void Dispose()
        {
            CurrentPlay.Dispose();
            for (int i = 0; i < _sprites.Length; i++)
                _sprites[i].Free();
            Sprite.Free();
        }

        /// <summary>
        /// Задача выполняемой анимации
        /// </summary>
        [JsonIgnore]
        public Task CurrentPlay { get; private set; }

        public Animation Clone()
        {
            Animation temp = (Animation)this.MemberwiseClone();
            if (Sprites != null)
            {
                temp.Sprites = new GLMultiImage[Sprites.Length];
                for (int i = 0; i < Sprites.Length; i++)
                    temp.Sprites[i].FromBitmap((Bitmap)Sprites[i].bitmap.Clone());
                temp.Sprite = temp.Sprites[0];
            }
            return temp;
        }
    }

    public class Animations
    {
        public Animation[] _anims = new Animation[0];
        public string[] _animsid = new string[0];

        /// <summary>
        /// Количество анимаций
        /// </summary>
        public int Count { get; set; } = 0;

        /// <summary>
        /// Добавить анимацию в список
        /// </summary>
        /// <param name="ID">Уникальное имя анимации</param>
        /// <param name="Anim">Анимация</param>
        public void Add(string ID, Animation Anim)
        {
            for (int i = 0; i < _animsid.Length; i++) if (_animsid[i] == ID) return;
            Count++;
            Array.Resize<string>(ref _animsid, Count);
            Array.Resize<Animation>(ref _anims, Count);
            _animsid[Count - 1] = ID;
            _anims[Count - 1] = Anim;
        }
        /// <summary>
        /// Удалить анимацию из списка
        /// </summary>
        /// <param name="ID">Имя удаляемой анимации</param>
        public void Delete(string ID)
        {
            for (int i = 0; i < _animsid.Length; i++)
            {
                if (_animsid[i] == ID)
                {
                    for (int l = i; l < _animsid.Length - 1; l++)
                    {
                        _animsid[l] = _animsid[l + 1];
                        _anims[l] = _anims[l + 1];
                    }
                    break;
                }
            }
            Count--;
            Array.Resize<string>(ref _animsid, Count);
            Array.Resize<Animation>(ref _anims, Count);
        }
        public Animation this[string ID]
        {
            get
            {
                for (int i = 0; i < _animsid.Length; i++)
                    if (_animsid[i] == ID)
                        return _anims[i];
                return null;
            }
        }

        public static Animation[] Index;
        public static void Init()
        {
            using (StreamReader sr = new StreamReader(GameData.AnimationFolder + "\\index.json"))
                Index = JsonSerializer.Deserialize<Animation[]>(sr.ReadToEnd());
        }
        public static Animation Get(string ID)
        {
            for (int i = 0; i < Index.Length; i++)
                if (Index[i].ID == ID)
                    return Index[i];
            return null;
        }
    }
}
