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
    public partial class FrmCatalogo : Form
    {
        private List<Articulo> listaArticulo = new List<Articulo>();

        public FrmCatalogo()
        {
            InitializeComponent();
        }

        // LOAD
        private void FormCatalogo_Load(object sender, EventArgs e)
        {
            Cargar();

            cboCampo.Items.Add("Código");
            cboCampo.Items.Add("Nombre");
            cboCampo.Items.Add("Descripción");
            cboCampo.Items.Add("Marca");
            cboCampo.Items.Add("Precio");

        }

        
        //Método que refresca la grilla 
        private void Cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                listaArticulo = negocio.Listar();
                // Al dataSource del data grid view le asigno la lista.
                dgvArticulos.DataSource = listaArticulo;
                OcultarColumnas();   
                CargarImagen(listaArticulo[0].UrlImagen); // cargo la primera imagen
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        //Método para ocultar columnas
        private void OcultarColumnas()
        {
            dgvArticulos.Columns["UrlImagen"].Visible = false;
            dgvArticulos.Columns["Id"].Visible = false;
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                CargarImagen(seleccionado.UrlImagen);            
            }
            
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


        // BOTÓN PARA AGREGAR
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmAltaArticulo alta = new FrmAltaArticulo();
            alta.ShowDialog();
            Cargar(); // Refresco la grilla con los datos actualizados
        }


        // BOTÓN PARA MODIFICAR
        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

            FrmAltaArticulo modificar = new FrmAltaArticulo(seleccionado);
            modificar.ShowDialog();
            Cargar(); // Refresco la grilla con los datos actualizados
        }

        //BOTÓN ELIMINAR 
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo seleccionado;
            try
            {
                DialogResult resultado = MessageBox.Show("¿Estás seguro de eliminar?", "Eliminando...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resultado == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    negocio.Eliminar(seleccionado.Id);
                    Cargar();
                }
                // no necesita else.. si el resultado es No, ciella el diálogo.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //BOTÓN FILTRO [Buscar] --> lo dejamos para un filtro más avanzado
        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            string filtro = txtFiltro.Text;

            if (filtro.Length >= 3) // Si tiene 3 o más caracteres, filtra, sino lista completa
            {
                listaFiltrada = listaArticulo.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.Descripcion.ToUpper().Contains(filtro.ToUpper()) || x.Mar.Descripcion.ToUpper().Contains(filtro.ToUpper())); // función Lambda : para comparar y buscar por Nombre, Descripción o Marca. 
            }
            else
            {
                listaFiltrada = listaArticulo;
            }

            dgvArticulos.DataSource = null; // limpiamos la lista
            dgvArticulos.DataSource = listaFiltrada;
            OcultarColumnas();
        }
        
        private void btnFiltro_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtFiltroAvanzado.Text;
                dgvArticulos.DataSource = negocio.Filtrar(campo, criterio, filtro);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }



        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cboCampo.SelectedItem.ToString();
            
            if (opcion == "Precio")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Mayor a: ");
                cboCriterio.Items.Add("Menor a: ");
                cboCriterio.Items.Add("Igual a: ");
            }
            else
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Comienza con: ");
                cboCriterio.Items.Add("Termina con: ");
                cboCriterio.Items.Add("Contiene: ");
            }
        }
    }
}
