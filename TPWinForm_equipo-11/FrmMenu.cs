using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPWinForm_equipo_11;

namespace WinForm
{
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAbout ventana = new FrmAbout();
            ventana.Show();
        }

        private void mostrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TPWinforms_Catalogo ventana1 = new TPWinforms_Catalogo();
            ventana1.Show();
        }


    }
}
