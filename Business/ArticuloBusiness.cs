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
            AccessData data = new AccessData();
            try
            {
                data.SetQuery("SELECT a.Id, a.Codigo, a.Nombre, a.Descripcion, m.Id AS IdMarca, m.Descripcion AS Marca, c.Id as IdCategoria, c.Descripcion AS Categoria, i.Id AS IdImagen, i.IdArticulo, i.ImagenUrl AS Imagen, a.Precio FROM ARTICULOS a INNER JOIN MARCAS m ON a.IdMarca = m.Id  INNER JOIN CATEGORIAS c ON a.IdCategoria = c.Id LEFT JOIN IMAGENES i ON a.Id = i.IdArticulo");
                data.ExecuteQuery();
                while (data.Reader.Read())
                {
                    Articulo aux = new Articulo()
                    {
                        Id = (int)data.Reader["Id"],
                        Codigo = data.Reader["Codigo"].ToString(),
                        Nombre = data.Reader["Nombre"].ToString(),
                        Descripcion = (string)data.Reader["Descripcion"],
                        Marca = new Marca
                        {
                            Id = (int)data.Reader["IdMarca"],
                            Descripcion = data.Reader["Marca"].ToString()
                        },
                        Categoria = new Categoria
                        {
                            Id = (int)data.Reader["IdCategoria"],
                            Descripcion = data.Reader["Categoria"].ToString()
                        },
                        Precio = Convert.ToDecimal(data.Reader["Precio"]),
                        Imagen = ImagenBusiness.List((int)data.Reader["Id"])
                    };
                    lista.Add(aux);
                }
            }
            catch (Exception ex1)
            {
                throw new Exception("Error al listar los artículos.", ex1);
            }
            return lista;
        }
        public int Agregar(Articulo articulo)
        {
            AccessData data = new AccessData();
            List<SqlParameter> parameters = new List<SqlParameter>();
            try
            {
                string columns, values;
                columns = values = "";
                if (articulo.Codigo != null && articulo.Codigo != "")
                {
                    columns += "Codigo,";
                    values += $"@Codigo,";
                    parameters.Add(new SqlParameter("@Codigo", articulo.Codigo));
                }
                if (articulo.Nombre != null && articulo.Nombre != "")
                {
                    columns += "Nombre,";
                    values += $"@Nombre,";
                    parameters.Add(new SqlParameter("@Nombre", articulo.Nombre));
                }
                if (articulo.Descripcion != null && articulo.Descripcion != "")
                {
                    columns += "Descripcion,";
                    values += $"@Descripcion,";
                    parameters.Add(new SqlParameter("@Descripcion", articulo.Descripcion));
                }
                if (articulo.Precio != null)
                {
                    columns += "Precio,";
                    values += $"@Precio,";
                    parameters.Add(new SqlParameter("@Precio", articulo.Precio));
                }
                string query = $@"
                    DECLARE @IdGenerado int

                    INSERT INTO ARTICULOS 
                        ({columns}IdMarca,IdCategoria)
                    VALUES
                        ({values}@BrandId,@CategoryId)
                    SET @IdGenerado = SCOPE_IDENTITY()
                    ";

                parameters.Add(new SqlParameter("@BrandId", articulo.Marca.Id));
                parameters.Add(new SqlParameter("@CategoryId", articulo.Categoria.Id));

                int imagesCount = articulo.Imagen is null ? 0 : articulo.Imagen.Count;
                if (imagesCount > 0)
                {
                    query += @"
                        INSERT INTO IMAGENES
                            (IdArticulo,ImagenUrl)
                        VALUES 
                        ";
                    for (int i = 0; i < imagesCount; i++)
                    {
                        query += $" (@IdGenerado, @ImagenUrl{i}),";
                        parameters.Add(new SqlParameter($"@ImagenUrl{i}", articulo.Imagen[i].ImagenUrl));
                    }
                    query = query.Remove(query.Length - 1);
                }

                data.SetQuery(query, parameters);

                return data.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.Close();
            }
        }

        public void Modificar(Articulo articulo)
        {
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand())
                {
                    comando.Connection = conexion;
                    comando.CommandType = System.Data.CommandType.Text;
                    comando.CommandText = "UPDATE ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, Precio = @Precio, IdMarca = @IdMarca, IdCategoria = @IdCategoria WHERE Id = @Id";

                    comando.Parameters.AddWithValue("@Codigo", articulo.Codigo);
                    comando.Parameters.AddWithValue("@Nombre", articulo.Nombre);
                    comando.Parameters.AddWithValue("@Descripcion", articulo.Descripcion);
                    comando.Parameters.AddWithValue("@Precio", articulo.Precio);
                    comando.Parameters.AddWithValue("@IdMarca", articulo.Marca.Id);
                    comando.Parameters.AddWithValue("@IdCategoria", articulo.Categoria.Id);
                    comando.Parameters.AddWithValue("@Id", articulo.Id);

                    try
                    {
                        conexion.Open();
                        comando.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al modificar el artículo.", ex);
                    }

                }

            }
        }
    }
}
