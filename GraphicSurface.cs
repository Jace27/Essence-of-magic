using GLGDIPlus;
using OpenTK;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EssenceOfMagic
{
    public partial class GraphicSurface : GLControl
    {
        public delegate void ThreadTransit(GLGraphics g);
        // ============================================================
        bool mIsLoaded = false;

        GLGraphics mGraphics = new GLGraphics();
        // ============================================================
        public GraphicSurface()
        {
            InitializeComponent();
            SetStyle(ControlStyles.Selectable, false);
        }
        // ============================================================
        protected override void OnPaint(PaintEventArgs e)
        {
            DateTime t1 = DateTime.Now;

            if (this.DesignMode)    // в режиме дизайна просто закрашиваем контрол цветом
            {
                e.Graphics.Clear(this.BackColor);
                e.Graphics.Flush();
                return;
            }

            if (!mIsLoaded)     // если OpenGL контекст еще не создан
                return;

            MakeCurrent();

            GLGraphics g = new GLGraphics();

            g.Init();
            g.Reset();
            g.Clear();
            g.SetClearColor(SystemColors.ActiveCaption);

            OnDraw?.Invoke(g);

            i++;
            if ((DateTime.Now - Last).TotalMilliseconds >= 1000)
            {
                FPS = i;
                i = 0;
                Last = DateTime.Now;
            }

            SwapBuffers();

            mGraphics = g;

            TimeSpan ts = DateTime.Now - t1;
            if (ts.TotalMilliseconds * 1.15 < _fpslimit)
                Thread.Sleep(_fpslimit - Convert.ToInt32(Math.Round(ts.TotalMilliseconds * 1.15, 0)));

            //Invalidate();
        }
        // ============================================================
        private void GraphicSurface_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
                return;

            Last = DateTime.Now;

            mIsLoaded = true;   // OpenGL контекст уже должен быть создан

            OnLoad?.Invoke();

            //Invalidate();
        }

        private void GraphicSurface_Resize(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

            mGraphics.Resize(Width, Height);

            GameData.Window = new Size(Width, Height);
        }

        public void Draw(GLGraphics g)
        {
            DateTime t1 = DateTime.Now;
            MakeCurrent();

            g.Init();
            g.Reset();
            g.Clear();
            g.SetClearColor(SystemColors.ActiveCaption);

            mGraphics = g;

                OnDraw?.Invoke(g);

                i++;
                if ((DateTime.Now - Last).TotalMilliseconds >= 1000)
                {
                    FPS = i;
                    i = 0;
                    Last = DateTime.Now;
                }

                SwapBuffers();

                TimeSpan ts = DateTime.Now - t1;
                if (ts.TotalMilliseconds < _fpslimit)
                    Thread.Sleep(_fpslimit - (int)Math.Round(ts.TotalMilliseconds, 0));
            
        }
        // ============================================================
        public delegate void DrawEventHandler(GLGraphics e);
        public delegate void GLLoadEventHandler();
        public event DrawEventHandler OnDraw;
        new public event GLLoadEventHandler OnLoad;

        private DateTime Last;
        private int i = 0;
        public int FPS { get; private set; } = 0;

        private int _fpslimit = 50;
        /// <summary>
        /// Лимит FPS
        /// </summary>
        public int FPSlimit
        {
            get { if (_fpslimit != 0) return (int)Math.Round(1000.0 / _fpslimit, 0); else return int.MaxValue; }
            set { if (value > 0) _fpslimit = (int)Math.Round(1000.0 / value, 0); else _fpslimit = 0; }
        }
    }
}
