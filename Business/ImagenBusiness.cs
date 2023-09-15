using Data;
using System;
using System.Collections.Generic;
using Domain;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Data.SqlClient;

namespace Business
{
    internal class ImagenBusiness
    {
        public static List<Imagen> List()
        {
            List<Imagen> imageList = new List<Imagen>();
            AccessData data = new AccessData();
            try
            {
                data.SetQuery(@"SELECT Id, IdArticulo, ImagenUrl FROM IMAGENES");
                data.ExecuteQuery();

                while (data.Reader.Read())
                {
                    Imagen imageAux = new Imagen()
                    {
                        Id = (int)data.Reader["Id"],
                        IdArticulo = (int)data.Reader["IdArticulo"],
                        ImagenUrl = (string)data.Reader["ImagenUrl"],
                    };
                    imageList.Add(imageAux);
                }

                return imageList;
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
        public static List<Imagen> List(int idItem)
        {
            List<Imagen> imagenList = new List<Imagen>();
            AccessData data = new AccessData();
            try
            {
                string query = @"SELECT Id, IdArticulo, ImagenUrl 
                                FROM IMAGENES 
                                WHERE IdArticulo = @IdItem";
                List<SqlParameter> parameters = new List<SqlParameter>() { new SqlParameter("@IdItem", idItem) };
                data.SetQuery(query, parameters);
                data.ExecuteQuery();

                while (data.Reader.Read())
                {
                    Imagen imagenAux = new Imagen()
                    {
                        Id = (int)data.Reader["Id"],
                        IdArticulo = (int)data.Reader["IdArticulo"],
                        ImagenUrl = (string)data.Reader["ImagenUrl"],
                    };
                    imagenList.Add(imagenAux);
                }

                return imagenList;
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
