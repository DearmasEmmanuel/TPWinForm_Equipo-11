using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPWinforms_equipo_11;

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



        private void productoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAltaArticulo frmAltaArticulo = new FrmAltaArticulo();
            frmAltaArticulo.Show();
        }

        private void marcaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMarca frmMarca = new FrmMarca(); 
            frmMarca.Show();
        }
    }
}
