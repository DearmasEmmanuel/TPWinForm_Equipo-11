using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain;
using Business;
using static System.Net.Mime.MediaTypeNames;
using TPWinforms_equipo_11;

namespace TPWinforms
{
    public partial class FrmMain : Form
    {
        private List<Articulo> listaArticulo;
        public FrmMain()
        {
            InitializeComponent();
        }

        private void TPWinforms_Catalogo_Load(object sender, EventArgs e)
        {
            ArticuloBusiness business = new ArticuloBusiness();
            listaArticulo = business.Listar();
            dgvArticulo.DataSource = listaArticulo;
           /// pbxArticulo.Load(listaArticulo[0].Imagen.ImagenUrl);
            //dgvArticulo.Columns["Id"].Visible = false;
            //dgvArticulo.Columns["Imagen"].Visible = false;
        }

        private void dvgArticulo_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulo.SelectedRows.Count > 0)
            {
                Articulo articuloSelecionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
                if (articuloSelecionado != null && articuloSelecionado.Imagen != null)
                {
                    cargarImagen(articuloSelecionado.Imagen.ImagenUrl);
                }
            }
        }


        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxArticulo.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmAltaArticulo alta = new FrmAltaArticulo();
            alta.ShowDialog();
        }


    }
}
