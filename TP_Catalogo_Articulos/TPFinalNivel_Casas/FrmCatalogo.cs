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
using System.Xml.Serialization;
using TPFinalNivel_Casas.UI;



namespace TPFinalNivel_Casas
{
    public partial class FrmCatalogo : Form
    {
        private List<Articulo> listaArticulo = new List<Articulo>();

        public FrmCatalogo()
        {
            InitializeComponent();
        }

        
        // LOAD FORMULARIO
        private void FormCatalogo_Load(object sender, EventArgs e)
        {
            // Formulario
            EstilosUI.ConfigurarFormulario(this);
            
            //Botón Principal
            EstilosUI.ConfigurarBotonPrincipal(btnAgregar, 435, 69);
            EstilosUI.ConfigurarBotonPrincipal(btnModificar, 435, 119);
            EstilosUI.ConfigurarBotonPrincipal(btnEliminar, 435, 169);

            //Botón Secundario
            EstilosUI.ConfigurarBotonSecundario(btnLimpiar, 280, 18);
            EstilosUI.ConfigurarBotonSecundario(btnVerMas, 280, 345);
            EstilosUI.ConfigurarBotonSecundario(btnFiltro, 258, 48);

            //Text Box
            EstilosUI.ConfigurarTextBox(txtFiltro,pnlFiltro);
            
            Cargar();
            cboCampo.Items.Add("Nombre");
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
                //CargarImagen(listaArticulo[0].UrlImagen); // cargo la primera imagen
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
            dgvArticulos.Columns["Descripcion"].Visible = false;
            dgvArticulos.Columns["Codigo"].Visible = false;
            dgvArticulos.Columns["Cate"].Visible = false;
        }

        //private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        //{
        //    if (dgvArticulos.CurrentRow != null)
        //    {
        //        Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
        //        CargarImagen(seleccionado.UrlImagen);            
        //    }
            
        //}

        // Método para cargar imágenes
        //public void CargarImagen(string imagen)
        //{
        //    try
        //    {
        //        pbxArticulos.Load(imagen);
        //    }
        //    catch (Exception ex)
        //    {
        //        pbxArticulos.Load("https://as2.ftcdn.net/jpg/01/07/43/45/220_F_107434511_iarF2z88c6Ds6AlgtwotHSAktWCdYOn7.jpg");
        //    }
        //}


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

        //Método para validar Filtro 
        private bool ValidarFiltro()
        {
            if (cboCampo.SelectedIndex < 0) // si tengo algo seleccionado en el cbo.
            {
                MessageBox.Show("Por favor seleccione el CAMPO");
                return true; //si, validamos
            }

            if (cboCriterio.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor seleccione el CRITERIO");
                return true;
            }

            if (string.IsNullOrEmpty(txtFiltroAvanzado.Text))
            {
                txtFiltroAvanzado.ForeColor = Color.OrangeRed;
                txtFiltroAvanzado.Text = "Completar Campo";
                btnFiltro.Enabled = false;

                return true;
            }

            if (cboCampo.SelectedItem.ToString() == "Precio")
            {
                if (!(SoloNumeros(txtFiltroAvanzado.Text)))
                {
                    MessageBox.Show("Solo Números para filtrar");
                    return true;
                }
            }

            return false; //no validamos
        }

        // Valida que solo haya números en el txtFiltroAvanzado
        private bool SoloNumeros(string cadena)
        {
            return decimal.TryParse(cadena, out _); 
            // intenta convertir la cadena a un número real, que no contenga otra cosa
        }

        // Botón que filtra contra Base de Datos
        private void btnFiltro_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                if (ValidarFiltro())
                    return; // corto la ejecución del void btmFiltro_Click()

                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtFiltroAvanzado.Text;
                
                //txtFiltroAvanzado.Enabled = true;  << OJO ACÁ

                dgvArticulos.DataSource = negocio.Filtrar(campo, criterio, filtro);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // Selección por ComboBox
        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cboCampo.SelectedItem.ToString();
            cboCriterio.DataSource = null;
            cboCriterio.Items.Clear();
            txtFiltroAvanzado.Text = string.Empty;

            if (opcion == "Precio")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Mayor a: ");
                cboCriterio.Items.Add("Menor a: ");
                cboCriterio.Items.Add("Igual a: ");
            }
            else if (opcion == "Marca")
            {
                // acá se carga el cboMarca con las marcas de la Base de datos 

                MarcaNegocio marNegocio = new MarcaNegocio();

                try
                {
                    cboCriterio.DataSource = marNegocio.Listar();
                    cboCriterio.ValueMember = "Id";
                    cboCriterio.DisplayMember = "Descripcion";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Comienza con: ");
                cboCriterio.Items.Add("Termina con: ");
                cboCriterio.Items.Add("Contiene: ");
            }
        }

        private void cboCriterio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCampo.SelectedItem.ToString() == "Marca")
            {
                txtFiltroAvanzado.Text = cboCriterio.SelectedItem.ToString();
                txtFiltroAvanzado.Enabled = false;
            }
            else
            {
                txtFiltroAvanzado.Enabled = true;
                cboCriterio.DataSource = null;
            }
        }

        // resetea el txtFiltroAavanzado
        private void txtFiltroAvanzado_Click(object sender, EventArgs e)
        {
            if (txtFiltroAvanzado.Text == "Completar Campo")
            {
                txtFiltroAvanzado.Text = string.Empty;
                btnFiltro.Enabled = true;
                txtFiltroAvanzado.ForeColor = Color.Black;
            }
        }

        // limpita el txtFiltro 
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtFiltro.Text = string.Empty;
            Cargar();
        }

        // Botón que muestra los detalles completos del producto
        private void btnVerMas_Click(object sender, EventArgs e)
        {
            Articulo seleccionado = new Articulo();
            seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem; 
            
            FrmProductCard formDetalles = new FrmProductCard(seleccionado);
            formDetalles.ShowDialog();
        }

        // Panel de Filtro Avanzado
        private void chkFiltroAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            // retorna tru o false según esté seleccionado...  
            pnlFiltroAvanzado.Enabled = chkFiltroAvanzado.Checked;
        }

    }
}
