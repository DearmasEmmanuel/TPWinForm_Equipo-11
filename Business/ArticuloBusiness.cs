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
        public List<Articulo> List()
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
                if (articulo.Precio != 0)
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
        public int Modificar(Articulo oldItem, Articulo updatedItem)
        {
            AccessData data = new AccessData();
            List<SqlParameter> parameters = new List<SqlParameter>();
            try
            {
                string query = "UPDATE ARTICULOS SET";
                parameters.Add(new SqlParameter("@Id", oldItem.Id));
                if (oldItem.Nombre != updatedItem.Nombre)
                {
                    query += " Nombre = @Name,";
                    parameters.Add(new SqlParameter("@Name", updatedItem.Nombre));
                }
                if (oldItem.Descripcion != updatedItem.Descripcion)
                {
                    query += " Descripcion = @Description,";
                    parameters.Add(new SqlParameter("@Description", updatedItem.Descripcion));
                }
                if (oldItem.Codigo != updatedItem.Codigo)
                {
                    query += " Codigo = @Code,";
                    parameters.Add(new SqlParameter("@Code", updatedItem.Codigo));
                }
                if (oldItem.Categoria != updatedItem.Categoria)
                {
                    query += " IdCategoria = @Category,";
                    parameters.Add(new SqlParameter("@Category", updatedItem.Categoria.Id));
                }
                if (oldItem.Marca != updatedItem.Marca)
                {
                    query += " IdMarca = @Brand,";
                    parameters.Add(new SqlParameter("@Brand", updatedItem.Marca.Id));
                }
                if (oldItem.Precio != updatedItem.Precio)
                {
                    query += " Precio = @Price,";
                    parameters.Add(new SqlParameter("@Price", updatedItem.Precio));
                }
                int imageUpdate = ImagenBusiness.UpdateList(oldItem.Imagen, updatedItem.Imagen);
                Console.WriteLine(imageUpdate);
                if (query[query.Length - 1] == ',')
                {
                    query = query.Remove(query.Length - 1, 1);
                }
                else
                {
                    return -1;
                }

                query += " WHERE Id = @Id";

                data.SetQuery(query, parameters);

                return data.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return -1;
                throw ex;
            }
            finally
            {
                data.Close();
            }
        }
        //public void Modificar(Articulo articulo)
        //{
        //    AccessData data = new AccessData();
        //    List<SqlParameter> parameters = new List<SqlParameter>();
        //    try
        //    {
        //        string query = @"
        //    UPDATE ARTICULOS 
        //    SET Codigo = @Codigo, 
        //        Nombre = @Nombre, 
        //        Descripcion = @Descripcion, 
        //        Precio = @Precio, 
        //        IdMarca = @IdMarca, 
        //        IdCategoria = @IdCategoria
        //    WHERE Id = @Id";

        //        parameters.Add(new SqlParameter("@Codigo", articulo.Codigo));
        //        parameters.Add(new SqlParameter("@Nombre", articulo.Nombre));
        //        parameters.Add(new SqlParameter("@Descripcion", articulo.Descripcion));
        //        parameters.Add(new SqlParameter("@Precio", articulo.Precio));
        //        parameters.Add(new SqlParameter("@IdMarca", articulo.Marca.Id));
        //        parameters.Add(new SqlParameter("@IdCategoria", articulo.Categoria.Id));
        //        parameters.Add(new SqlParameter("@Id", articulo.Id));

        //        data.SetQuery(query, parameters);

        //        // Actualizar todas las imágenes en la tabla IMAGENES en una sola consulta
        //        StringBuilder updateImagenQuery = new StringBuilder();
        //        updateImagenQuery.Append("UPDATE IMAGENES SET ImagenUrl = CASE IdArticulo ");

        //        for (int i = 0; i < articulo.Imagen.Count; i++)
        //        {
        //            updateImagenQuery.Append($"WHEN {articulo.Id} THEN @ImagenUrl{i} ");
        //            parameters.Add(new SqlParameter($"@ImagenUrl{i}", articulo.Imagen[i].ImagenUrl));
        //        }

        //        updateImagenQuery.Append("ELSE ImagenUrl END");

        //        data.SetQuery(updateImagenQuery.ToString(), parameters);

        //        // Ejecutar la actualización
        //        data.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al modificar el artículo.", ex);
        //    }
        //    finally
        //    {
        //        data.Close();
        //    }
        //}
        public int Eliminar(Articulo articulo)
        {
            AccessData data = new AccessData();
            List<SqlParameter> parameters = new List<SqlParameter>();
            try
            {
                string query = "DELETE FROM ARTICULOS WHERE Id = @Id";
                parameters.Add(new SqlParameter("@Id", articulo.Id));

                if (articulo.Imagen.Count > 0)
                {
                    query += " DELETE FROM IMAGENES WHERE IdArticulo = @Id";
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
    }
}
