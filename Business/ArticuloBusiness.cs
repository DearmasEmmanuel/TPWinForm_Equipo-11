﻿using System;
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
                    comando.CommandText = "SELECT a.Id, a.Codigo, a.Nombre, a.Descripcion, m.Id AS IdMarca, m.Descripcion AS Marca, c.Id as IdCategoria, c.Descripcion AS Categoria, i.Id AS IdImagen, i.IdArticulo, i.ImagenUrl AS Imagen, a.Precio FROM ARTICULOS a INNER JOIN MARCAS m ON a.IdMarca = m.Id  INNER JOIN CATEGORIAS c ON a.IdCategoria = c.Id LEFT JOIN IMAGENES i ON a.Id = i.IdArticulo";

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

                                if (!(lector["IdImagen"] is DBNull))
                                {
                                    aux.Imagen.Id = (int)lector["IdImagen"];
                                    aux.Imagen.IdArticulo = (int)lector["IdArticulo"];
                                    aux.Imagen.ImagenUrl = (string)lector["Imagen"];
                                }
                                else
                                {
                                    aux.Imagen.Id = 0;
                                    aux.Imagen.IdArticulo = 0;
                                    aux.Imagen.ImagenUrl = "";
                                }

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
