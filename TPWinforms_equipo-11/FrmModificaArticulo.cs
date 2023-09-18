using Business;
using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPWinforms;

namespace TPWinforms_equipo_11
{
    public partial class FrmModificaArticulo : Form
    {
        private Articulo articulo = null;
        public delegate void UpdateArticuloList();
        public UpdateArticuloList updateArticuloList;
        public FrmModificaArticulo()
        {
            InitializeComponent();
            updateArticuloList += FrmMain.UpdateArticuloList;
        }

        public FrmModificaArticulo(Articulo articulo)
        {
            InitializeComponent();
            updateArticuloList += FrmMain.UpdateArticuloList;
            this.articulo = articulo;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo nuevo = new Articulo();

            ArticuloBusiness articuloBusiness = new ArticuloBusiness();

            try
            {
                nuevo.Id = this.articulo.Id;
                nuevo.Codigo = txtCodigo.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.Precio = decimal.Parse(txtPrecio.Text);
                nuevo.Marca = (Marca)cboMarca.SelectedItem;
                nuevo.Categoria = (Categoria)cboCategoria.SelectedItem;
                Imagen nuevaImagen = new Imagen
                {
                    IdArticulo = nuevo.Id,
                    ImagenUrl = txtImagenUrl.Text
                };
                nuevo.Imagen = new List<Imagen> { nuevaImagen };

                articuloBusiness.Modificar(articulo, nuevo);

                MessageBox.Show("Modificdo Exitosamente");
                this.Close();
            }
            
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el artículo: " + ex.Message);
            }
        }       
        private void FrmModificaArticulo_Load(object sender, EventArgs e)
        {
            MarcaBusiness marcaNegocio = new MarcaBusiness();
            CategoriaBusiness categoriaNegocio = new CategoriaBusiness();
            try
            {
                cboMarca.DataSource = marcaNegocio.List();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";

                cboCategoria.DataSource = categoriaNegocio.List();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";

                if (articulo != null)
                {
                    txtCodigo.Text = articulo.Codigo.ToString();
                    txtNombre.Text = articulo.Nombre.ToString();
                    txtDescripcion.Text = articulo.Descripcion.ToString();
                    txtPrecio.Text = articulo.Precio.ToString();
                    if (articulo.Imagen != null)
                    {
                        txtImagenUrl.Text = articulo.Imagen[0].ImagenUrl.ToString();
                        cargarImagen(txtImagenUrl.Text);
                    }
                    cboMarca.SelectedValue = articulo.Marca.Id;
                    cboCategoria.SelectedValue = articulo.Categoria.Id;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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

        private void FrmModificaArticulo_FormClosing(object sender, FormClosingEventArgs e)
        {
            updateArticuloList.Invoke();
        }
    }
}