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

namespace TPFinalNivel_Casas
{
    public partial class FrmProductCard : Form
    {
        Articulo articulo = new Articulo();
        
        public FrmProductCard()
        {
            InitializeComponent();
        }

        public FrmProductCard(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        private void FrmProductCard_Load(object sender, EventArgs e)
        {

            try
            {
                // CArgo la imagen del artíuclo
                CargarImagen(articulo.UrlImagen);

                // Cargo los labels con la info 
                lblShowCodigo.Text = articulo.Codigo;
                lblShowNombre.Text = articulo.Nombre;
                lblShowDescripcion.Text = articulo.Descripcion;
                lblShowMarca.Text = articulo.Mar.Descripcion;
                lblShowCategoria.Text = articulo.Cate.Descripcion;
                lblShowPrecio.Text = "$ " + articulo.Precio.ToString("F2");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
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
