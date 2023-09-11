using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPWinforms
{
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAboutUs ventana = new FrmAboutUs();
            ventana.Show();
        }

        private void mostrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMain ventana1 = new FrmMain();
            ventana1.Show();
        }
    }
}
