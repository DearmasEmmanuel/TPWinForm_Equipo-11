using System;
using System.Windows.Forms;
using Business;
using Domain;

namespace TPWinforms_equipo_11
{
    public partial class FrmAltaArticulo : Form
    {
        public FrmAltaArticulo()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Articulo nuevo = new Articulo();
                ArticuloBusiness articuloBusiness = new ArticuloBusiness();

                nuevo.Codigo = txtCodigo.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.Precio = decimal.Parse(txtPrecio.Text);
                nuevo.Marca = (Marca)cbMarcas.SelectedItem;
                nuevo.Categoria = (Categoria)cbCategoria.SelectedItem;

                articuloBusiness.Agregar(nuevo);

                MessageBox.Show("Agregado Exitosamente");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el artículo: " + ex.Message);
            }
        }

        private void FrmAltaArticulo_Load(object sender, EventArgs e)
        {
            try
            {
                // Asegúrate de que las clases MarcaBusiness y CategoriaBusiness tengan métodos estáticos List() que devuelvan una lista de elementos.
                // Puedes cambiar MarcaBusiness.List() y CategoriaBusiness.List() por los métodos correctos que obtienen la lista de marcas y categorías.

                cbMarcas.DataSource = MarcaBusiness.List();
                cbCategoria.DataSource = CategoriaBusiness.List();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
        }
    }
}
