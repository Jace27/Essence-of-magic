using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldEditor
{
    public partial class SelectTextureDialog : Form
    {
        public SelectTextureDialog(EssenceOfMagic.Texture[] textures, WorldEditor parent)
        {
            InitializeComponent();
            Textures = textures;
            Parent = parent;
        }
        EssenceOfMagic.Texture[] Textures;
        new WorldEditor Parent;
        private void SelectTextureDialog_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            for (int i = 0; i < Textures.Length; i++)
            {
                listBox1.Items.Add(Textures[i].ID);
            }
        }

        string selected;
        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selected = listBox1.SelectedItem.ToString();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Parent.SelectedID = selected;
            DialogResult = DialogResult.OK;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
