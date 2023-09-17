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
            cargar();
        }

        private void dvgArticulo_SelectionChanged(object sender, EventArgs e)
        {
            Articulo articuloSelecionado = dgvArticulo.CurrentRow?.DataBoundItem as Articulo;
            if (articuloSelecionado != null && articuloSelecionado.Imagen.Count > 0)
            {
                string primeraImagenUrl = articuloSelecionado.Imagen[0].ImagenUrl;
                cargarImagen(primeraImagenUrl);
            }
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception)
            {
                pbxArticulo.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmAltaArticulo alta = new FrmAltaArticulo();
            alta.ShowDialog();
            cargar();
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;

            if (dgvArticulo.CurrentCell is null)
            {
                MessageBox.Show("Debe Seleccionar un Articulo");
            }
            else
            {
                seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
                FrmModificaArticulo modificar = new FrmModificaArticulo(seleccionado);
                modificar.ShowDialog();
                cargar();
            }
        }
        private void cargar()
        {
            ArticuloBusiness business = new ArticuloBusiness();
            try
            {
                listaArticulo = business.Listar();
                dgvArticulo.DataSource = listaArticulo;
                dgvArticulo.Columns["Id"].Visible = false;
                //dgvArticulo.Columns["Imagen"].Visible = false;
                cargarImagen(listaArticulo[0].Imagen[0].ImagenUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void numPriceMin_ValueChanged(object sender, EventArgs e)
        {

        }

        private void buttonBrandWindow_Click(object sender, EventArgs e)
        {
            FrmMarca frmMarca = new FrmMarca();
            frmMarca.Show();
        }

        private void buttonCategoryWindow_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            eliminar();
        }
        private void eliminar()
        {
            ArticuloBusiness business = new ArticuloBusiness();
            Articulo seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿De verdad querés eliminarlo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
                    business.eliminar(seleccionado.Id);
                    cargar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
