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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace TPWinforms
{
    public partial class FrmMain : Form
    {
        private static List<Articulo> listaArticulo;
        private static List<Categoria> listaCategoria;
        private static List<Marca> listaMarca;
        private const string WITHOUTFILTER = "Todas";
        private const string NotAssigned = "Sin Asignar";
        public FrmMain()
        {
            InitializeComponent();
        }

        private void TPWinforms_Catalogo_Load(object sender, EventArgs e)
        {
            try
            {
                UpdateArticuloList();
                UpdateCategoriaList();
                UpdateBrandList();
                //dgvArticulo.DataSource = listaArticulo;
                //dgvArticulo.Columns["Id"].Visible = false;
                //dgvArticulo.Columns["Imagen"].Visible = false;
                cargarImagen(listaArticulo[0].Imagen[0].ImagenUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void TPWinforms_Activated(object sender, EventArgs e)
        {
            dgvArticulo.DataSource = listaArticulo;
            cbCategory.Items.Clear();
            cbBrand.Items.Clear();
            cbCategory.Items.Add(WITHOUTFILTER);
            cbBrand.Items.Add(WITHOUTFILTER);
            listaCategoria.ForEach(x => cbCategory.Items.Add(x));
            listaMarca.ForEach(x => cbBrand.Items.Add(x));
            cbCategory.Items.Add(NotAssigned);
            cbBrand.Items.Add(NotAssigned);
            cbCategory.SelectedItem = cbCategory.Items[0];
            cbBrand.SelectedItem = cbBrand.Items[0];
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
            }
        }

        public static void UpdateArticuloList()
        {
            ArticuloBusiness business = new ArticuloBusiness();
            listaArticulo = business.List();
        }
        public static void UpdateCategoriaList()
        {

            CategoriaBusiness business = new CategoriaBusiness();
            listaCategoria = business.List();
        }
        public static void UpdateBrandList()
        {
            MarcaBusiness business = new MarcaBusiness();
            listaMarca = business.List();
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
                    business.Eliminar(seleccionado);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FilterEvent(object sender, EventArgs e)
        {
            var categoryFilter = cbCategory.SelectedItem;
            var brandFilter = cbBrand.SelectedItem;
            List<Articulo> itemFilteredList = listaArticulo;
            if (categoryFilter != null)
            {
                if (categoryFilter.ToString() != WITHOUTFILTER)
                {
                    itemFilteredList = itemFilteredList.Where(x => categoryFilter.ToString() == x.Categoria.ToString()).ToList();
                }
            }
            if (brandFilter != null)
            {
                if (brandFilter.ToString() != WITHOUTFILTER)
                {
                    itemFilteredList = itemFilteredList.Where(x => brandFilter.ToString() == x.Marca.ToString()).ToList();
                }
            }
            if (txtNombre.TextLength > 0)
            {
                itemFilteredList = itemFilteredList.Where(x => x.Nombre.ToLower().Contains(txtNombre.Text.ToLower())).ToList();
            }
            if (txtCode.TextLength > 0)
            {
                itemFilteredList = itemFilteredList.Where(x => x.Codigo.ToLower().Contains(txtCode.Text.ToLower())).ToList();
            }
            if (txtDescription.TextLength > 0)
            {
                itemFilteredList = itemFilteredList.Where(x => x.Descripcion.ToLower().Contains(txtDescription.Text.ToLower())).ToList();
            }
            if (numPriceMin.Value > 0)
            {
                itemFilteredList = itemFilteredList.Where(x => x.Precio >= numPriceMin.Value).ToList();
            }
            if (numPriceMax.Value > 0)
            {
                itemFilteredList = itemFilteredList.Where(x => x.Precio <= numPriceMax.Value).ToList();
            }

            dgvArticulo.DataSource = itemFilteredList;
        }
    }
}
