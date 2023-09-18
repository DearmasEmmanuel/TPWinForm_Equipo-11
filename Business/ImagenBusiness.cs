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
        public static int UpdateList(List<Imagen> oldImages, List<Imagen> updatedImages)
        {
            if (oldImages == updatedImages) { return 0; }
            AccessData data = new AccessData();
            List<SqlParameter> parameters = new List<SqlParameter>();
            try
            {
                string query = "";
                bool flagDeleteIteration = true;
                List<Imagen> lstToAdd = new List<Imagen>();
                List<Imagen> lstToDelete = new List<Imagen>();
                for (int i = 0; i < updatedImages.Count; i++)
                {
                    bool flagExist = false;
                    foreach (Imagen imageOld in oldImages)
                    {
                        if (updatedImages[i].Id == imageOld.Id)
                        {
                            flagExist = true;
                            if (updatedImages[i].ImagenUrl != imageOld.ImagenUrl)
                            {
                                query += $"UPDATE IMAGES SET ImagenUrl = @UrlUpdate{i} WHERE Id = @Id{i} ";
                                parameters.Add(new SqlParameter($"@UrlUpdate{i}", updatedImages[i].ImagenUrl));
                                parameters.Add(new SqlParameter($"@Id{i}", updatedImages[i].Id));
                            }
                            break;
                        }
                        if (flagDeleteIteration)
                        {
                            if (!updatedImages.Contains(imageOld))
                            {
                                lstToDelete.Add(imageOld);
                            }
                        }
                    }
                    if (!flagExist)
                    {
                        lstToAdd.Add(updatedImages[i]);
                    }
                    flagDeleteIteration = false;
                }
                if (lstToAdd.Count > 0)
                {
                    query += " INSERT INTO IMAGES (IdArticulo, ImagenUrl) VALUES ";
                    for (int i = 0; i < lstToAdd.Count; i++)
                    {
                        query += $" (@IdArticulo, @UrlInsert{i}),";
                        parameters.Add(new SqlParameter($"@IdArticulo", lstToAdd[i].IdArticulo));
                        parameters.Add(new SqlParameter($"@UrlInsert{i}", lstToAdd[i].ImagenUrl));
                    }
                    query = query.Remove(query.Length - 1);
                }

                if (lstToDelete.Count > 0)
                {
                    query += " DELETE FROM IMAGES WHERE ";
                    for (int i = 0; i < lstToDelete.Count; i++)
                    {
                        query += $@"Id = @Id{i} OR ";
                        parameters.Add(new SqlParameter($"@Id{i}", lstToDelete[i].Id));
                    }
                    query = query.Remove(query.Length - 3, 3);
                }

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
    }
}
