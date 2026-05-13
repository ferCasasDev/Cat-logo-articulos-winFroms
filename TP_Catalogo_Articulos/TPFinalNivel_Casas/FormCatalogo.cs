using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Dominio;



namespace TPFinalNivel_Casas
{
    public partial class FormCatalogo : Form
    {
        public FormCatalogo()
        {
            InitializeComponent();
        }

        private void FormCatalogo_Load(object sender, EventArgs e)
        {
            List<Articulo> listaArticulo = new List<Articulo>();
            ArticuloNegocio negocio = new ArticuloNegocio();
            
            listaArticulo = negocio.Listar();
            // Al dataSource del data grid view le asigno la lista.
            dgvArticulos.DataSource = listaArticulo;
            pbxArticulos.Load(listaArticulo[0].UrlImagen); // cargo la primera imagen
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            CargarImagen(seleccionado.UrlImagen);            
        }

        // Método para cargar imágenes
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
