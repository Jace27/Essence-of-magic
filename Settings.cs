using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EssenceOfMagic
{
    public partial class Settings : Form
    {
        #region Open Settings
        public static bool ShowInterface = true;
        public static bool RegenerateInterface = true;
        public static bool ShowDebugInformation = true;
        public static bool ShowHitboxes = true;
        public static int  MusicVolume = 100;
        #endregion

        #region Inner Settings
        public static int BackpackCapacity = 24;
        public static int HotbarCapacity = 5;
        public static int EquipmentCapacity = 1;
        #endregion

        public Settings(Form owner)
        {
            InitializeComponent();

            Owner = owner;
            Size = Owner.Size;
            Location = Owner.Location;

            Read();

            ShowInterface_CheckBox.Checked = ShowInterface;
            ShowInfo_CheckBox.Checked = ShowDebugInformation;
            ShowHitbox_CheckBox.Checked = ShowHitboxes;

            if (TitleScreen.woe != null)
            {
                int vol = (int)Math.Round(TitleScreen.woe.Volume * 100, 0);
                if (vol > 100)
                    vol = 100;
                else if (vol < 0)
                    vol = 0;
                MusicVolume = vol;
            }
            MusicVolume_TrackBar.Value = MusicVolume;
        }

        #region Settings in file
        public static void Read()
        {
            using (StreamReader sr = new StreamReader("Settings.ini"))
            {
                while (!sr.EndOfStream)
                {
                    string[] str = sr.ReadLine().Trim().Split(new char[] { ' ', '=', ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (str.Length == 2)
                    {
                        string name = str[0].Trim();
                        string value = str[1].Trim();
                        try
                        {
                            switch (name)
                            {
                                case "ShowInterface":
                                    ShowInterface = Convert.ToBoolean(value);
                                    break;
                                case "RegenerateInterface":
                                    RegenerateInterface = Convert.ToBoolean(value);
                                    break;
                                case "ShowDebugInformation":
                                    ShowDebugInformation = Convert.ToBoolean(value);
                                    break;
                                case "ShowHitboxes":
                                    ShowHitboxes = Convert.ToBoolean(value);
                                    break;
                                case "MusicVolume":
                                    MusicVolume = Convert.ToInt32(value);
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        public static void Write()
        {
            using (StreamWriter sw = new StreamWriter("Settings.ini", false))
            {
                string output = "";

                output += "ShowInterface" + " = " + ShowInterface.ToString() + "\n";
                output += "RegenerateInterface" + " = " + RegenerateInterface.ToString() + "\n";
                output += "ShowDebugInformation" + " = " + ShowDebugInformation.ToString() + "\n";
                output += "ShowHitboxes" + " = " + ShowHitboxes.ToString() + "\n";
                output += "MusicVolume" + " = " + MusicVolume.ToString() + "\n";

                sw.Write(output);
            }
        }
        #endregion

        #region Window Dragging
        bool isWindowDragging = false;
        Size delta = new Size(-1, -1);
        private void Settings_MouseDown(object sender, MouseEventArgs e)
        {
            isWindowDragging = true;
            delta = new Size(-e.X - SystemInformation.FrameBorderSize.Width, -e.Y - SystemInformation.FrameBorderSize.Height);
        }

        private void Settings_MouseMove(object sender, MouseEventArgs e)
        {
            if (isWindowDragging)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(delta.Width, delta.Height);
                Location = mousePos;
                Owner.Location = Location;
            }
        }

        private void Settings_MouseUp(object sender, MouseEventArgs e)
        {
            isWindowDragging = false;
            delta = new Size(-1, -1);
        }
        #endregion

        private void ShowInterface_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ShowInterface = ShowInterface_CheckBox.Checked;
        }

        private void ShowInfo_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ShowDebugInformation = ShowInfo_CheckBox.Checked;
        }

        private void ShowHitbox_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ShowHitboxes = ShowHitbox_CheckBox.Checked;
        }

        private void Settings_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Write();
                Close();
            }
        }

        private void Label3_Click(object sender, EventArgs e)
        {
            Write();
            Close();
        }

        private void MusicVolume_TrackBar_ValueChanged(object sender, EventArgs e)
        {
            MusicVolume = MusicVolume_TrackBar.Value;
            TitleScreen.woe.Volume = MusicVolume / 100.0f;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            Location = Owner.Location;
            Owner.Location = Location;
        }
    }
}
