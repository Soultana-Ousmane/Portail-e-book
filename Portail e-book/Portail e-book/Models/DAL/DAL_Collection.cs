using Portail_e_book.Models.BLL;
using Portail_e_book.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Models.DAL
{
    public class DAL_Collection
    {
        //CheckCollectionleUnicity
        protected static bool CheckCollectionUnicity(string Title)
        {
            int NbOccs = 0;
            try
            {
                using (SqlConnection Connection = DBConnection.GetConnection())
                {
                    string StrSQL = "select * from Collection where Title = @Title";
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
        protected static int CountCollectionUnicity(string Title)
        {
            int NbOccs = 0;
            try
            {
                using (SqlConnection Connection = DBConnection.GetConnection())
                {
                    string StrSQL = "select * from Collection where Title = @Title";
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
                string sql = "If not exists (select * from sysobjects where name = 'Collection') CREATE TABLE [dbo].[Collection] ([IdCollection] BIGINT        IDENTITY (1, 1) NOT NULL,[Editor]       NVARCHAR (50) NULL,[Theme]        NVARCHAR (50) NULL,[Title]        NVARCHAR (50) NULL,[ShortTitle]   NVARCHAR (50) NULL,[Description]  NVARCHAR (50) NULL,CONSTRAINT [PK_Collection] PRIMARY KEY CLUSTERED ([IdCollection] ASC));";
                using (SqlCommand command = new SqlCommand(sql, cnn))
                    command.ExecuteNonQuery();
                cnn.Close();
            }
            catch { }
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        public static List<Collection> getAllCollection()
        {
            List<Collection> listCollection = new List<Collection>();
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "select * from Collection";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                Collection collection = new Collection();

                                collection.IdCollection = Int32.Parse(dataReader["IdCollection"].ToString());
                                collection.Editor = dataReader["Editor"].ToString();
                                collection.Theme = dataReader["Theme"].ToString();
                                collection.Title = dataReader["Title"].ToString();
                                collection.ShortTitle = dataReader["ShortTitle"].ToString();
                                collection.Description = dataReader["Description"].ToString();

                                listCollection.Add(collection);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch { }
            return listCollection;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        //Get the details of a particular Collection
        public static Collection getCollectionBy(string Field, string Value)
        {
            Collection collection = new Collection();
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();

                    string sql = "select * from Collection where [" + Field + "]=@Field"; //

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Field", Value);
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                collection.IdCollection = Int32.Parse(dataReader["IdCollection"].ToString());
                                collection.Editor = dataReader["Editor"].ToString();
                                collection.Theme = dataReader["Theme"].ToString();
                                collection.Title = dataReader["Title"].ToString();
                                collection.ShortTitle = dataReader["ShortTitle"].ToString();
                                collection.Description = dataReader["Description"].ToString();
                            }


                        }
                    }
                    connection.Close();
                }
            }
            catch { }
            return collection;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        public static List<Collection> getAllCollectionBy(string Field, string Value)
        {
            List<Collection> listCollection = new List<Collection>();
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "select * from Collection where [" + Field + "]=@Field";//

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Field", Value);
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                Collection collection = new Collection();

                                collection.IdCollection = Int32.Parse(dataReader["IdCollection"].ToString());
                                collection.Editor = dataReader["Editor"].ToString();
                                collection.Theme = dataReader["Theme"].ToString();
                                collection.Title = dataReader["Title"].ToString();
                                collection.ShortTitle = dataReader["ShortTitle"].ToString();
                                collection.Description = dataReader["Description"].ToString();

                                listCollection.Add(collection);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch { }
            return listCollection;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        //Add new Collection record
        public static JsonResponse AddCollection(Collection collection)
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
                    if (CheckCollectionUnicity(collection.Title) == false)
                    {

                        string sql = "Insert into Collection (Editor, Theme, Title, ShortTitle, Description)values(@Editor, @Theme, @Title, @ShortTitle, @Description)";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.CommandType = CommandType.Text;
                            if (String.IsNullOrEmpty(collection.Editor))
                                command.Parameters.AddWithValue("@Editor", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Editor", collection.Editor);

                            if (String.IsNullOrEmpty(collection.Theme))
                                command.Parameters.AddWithValue("@Theme", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Theme", collection.Theme);

                            if (String.IsNullOrEmpty(collection.Title))
                                command.Parameters.AddWithValue("@Title", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Title", collection.Title);

                            if (String.IsNullOrEmpty(collection.ShortTitle))
                                command.Parameters.AddWithValue("@ShortTitle", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@ShortTitle", collection.ShortTitle);

                            if (String.IsNullOrEmpty(collection.Description))
                                command.Parameters.AddWithValue("@Description", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Description", collection.Description);


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
        //Update Collection
        public static JsonResponse UpdateCollection(Collection collection)
        {
            JsonResponse jr = new JsonResponse();
            jr.success = false;
            jr.message = "Erreur";
            Collection ancienneCollection = getCollectionBy("IdCollection", collection.IdCollection.ToString());
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "Update Collection set Editor=@Editor,Theme=@Theme,Title=@Title,ShortTitle=@ShortTitle,Description=@Description  where IdCollection=@IdCollection";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;

                        if (String.IsNullOrEmpty(collection.Editor))
                            command.Parameters.AddWithValue("@Editor", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Editor", collection.Editor);

                        if (String.IsNullOrEmpty(collection.Theme))
                            command.Parameters.AddWithValue("@Theme", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Theme", collection.Theme);

                        if (String.IsNullOrEmpty(collection.Title))
                            command.Parameters.AddWithValue("@Title", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Title", collection.Title);

                        if (String.IsNullOrEmpty(collection.ShortTitle))
                            command.Parameters.AddWithValue("@ShortTitle", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@ShortTitle", collection.ShortTitle);

                        if (String.IsNullOrEmpty(collection.Description))
                            command.Parameters.AddWithValue("@Description", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Description", collection.Description);


                        command.Parameters.AddWithValue("@IdCollection", collection.IdCollection);

                        command.ExecuteNonQuery();
                        if (CountCollectionUnicity(collection.Title) > 1)
                        {
                            DeleteCollectionBy("IdCollection", collection.IdCollection.ToString());
                            AddCollection(ancienneCollection);
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
        public static JsonResponse DeleteCollectionBy(string Field, string Value)
        {
            JsonResponse Message = new JsonResponse();
            Message.success = false;
            Message.message = "Erreur";
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();

                    string sql = "delete from Collection where [" + Field + "]=@Field";
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
