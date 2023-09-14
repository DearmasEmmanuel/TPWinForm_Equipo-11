using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Data;
using Domain;

using static System.Net.Mime.MediaTypeNames;


namespace Business
{
    public class ArticuloBusiness
    {
        private string connectionString = "Server=.\\SQLEXPRESS;Database=CATALOGO_P3_DB;Integrated Security=true";

        public List<Articulo> Listar()
        {
            List<Articulo> lista = new List<Articulo>();

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand())
                {
                    comando.Connection = conexion;
                    comando.CommandType = System.Data.CommandType.Text;
                    comando.CommandText = "SELECT a.Codigo, a.Nombre, a.Descripcion, a.Precio, i.ImagenUrl, i.IdArtico, m.Descripcion AS Marca, c.Descripcion AS Categoria, a.IdMarca, a.IdCategoria, a.Id FROM ARTICULOS a, MARCAS m, CATEGORIAS c, IMAGENES i WHERE a.IdMarca = m.Id AND a.IdCategoria = c.Id AND a.Id = i.IdArticulo";

                    try
                    {
                        conexion.Open();
                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                Articulo aux = new Articulo();
                                aux.Id = (int)lector["Id"];
                                aux.Codigo = (string)lector["Codigo"];
                                aux.Nombre = (string)lector["Nombre"];
                                aux.Descripcion = (string)lector["Descripcion"];

                                aux.Marca = new Marca();
                                aux.Marca.Id = (int)lector["IdMarca"];
                                aux.Marca.Descripcion = (string)lector["Marca"];
                                aux.Categoria = new Categoria();
                                aux.Categoria.Id = (int)lector["IdCategoria"];
                                aux.Categoria.Descripcion = (string)lector["Categoria"];
                                aux.Imagen = new Imagen();
                                aux.Imagen.Id = (int)lector["IdArticulo"];
                                aux.Imagen.ImagenUrl = (string)lector["ImagenUrl"];



                                aux.Precio = (decimal)lector["Precio"];

                                lista.Add(aux);
                            }
                        }
                    }
                    catch (Exception ex1)
                    {
                        throw new Exception("Error al listar los artículos.", ex1);
                    }
                }
            }

            return lista;
        }

        public void Agregar(Articulo articulo)
        {
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand())
                {
                    comando.Connection = conexion;
                    comando.CommandType = System.Data.CommandType.Text;
                    comando.CommandText = "INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, Precio, IdMarca, IdCategoria) VALUES (@Codigo, @Nombre, @Descripcion, @Precio, @IdMarca, @IdCategoria)";

                    comando.Parameters.AddWithValue("@Codigo", articulo.Codigo);
                    comando.Parameters.AddWithValue("@Nombre", articulo.Nombre);
                    comando.Parameters.AddWithValue("@Descripcion", articulo.Descripcion);
                    comando.Parameters.AddWithValue("@Precio", articulo.Precio);
                    comando.Parameters.AddWithValue("@IdMarca", articulo.Marca.Id);
                    comando.Parameters.AddWithValue("@IdCategoria", articulo.Categoria.Id);

                    try
                    {
                        conexion.Open();
                        comando.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al agregar el artículo.", ex);
                    }
                }
            }
        }
    }
}
