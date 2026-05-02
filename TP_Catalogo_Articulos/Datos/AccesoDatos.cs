using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using System.Data.SqlClient;

namespace Datos
{
    public class AccesoDatos
    {

        // declaracion de variables 
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        // propiedad solo lectura de lector 
        public SqlDataReader Lector
        {
            get {  return lector; }
        }

        // constructor de la clase
        public AccesoDatos()
        {
            conexion = new SqlConnection("sever=.\\SQLEXPRESS; database=CATALOGO_DB; integrated security=true");
            comando = new SqlCommand();
        }

        // método para setear la consulta 
        public void SetearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        // método para ejecutar la lectura 
        public void EjecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // método para cerrar conexión y lector
        public void CerrarConexion()
        {
            if (lector != null)
                lector.Close();
            conexion.Close();
        }

    }
}
