using Portail_e_book.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Models.DAL
{
    public class DAL_DocumentFiles
    {
        protected static bool CheckDocumentFilesUnicity(string Title)
        {
            int NbOccs = 0;
            try
            {
                using (SqlConnection Connection = DBConnection.GetConnection())
                {
                    string StrSQL = "select * from DocumentFiles where Title = @Title";
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
        protected static int CountDocumentFilesUnicity(string Title)
        {
            int NbOccs = 0;
            try
            {
                using (SqlConnection Connection = DBConnection.GetConnection())
                {
                    string StrSQL = "select * from DocumentFiles where Title = @Title";
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
                string sql = "If not exists (select * from sysobjects where name = 'DocumentFiles') CREATE TABLE [dbo].[DocumentFiles] ([IdDocumentFiles] BIGINT        IDENTITY (1, 1) NOT NULL,[IdDocument]      BIGINT        NOT NULL,[Title]           NVARCHAR (50) NULL,[FileDocument]    NVARCHAR (50) NULL,[FileFormat]      NVARCHAR (50) NULL,[StartPage]       NVARCHAR (50) NULL,[EndPage]         NVARCHAR (50) NULL,CONSTRAINT [PK_DocumentFiles] PRIMARY KEY CLUSTERED ([IdDocumentFiles] ASC),CONSTRAINT [FK_DocumentFiles_Document] FOREIGN KEY ([IdDocument]) REFERENCES [dbo].[Document] ([IdDocument]));";
                using (SqlCommand command = new SqlCommand(sql, cnn))
                    command.ExecuteNonQuery();
                cnn.Close();
            }
            catch { }
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        public static List<DocumentFiles> getAllDocumentFiles()
        {
            List<DocumentFiles> listDocumentFiles = new List<DocumentFiles>();
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "select * from DocumentFiles";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                DocumentFiles documentFiles = new DocumentFiles();

                                documentFiles.IdDocumentFiles = Int32.Parse(dataReader["IdDocumentFiles"].ToString());
                                documentFiles.IdDocument = Int32.Parse(dataReader["IdDocument"].ToString());
                                
                                documentFiles.Title = dataReader["Title"].ToString();
                                documentFiles.FileDocument = dataReader["FileDocument"].ToString();
                                documentFiles.FileFormat = dataReader["FileFormat"].ToString();
                                documentFiles.StartPage = dataReader["StartPage"].ToString();
                                documentFiles.EndPage = dataReader["EndPage"].ToString();



                                listDocumentFiles.Add(documentFiles);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch { }
            return listDocumentFiles;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        public static DocumentFiles getDocumentFilesBy(string Field, string Value)
        {
            DocumentFiles documentFiles = new DocumentFiles();
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();

                    string sql = "select * from DocumentFiles where [" + Field + "]=@Field";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Field", Value);
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                documentFiles.IdDocumentFiles = Int32.Parse(dataReader["IdDocumentFiles"].ToString());
                                documentFiles.IdDocument = Int32.Parse(dataReader["IdDocument"].ToString());

                                documentFiles.Title = dataReader["Title"].ToString();
                                documentFiles.FileDocument = dataReader["FileDocument"].ToString();
                                documentFiles.FileFormat = dataReader["FileFormat"].ToString();
                                documentFiles.StartPage = dataReader["StartPage"].ToString();
                                documentFiles.EndPage = dataReader["EndPage"].ToString();

                            }


                        }
                    }
                    connection.Close();
                }
            }
            catch { }
            return documentFiles;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        public static List<DocumentFiles> getAllDocumentFilesBy(string Field, string Value)
        {
            List<DocumentFiles> listDocumentFiles = new List<DocumentFiles>();
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "select * from DocumentFiles where [" + Field + "]=@Field";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Field", Value);
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                DocumentFiles documentFiles = new DocumentFiles();

                                documentFiles.IdDocumentFiles = Int32.Parse(dataReader["IdDocumentFiles"].ToString());
                                documentFiles.IdDocument = Int32.Parse(dataReader["IdDocument"].ToString());

                                documentFiles.Title = dataReader["Title"].ToString();
                                documentFiles.FileDocument = dataReader["FileDocument"].ToString();
                                documentFiles.FileFormat = dataReader["FileFormat"].ToString();
                                documentFiles.StartPage = dataReader["StartPage"].ToString();
                                documentFiles.EndPage = dataReader["EndPage"].ToString();



                                listDocumentFiles.Add(documentFiles);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch { }
            return listDocumentFiles;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        public static JsonResponse AddDocumentFiles (DocumentFiles documentFiles)
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
                    if (CheckDocumentFilesUnicity(documentFiles.Title) == false)
                    {
                        string sql = "Insert into DocumentFiles (IdDocument, Title, FileDocument, FileFormat, StartPage, EndPage)values(@IdDocument, @Title, @FileDocument, @FileFormat, @StartPage, @EndPage)";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.CommandType = CommandType.Text;
                            if (documentFiles.IdDocument == 0)
                                command.Parameters.AddWithValue("@IdDocument", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@IdDocument", documentFiles.IdDocument);


                            if (String.IsNullOrEmpty(documentFiles.Title))
                                command.Parameters.AddWithValue("@Title", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Title", documentFiles.Title);
                            if (String.IsNullOrEmpty(documentFiles.FileDocument))
                                command.Parameters.AddWithValue("@FileDocument", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@FileDocument", documentFiles.FileDocument);
                            if (String.IsNullOrEmpty(documentFiles.FileFormat))
                                command.Parameters.AddWithValue("@FileFormat", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@FileFormat", documentFiles.FileFormat);
                            if (String.IsNullOrEmpty(documentFiles.StartPage))
                                command.Parameters.AddWithValue("@StartPage", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@StartPage", documentFiles.StartPage);
                            if (String.IsNullOrEmpty(documentFiles.EndPage))
                                command.Parameters.AddWithValue("@EndPage", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@EndPage", documentFiles.EndPage);




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

        public static JsonResponse UpdateDocumentFiles(DocumentFiles documentFiles)
        {
            JsonResponse jr = new JsonResponse();
            jr.success = false;
            jr.message = "Erreur";
            DocumentFiles ancienDocumentFiles = getDocumentFilesBy("IdDocumentFiles", documentFiles.IdDocumentFiles.ToString());
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "Update DocumentFiles set IdDocument=@IdDocument, Title=@Title, FileDocument=@FileDocument, FileFormat=@FileFormat, StartPage=@StartPage, EndPage=@EndPage  where IdDocumentFiles=@IdDocumentFiles";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;

                        if (documentFiles.IdDocument == 0)
                            command.Parameters.AddWithValue("@IdDocument", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@IdDocument", documentFiles.IdDocument);


                        if (String.IsNullOrEmpty(documentFiles.Title))
                            command.Parameters.AddWithValue("@Title", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Title", documentFiles.Title);
                        if (String.IsNullOrEmpty(documentFiles.FileDocument))
                            command.Parameters.AddWithValue("@FileDocument", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@FileDocument", documentFiles.FileDocument);
                        if (String.IsNullOrEmpty(documentFiles.FileFormat))
                            command.Parameters.AddWithValue("@FileFormat", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@FileFormat", documentFiles.FileFormat);
                        if (String.IsNullOrEmpty(documentFiles.StartPage))
                            command.Parameters.AddWithValue("@StartPage", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@StartPage", documentFiles.StartPage);
                        if (String.IsNullOrEmpty(documentFiles.EndPage))
                            command.Parameters.AddWithValue("@EndPage", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@EndPage", documentFiles.EndPage);



                        command.Parameters.AddWithValue("@IdDocumentFiles", documentFiles.IdDocumentFiles);

                        command.ExecuteNonQuery();
                        if (CountDocumentFilesUnicity(documentFiles.Title) > 1)
                        {
                            DeleteDocumentFilesBy("IdDocumentFiles", documentFiles.IdDocumentFiles.ToString());
                            AddDocumentFiles(ancienDocumentFiles);
                            jr.message = "Title existe déjà !";
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
       
        public static JsonResponse DeleteDocumentFilesBy(string Field, string Value)
        {
            JsonResponse Message = new JsonResponse();
            Message.success = false;
            Message.message = "Erreur";
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "delete from DocumentFiles where [" + Field + "]=@Field";
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
