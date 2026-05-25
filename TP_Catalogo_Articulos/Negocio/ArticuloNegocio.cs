using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Dominio;


namespace Negocio
{
    public class ArticuloNegocio
    {
        //Método para Listar Artículos
        public List<Articulo> Listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // seteo de consulta y lectura de la DB
                datos.SetearConsulta("select A.Id, A.Codigo, A.Nombre, A.Descripcion, ImagenUrl, M.Descripcion Marca, C.Descripcion Categoria, A.Precio from ARTICULOS A, MARCAS M, CATEGORIAS C where A.IdCategoria = C.Id and A.IdMarca = M.Id");
                datos.EjecutarLectura();

                // mapeo de datos de la DB a través del lector
                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    // valido que el campo ImagenUrl no esté DBNULL
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["ImagenUrl"];
                    // si encuetra un DBNULL deja el campo con un string vacío

                    aux.Mar = new Marca();
                    aux.Mar.Descripcion = (string)datos.Lector["Marca"];
                    aux.Cate = new Categoria();
                    aux.Cate.Descripcion = (string)datos.Lector["Categoria"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(aux);
                }
                
                return lista;

            }
            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                datos.CerrarConexion();
            }
        }

        //Método para la Carga y Alta
        public void Cargar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // aquí setaearemos los datos con parámetros @ 
                datos.SetearConsulta("insert into ARTICULOS(Codigo, Nombre, Descripcion, ImagenUrl, IdMarca, IdCategoria, Precio) values(@Codigo, @Nombre, @Descripcion, @ImagenUrl, @IdMarca, @IdCategoria, @Precio)");
                datos.SetearParametros("@Codigo",nuevo.Codigo);
                datos.SetearParametros("@Nombre",nuevo.Nombre);
                datos.SetearParametros("@Descripcion", nuevo.Descripcion);
                datos.SetearParametros("@IdMarca", nuevo.Mar.Id);
                datos.SetearParametros("@IdCategoria", nuevo.Cate.Id);
                datos.SetearParametros("@Precio", nuevo.Precio);
                datos.SetearParametros("@ImagenUrl", nuevo.UrlImagen);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }

        }


    }
}
