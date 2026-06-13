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
        private Articulo articulo = null; // para definir si cargo o modifico

        public FrmAltaArticulo()
        {
            InitializeComponent();
        }

        // CONSRUCTOR con parámetros
        // ya viene con datos
        public FrmAltaArticulo( Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar Artículo";
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                if (articulo == null)
                    articulo = new Articulo();

                articulo.Codigo = txtCodigo.Text; 
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.UrlImagen = txtUrlImagen.Text;
                articulo.Mar = (Marca)cboMarca.SelectedItem;
                articulo.Cate = (Categoria)cboCategoria.SelectedItem;
                articulo.Precio = decimal.Parse(txtPrecio.Text);

                if(articulo.Id != 0)
                {
                    negocio.Modificar(articulo);
                    MessageBox.Show("Artículo modificado exitosamente");
                }
                else
                {
                    negocio.Agregar(articulo);
                    MessageBox.Show("Artículo agregado exitosamente");
                }

                // cierro el formulario
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
                cboMarca.DataSource = marNeg.Listar(); // carga de desplegables
                cboMarca.ValueMember = "Id";  // Nombre de la prop. de Marca : es hiden
                cboMarca.DisplayMember = "Descripcion"; // Nombre de la prop. de Marca : lo que se ve
                
                cboCategoria.DataSource = catNeg.Listar(); 
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";

                if (articulo != null) // si es != null, es para modificar y tengo que precargar los datos
                {
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    txtUrlImagen.Text = articulo.UrlImagen;
                    CargarImagen(articulo.UrlImagen);
                    cboMarca.SelectedValue = articulo.Mar.Id;
                    cboCategoria.SelectedValue = articulo.Cate.Id;
                    txtPrecio.Text = articulo.Precio.ToString();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            CargarImagen(txtUrlImagen.Text);
        }

        public void CargarImagen(string imagen)
        {
            try
            {
                pbxArticulos.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxArticulos.Load("https://as2.ftcdn.net/jpg/01/07/43/45/220_F_107434511_iarF2z88c6Ds6AlgtwotHSAktWCdYOn7.jpg");
            }
        }
    }
}
