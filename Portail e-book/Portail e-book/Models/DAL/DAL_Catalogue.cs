using Portail_e_book.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Models.DAL
{
    public class DAL_Catalogue
    {
        protected static bool CheckCatalogueUnicity(string Title)
        {
            int NbOccs = 0;
            try
            {
                using (SqlConnection Connection = DBConnection.GetConnection())
                {
                    string StrSQL = "select * from Catalogue where Title = @Title";
                    SqlCommand Cmd = new SqlCommand(StrSQL, Connection);
                    Cmd.Parameters.AddWithValue("@Title", Title);
                    SqlDataAdapter da = new SqlDataAdapter(Cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    NbOccs = dt.Rows.Count;
                }
            }
            catch { }
            if (NbOccs > 0)
                return true;
            else
                return false;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        protected static int CountCatalogueUnicity(string Title)
        {
            int NbOccs = 0;
            try
            {
                using (SqlConnection Connection = DBConnection.GetConnection())
                {
                    string StrSQL = "select * from Catalogue where Title = @Title";
                    SqlCommand Cmd = new SqlCommand(StrSQL, Connection);
                    Cmd.Parameters.AddWithValue("@Title", Title);
                    SqlDataAdapter da = new SqlDataAdapter(Cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    NbOccs = dt.Rows.Count;
                }
            }
            catch { }
            return NbOccs;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        protected static void CreateTable()
        {
            try
            {
                SqlConnection cnn = DBConnection.GetConnection();
                cnn.Open();
                string sql = "If not exists (select * from sysobjects where name = 'Collection') CREATE TABLE [dbo].[Catalogue] ([IdCatalogue] BIGINT        IDENTITY (1, 1) NOT NULL,[Title]       NVARCHAR (50) NULL,[ShortTitle]  NVARCHAR (50) NULL,[Description] NVARCHAR (50) NULL,CONSTRAINT [PK_Catalogue] PRIMARY KEY CLUSTERED ([IdCatalogue] ASC)); ";
                using (SqlCommand command = new SqlCommand(sql, cnn))
                    command.ExecuteNonQuery();
                cnn.Close();
            }
            catch { }
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        public static List<Catalogue> getAllCatalogue()
        {
            List<Catalogue> listCatalogue = new List<Catalogue>();
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "select * from Catalogue";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                Catalogue catalogue = new Catalogue();

                                catalogue.IdCatalogue = Int32.Parse(dataReader["IdCatalogue"].ToString());
                                catalogue.Title = dataReader["Title"].ToString();
                                catalogue.ShortTitle = dataReader["ShortTitle"].ToString();
                                catalogue.Description = dataReader["Description"].ToString();
                                

                                listCatalogue.Add(catalogue);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch { }
            return listCatalogue;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        //Get the details of a particular Catalogue
        public static Catalogue getCatalogueBy(string Field, string Value)
        {
            Catalogue catalogue = new Catalogue();
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();

                    string sql = "select * from Catalogue where [" + Field + "]=@Field"; 

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Field", Value);
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                catalogue.IdCatalogue = Int32.Parse(dataReader["IdCatalogue"].ToString());
                                catalogue.Title = dataReader["Title"].ToString();
                                catalogue.ShortTitle = dataReader["ShortTitle"].ToString();
                                catalogue.Description = dataReader["Description"].ToString();
                            }


                        }
                    }
                    connection.Close();
                }
            }
            catch { }
            return catalogue;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        public static List<Catalogue> getAllCatalogueBy(string Field, string Value)
        {
            List<Catalogue> listCatalogue = new List<Catalogue>();
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "select * from Catalogue where [" + Field + "]=@Field";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Field", Value);
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                Catalogue catalogue = new Catalogue();

                                catalogue.IdCatalogue = Int32.Parse(dataReader["IdCatalogue"].ToString());
                                catalogue.Title = dataReader["Title"].ToString();
                                catalogue.ShortTitle = dataReader["ShortTitle"].ToString();
                                catalogue.Description = dataReader["Description"].ToString();


                                listCatalogue.Add(catalogue);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch { }
            return listCatalogue;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        //Add new Catalogue record
        public static JsonResponse AddCatalogue(Catalogue catalogue)
        {
            JsonResponse jr = new JsonResponse();
            jr.success = false;
            jr.message = "Erreur d'ajout";

            try
            {
                CreateTable();
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    if (CheckCatalogueUnicity(catalogue.Title) == false)
                    {
                        string sql = "Insert into Catalogue (Title, ShortTitle, Description)values(@Title, @ShortTitle, @Description)";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.CommandType = CommandType.Text;

                           
                            if (String.IsNullOrEmpty(catalogue.Title))
                                command.Parameters.AddWithValue("@Title", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Title", catalogue.Title);

                            if (String.IsNullOrEmpty(catalogue.ShortTitle))
                                command.Parameters.AddWithValue("@ShortTitle", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@ShortTitle", catalogue.ShortTitle);

                            if (String.IsNullOrEmpty(catalogue.Description))
                                command.Parameters.AddWithValue("@Description", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Description", catalogue.Description);


                            if (command.ExecuteNonQuery() == 1)
                            {
                                jr.success = true;
                                jr.message = "Ajout ok";
                            }
                        }
                    }
                    else
                    {
                        jr.message = "Title existe déjà !";
                    }
                    connection.Close();
                }

            }
            catch (Exception e)
            {
                jr.message = "Erreur : " + e.Message;
            }
            return jr;
        }
        /*-------------------------------------------------------------------------------------------------------------------------------------------*/
        //Update Catalogue
        public static JsonResponse UpdateCatalogue(Catalogue catalogue)
        {
            JsonResponse jr = new JsonResponse();
            jr.success = false;
            jr.message = "Erreur";
            Catalogue ancienCatalogue = getCatalogueBy("IdCatalogue", catalogue.IdCatalogue.ToString());
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "Update Catalogue set Title=@Title,ShortTitle=@ShortTitle,Description=@Description  where IdCatalogue=@IdCatalogue";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;

                        if (String.IsNullOrEmpty(catalogue.Title))
                            command.Parameters.AddWithValue("@Title", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Title", catalogue.Title);

                        if (String.IsNullOrEmpty(catalogue.ShortTitle))
                            command.Parameters.AddWithValue("@ShortTitle", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@ShortTitle", catalogue.ShortTitle);

                        if (String.IsNullOrEmpty(catalogue.Description))
                            command.Parameters.AddWithValue("@Description", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Description", catalogue.Description); ;


                        command.Parameters.AddWithValue("@IdCatalogue", catalogue.IdCatalogue);

                        command.ExecuteNonQuery();
                        if (CountCatalogueUnicity(catalogue.Title) > 1)
                        {
                            DeleteCatalogueBy("IdCatalogue", catalogue.IdCatalogue.ToString());
                            AddCatalogue(ancienCatalogue);
                            jr.message = "Title existe déjà !";
                        }
                        else
                        {
                            jr.success = true;
                            jr.message = "Resultat ok";
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                jr.message = "Erreur : " + e.Message;
            }
            return jr;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        //Delete Collection
        public static JsonResponse DeleteCatalogueBy(string Field, string Value)
        {
            JsonResponse Message = new JsonResponse();
            Message.success = false;
            Message.message = "Erreur";
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "delete from Catalogue where [" + Field + "]=@Field";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Field", Value);

                        if (command.ExecuteNonQuery() == 1)
                        {
                            Message.success = true;
                            Message.message = "Suppression effectuée ! ";
                        }
                    }

                    connection.Close();

                }
            }
            catch (Exception e)
            {
                Message.message = "Erreur : " + e.Message;
            }
            return Message;
        }

    }
}
