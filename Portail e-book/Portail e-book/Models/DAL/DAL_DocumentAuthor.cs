using Portail_e_book.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Models.DAL
{
    public class DAL_DocumentAuthor
    {
        protected static bool CheckDocumentAuthorUnicity(string Role)
        {
            int NbOccs = 0;
            try
            {
                using (SqlConnection Connection = DBConnection.GetConnection())
                {
                    string StrSQL = "select * from DocumentAuthor where Role = @Role";
                    SqlCommand Cmd = new SqlCommand(StrSQL, Connection);
                    Cmd.Parameters.AddWithValue("@Role", Role);
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
        protected static int CountDocumentAuthorUnicity(string Role)
        {
            int NbOccs = 0;
            try
            {
                using (SqlConnection Connection = DBConnection.GetConnection())
                {
                    string StrSQL = "select * from DocumentAuthor where Role = @Role";
                    SqlCommand Cmd = new SqlCommand(StrSQL, Connection);
                    Cmd.Parameters.AddWithValue("@Role", Role);
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
                string sql = "If not exists (select * from sysobjects where name = 'DocumentAuthor') CREATE TABLE [dbo].[DocumentAuthor] ([IdDocumentAuthor] BIGINT        IDENTITY (1, 1) NOT NULL,[IdAuthor]         BIGINT        NOT NULL,[IdDocument]       BIGINT        NOT NULL,[Role]             NVARCHAR (50) NULL,CONSTRAINT [PK_DocumentAuthor] PRIMARY KEY CLUSTERED ([IdDocumentAuthor] ASC),CONSTRAINT [FK_DocumentAuthor_DocumentAuthor] FOREIGN KEY ([IdDocument]) REFERENCES [dbo].[Document] ([IdDocument]));";
                using (SqlCommand command = new SqlCommand(sql, cnn))
                    command.ExecuteNonQuery();
                cnn.Close();
            }
            catch { }
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        public static List<DocumentAuthor> getAllDocumentAuthor()
        {
            List<DocumentAuthor> listDocumentAuthor = new List<DocumentAuthor>();
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "select * from DocumentAuthor";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                DocumentAuthor documentAuthor = new DocumentAuthor();

                                documentAuthor.IdDocumentAuthor = Int32.Parse(dataReader["IdDocumentAuthor"].ToString());
                                documentAuthor.IdAuthor = Int32.Parse(dataReader["IdDocumentAuthor"].ToString());
                                documentAuthor.IdDocument = Int32.Parse(dataReader["IdDocumentAuthor"].ToString());
                                documentAuthor.Role = dataReader["Role"].ToString();



                                listDocumentAuthor.Add(documentAuthor);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch { }
            return listDocumentAuthor;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        public static DocumentAuthor getDocumentAuthorBy(string Field, string Value)
        {
            DocumentAuthor documentAuthor = new DocumentAuthor();
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();

                    string sql = "select * from DocumentAuthor where [" + Field + "]=@Field"; 

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Field", Value);
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                documentAuthor.IdDocumentAuthor = Int32.Parse(dataReader["IdDocumentAuthor"].ToString());
                                documentAuthor.IdAuthor = Int32.Parse(dataReader["IdDocumentAuthor"].ToString());
                                documentAuthor.IdDocument = Int32.Parse(dataReader["IdDocumentAuthor"].ToString());
                                documentAuthor.Role = dataReader["Role"].ToString();

                            }


                        }
                    }
                    connection.Close();
                }
            }
            catch { }
            return documentAuthor;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        public static List<DocumentAuthor> getAllDocumentAuthorBy(string Field, string Value)
        {
            List<DocumentAuthor> listDocumentAuthor = new List<DocumentAuthor>();
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "select * from DocumentAuthor where [" + Field + "]=@Field";//

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Field", Value);
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                DocumentAuthor documentAuthor = new DocumentAuthor();

                                documentAuthor.IdDocumentAuthor = Int32.Parse(dataReader["IdDocumentAuthor"].ToString());
                                documentAuthor.IdAuthor = Int32.Parse(dataReader["IdDocumentAuthor"].ToString());
                                documentAuthor.IdDocument = Int32.Parse(dataReader["IdDocumentAuthor"].ToString());
                                documentAuthor.Role = dataReader["Role"].ToString();



                                listDocumentAuthor.Add(documentAuthor);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch { }
            return listDocumentAuthor;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        public static JsonResponse AddDocumentAuthor(DocumentAuthor documentAuthor)
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
                    if (CheckDocumentAuthorUnicity(documentAuthor.Role) == false)
                    {
                        string sql = "Insert into DocumentAuthor (IdAuthor, IdDocument, Role)values(@IdAuthor, @IdDocument, @Role)";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.CommandType = CommandType.Text;
                            if (documentAuthor.IdAuthor == 0)
                                command.Parameters.AddWithValue("@IdAuthor", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@IdAuthor", documentAuthor.IdAuthor);

                            if (documentAuthor.IdDocument == 0)
                                command.Parameters.AddWithValue("@IdDocument", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@IdDocument", documentAuthor.IdDocument);

                            if (String.IsNullOrEmpty(documentAuthor.Role))
                                command.Parameters.AddWithValue("@Role", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Role", documentAuthor.Role);

                           

                            if (command.ExecuteNonQuery() == 1)
                            {
                                jr.success = true;
                                jr.message = "Ajout ok";
                            }
                        }
                    }
                    else
                    {
                        jr.message = "Role existe déjà !";
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
        
        public static JsonResponse UpdateDocumentAuthor(DocumentAuthor documentAuthor)
        {
            JsonResponse jr = new JsonResponse();
            jr.success = false;
            jr.message = "Erreur";
            DocumentAuthor ancienDocumentAuthor = getDocumentAuthorBy("IdDocumentAuthor", documentAuthor.IdDocumentAuthor.ToString());
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "Update DocumentAuthor set IdAuthor=@IdAuthor, IdDocument=@IdDocument, Role=@Role  where IdDocumentAuthor=@IdDocumentAuthor";
                  
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;

                        if (documentAuthor.IdAuthor == 0)
                            command.Parameters.AddWithValue("@IdAuthor", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@IdAuthor", documentAuthor.IdAuthor);

                        if (documentAuthor.IdDocument == 0)
                            command.Parameters.AddWithValue("@IdDocument", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@IdDocument", documentAuthor.IdDocument);

                        if (String.IsNullOrEmpty(documentAuthor.Role))
                            command.Parameters.AddWithValue("@Role", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Role", documentAuthor.Role);



                        command.Parameters.AddWithValue("@IdDocumentAuthor", documentAuthor.IdDocumentAuthor);

                        command.ExecuteNonQuery();
                        if (CountDocumentAuthorUnicity(documentAuthor.Role) > 1)
                        {
                            DeleteDocumentAuthorBy("IdDocumentAuthor", documentAuthor.IdDocumentAuthor.ToString());
                            AddDocumentAuthor(ancienDocumentAuthor);
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
        public static JsonResponse DeleteDocumentAuthorBy(string Field, string Value)
        {
            JsonResponse Message = new JsonResponse();
            Message.success = false;
            Message.message = "Erreur";
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "delete from DocumentAuthor where [" + Field + "]=@Field";
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
