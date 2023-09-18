using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Business;
using Domain;
using static TPWinforms_equipo_11.FrmModificaArticulo;
using TPWinforms;

namespace TPWinforms_equipo_11
{
    public partial class FrmAltaArticulo : Form
    {
        public delegate void UpdateArticuloList();
        public UpdateArticuloList updateArticuloList;
        public FrmAltaArticulo()
        {
            InitializeComponent();
            updateArticuloList += FrmMain.UpdateArticuloList;
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
                Imagen nuevaImagen = new Imagen
                {
                    IdArticulo = nuevo.Id, 
                    ImagenUrl = txtImagenUrl.Text
                };
                nuevo.Imagen = new List<Imagen> { nuevaImagen };

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
           
                MarcaBusiness marcaNegocio = new MarcaBusiness();
                CategoriaBusiness categoriaNegocio = new CategoriaBusiness();
                cbMarcas.DataSource = marcaNegocio.List();
                cbCategoria.DataSource = categoriaNegocio.List();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
        }

        private void txtImagenUrl_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtImagenUrl.Text);

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

        private void FrmAltaArticulo_FormClosing(object sender, FormClosingEventArgs e)
        {
            updateArticuloList.Invoke();
        }
    }
}
