using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace TPFinalNivel_Casas
{
    public partial class FrmAltaArticulo : Form
    {
        public FrmAltaArticulo()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Articulo art = new Articulo();
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                art.Codigo = txtCodigo.Text; 
                art.Nombre = txtNombre.Text;
                art.Descripcion = txtDescripcion.Text;

                negocio.Cargar(art);
                MessageBox.Show("Artículo cargado exitosamente");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FrmAltaArticulo_Load(object sender, EventArgs e)
        {
            MarcaNegocio marNeg = new MarcaNegocio();
            CategoriaNegocio catNeg = new CategoriaNegocio();
            try
            {
                cboMarca.DataSource = marNeg.Listar();
                cboCategoria.DataSource = catNeg.Listar();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
