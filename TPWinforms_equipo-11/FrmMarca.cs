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

namespace TPWinforms_equipo_11
{
    public partial class FrmMarca : Form
    {
        public FrmMarca()
        {
            InitializeComponent();
        }



        private void FrmMarca_Load(object sender, EventArgs e)
        {
            MarcaBusiness marcaNegocio = new MarcaBusiness();
            dgvMarca.DataSource = marcaNegocio.List();

        }

        private void btnMarcaAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Marca NuevaMarca = new Marca();


                NuevaMarca.Descripcion = txNuevaMarca.Text;

                MarcaBusiness.Add(NuevaMarca);
                MessageBox.Show("Agregado Exitosamente");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el artículo: " + ex.Message);
            }
        }

        private void btnEliminarMarca_Click(object sender, EventArgs e)
        {
            try
            {
                Marca marcaEliminar = new Marca();


                marcaEliminar.Id = int.Parse(txtIdmarca.Text);
                MarcaBusiness.Remove(marcaEliminar);
                MessageBox.Show("Se Elimino Correctamente");
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar la Marca: " + ex.Message);
            }

        }
    }
}

