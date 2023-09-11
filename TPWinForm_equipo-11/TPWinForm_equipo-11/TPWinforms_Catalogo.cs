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

namespace TPWinForm_equipo_11
{
    public partial class TPWinforms_Catalogo : Form
    {
        private List<Articulo> listaArticulo;
        public TPWinforms_Catalogo()
        {
            InitializeComponent();
        }

        private void TPWinforms_Catalogo_Load(object sender, EventArgs e)
        {
            ArticuloBusiness business = new ArticuloBusiness();
            listaArticulo = business.listar();
            dgvArticulo.DataSource = listaArticulo;
            dgvArticulo.Columns["Id"].Visible = false;
            dgvArticulo.Columns["Imagen"].Visible = false;
        }

        private void dvgArticulo_SelectionChanged(object sender, EventArgs e)
        {
            Articulo articuloSelecionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
            cargarImagen(articuloSelecionado.Imagen.ImagenUrl);
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
    }
}
