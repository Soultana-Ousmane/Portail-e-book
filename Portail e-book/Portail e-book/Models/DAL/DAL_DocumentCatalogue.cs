using Portail_e_book.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Models.DAL
{
    public class DAL_DocumentCatalogue
    {
        protected static bool CheckDocumentCatalogueUnicity(int idDocument)
        {
            int NbOccs = 0;
            try
            {
                using (SqlConnection Connection = DBConnection.GetConnection())
                {
                    string StrSQL = "select * from DocumentCatalogue where idDocument = @idDocument";
                    SqlCommand Cmd = new SqlCommand(StrSQL, Connection);
                    Cmd.Parameters.AddWithValue("@idDocument", idDocument);
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
        protected static int CountDocumentCatalogueUnicity(int idDocument)
        {
            int NbOccs = 0;
            try
            {
                using (SqlConnection Connection = DBConnection.GetConnection())
                {
                    string StrSQL = "select * from DocumentCatalogue where idDocument = @idDocument";
                    SqlCommand Cmd = new SqlCommand(StrSQL, Connection);
                    Cmd.Parameters.AddWithValue("@idDocument", idDocument);
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
                string sql = "If not exists (select * from sysobjects where name = 'DocumentCatalogue') CREATE TABLE [dbo].[DocumentCatalogue] ([IdDocumentCatalogue] BIGINT NOT NULL,[IdDocument]          BIGINT NOT NULL,[IdCatalogue]         BIGINT NOT NULL,CONSTRAINT [PK_DocumentCatalogue] PRIMARY KEY CLUSTERED ([IdDocumentCatalogue] ASC),CONSTRAINT [FK_DocumentCatalogue_Document] FOREIGN KEY ([IdDocument]) REFERENCES [dbo].[Document] ([IdDocument]),CONSTRAINT [FK_DocumentCatalogue_Catalogue] FOREIGN KEY ([IdCatalogue]) REFERENCES [dbo].[Catalogue] ([IdCatalogue]));";
                using (SqlCommand command = new SqlCommand(sql, cnn))
                    command.ExecuteNonQuery();
                cnn.Close();
            }
            catch { }
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        public static List<DocumentCatalogue> getAllDocumentCatalogue()
        {
            List<DocumentCatalogue> listDocumentCatalogue = new List<DocumentCatalogue>();
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "select * from DocumentCatalogue";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                DocumentCatalogue documentCatalogue = new DocumentCatalogue();

                                documentCatalogue.IdDocumentCatalogue = Int32.Parse(dataReader["IdDocumentCatalogue"].ToString());
                                documentCatalogue.IdDocument = Int32.Parse(dataReader["IdDocument"].ToString());
                                documentCatalogue.IdCatalogue = Int32.Parse(dataReader["IdCatalogue"].ToString());



                                listDocumentCatalogue.Add(documentCatalogue);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch { }
            return listDocumentCatalogue;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        public static DocumentCatalogue getDocumentCatalogueBy(string Field, string Value)
        {
            DocumentCatalogue documentCatalogue = new DocumentCatalogue();
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();

                    string sql = "select * from DocumentCatalogue where [" + Field + "]=@Field";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Field", Value);
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                documentCatalogue.IdDocumentCatalogue = Int32.Parse(dataReader["IdDocumentCatalogue"].ToString());
                                documentCatalogue.IdDocument = Int32.Parse(dataReader["IdDocument"].ToString());
                                documentCatalogue.IdCatalogue = Int32.Parse(dataReader["IdCatalogue"].ToString());


                            }

                        }
                    }
                    connection.Close();
                }
            }
            catch { }
            return documentCatalogue;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        public static List<DocumentCatalogue> getAllDocumentCatalogueBy(string Field, string Value)
        {
            List<DocumentCatalogue> listDocumentCatalogue = new List<DocumentCatalogue>();
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "select * from DocumentCatalogue where [" + Field + "]=@Field";//

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Field", Value);
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                DocumentCatalogue documentCatalogue = new DocumentCatalogue();

                                documentCatalogue.IdDocumentCatalogue = Int32.Parse(dataReader["IdDocumentCatalogue"].ToString());
                                documentCatalogue.IdDocument = Int32.Parse(dataReader["IdDocument"].ToString());
                                documentCatalogue.IdCatalogue = Int32.Parse(dataReader["IdCatalogue"].ToString());


                                listDocumentCatalogue.Add(documentCatalogue);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch { }
            return listDocumentCatalogue;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        public static JsonResponse AddDocumentCatalogue(DocumentCatalogue documentCatalogue)
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
                    if (CheckDocumentCatalogueUnicity(documentCatalogue.IdDocument) == false)
                    {
                        string sql = "Insert into DocumentCatalogue (IdDocument, IdCatalogue)values(@IdDocument, @IdCatalogue)";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.CommandType = CommandType.Text;
                            if (documentCatalogue.IdDocument == 0)
                                command.Parameters.AddWithValue("@IdDocument", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@IdDocument", documentCatalogue.IdDocument);

                            if (documentCatalogue.IdCatalogue == 0)
                                command.Parameters.AddWithValue("@IdCatalogue", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@IdCatalogue", documentCatalogue.IdCatalogue);

                            if (command.ExecuteNonQuery() == 1)
                            {
                                jr.success = true;
                                jr.message = "Ajout ok";
                            }
                        }
                    }
                    else
                    {
                        jr.message = "IdDocument existe déjà !";
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

        public static JsonResponse UpdateDocumentCatalogue(DocumentCatalogue documentCatalogue)
        {
            JsonResponse jr = new JsonResponse();
            jr.success = false;
            jr.message = "Erreur";
            DocumentCatalogue ancienDocumentCatalogue = getDocumentCatalogueBy("IdDocumentCatalogue", documentCatalogue.IdDocumentCatalogue.ToString());
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "Update DocumentCatalogue set  IdDocument=@IdDocument, IdCatalogue=@IdCatalogue  where IdDocumentCatalogue=@IdDocumentCatalogue";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;

                        if (documentCatalogue.IdDocument == 0)
                            command.Parameters.AddWithValue("@IdDocument", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@IdDocument", documentCatalogue.IdDocument);

                        if (documentCatalogue.IdCatalogue == 0)
                            command.Parameters.AddWithValue("@IdCatalogue", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@IdCatalogue", documentCatalogue.IdCatalogue);


                        command.Parameters.AddWithValue("@IdDocumentCatalogue", documentCatalogue.IdDocumentCatalogue);

                        command.ExecuteNonQuery();
                        if (CountDocumentCatalogueUnicity(documentCatalogue.IdDocument) > 1)
                        {
                            DeleteDocumentCatalogueBy("IdDocumentCatalogue", documentCatalogue.IdDocumentCatalogue.ToString());
                            AddDocumentCatalogue(ancienDocumentCatalogue);
                            jr.message = "Role existe déjà !";
                        }
                        else
                        {
                            jr.success = true;
                            jr.message = "Modification effectuée !";
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
        public static JsonResponse DeleteDocumentCatalogueBy(string Field, string Value)
        {
            JsonResponse Message = new JsonResponse();
            Message.success = false;
            Message.message = "Erreur";
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "delete from DocumentCatalogue where [" + Field + "]=@Field";
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
