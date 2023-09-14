using Data;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class CategoriaBusiness
    {
        public static List<Categoria> List()
        {

            List<Categoria> categoriaList = new List<Categoria>();
            AccessData accessData = new AccessData();
            try
            {
                accessData.SetQuery(@"SELECT Id, Descripcion FROM CATEGORIAS");


                accessData.ExecuteQuery();

                while (accessData.Reader.Read())
                {
                    Categoria cateAux = new Categoria()
                    {
                        Id = (int)accessData.Reader["Id"],
                        Descripcion = (string)accessData.Reader["Descripcion"],
                    };
                    categoriaList.Add(cateAux);
                }

                return categoriaList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                accessData.Close();
            }
        }
    }
}
