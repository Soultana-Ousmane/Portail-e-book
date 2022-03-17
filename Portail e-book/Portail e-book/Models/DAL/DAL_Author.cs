using Portail_e_book.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Models.DAL
{
    public class DAL_Author
    {
        //CheckAuthorUnicity
        protected static bool CheckAuthorUnicity(string Orcld)
        {
            int NbOccs = 0;
            try
            {
                using (SqlConnection Connection = DBConnection.GetConnection())
                {
                    string StrSQL = "select * from Author where Orcld = @Orcld";
                    SqlCommand Cmd = new SqlCommand(StrSQL, Connection);
                    Cmd.Parameters.AddWithValue("@Orcld", Orcld);
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
        protected static int CountAuthorUnicity(string Orcld)
        {
            int NbOccs = 0;
            try
            {
                using (SqlConnection Connection = DBConnection.GetConnection())
                {
                    string StrSQL = "select * from Author where Orcld = @Orcld";
                    SqlCommand Cmd = new SqlCommand(StrSQL, Connection);
                    Cmd.Parameters.AddWithValue("@Orcld", Orcld);
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
                string sql = "If not exists (select * from sysobjects where name = 'Author') CREATE TABLE [dbo].[Author] ([IdAuthor]    BIGINT         IDENTITY (1, 1) NOT NULL,[Orcld]       NVARCHAR (50)  NULL,[FirstName]   NVARCHAR (50)  NULL,[LastName]    NVARCHAR (50)  NULL,[ArName]      NVARCHAR (50)  NULL,[DateOfBirth] DATETIME       NULL,[Civility]    NVARCHAR (50)  NULL,[City]        NVARCHAR (50)  NULL,[Adress]     NVARCHAR (50)  NULL,[PostalCode]  INT            NULL,[Country]     NVARCHAR (50)  NULL,[Position]    NVARCHAR (50)  NULL,[Email]       NVARCHAR (50)  NULL,[Biography]   NVARCHAR (MAX) NULL,[Phone]       NVARCHAR (50)  NULL,[Photo]       NVARCHAR (50)  NULL,CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED ([IdAuthor] ASC),CONSTRAINT [FK_Author_Author] FOREIGN KEY ([IdAuthor]) REFERENCES [dbo].[Author] ([IdAuthor]));";
                using (SqlCommand command = new SqlCommand(sql, cnn))
                    command.ExecuteNonQuery();
                cnn.Close();
            }
            catch { }
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        public static List<Author> getAllAuthor()
        {
            List<Author> listAuthor = new List<Author>();
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "select * from Author";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                Author author = new Author();

                                author.IdAuthor = Int32.Parse(dataReader["IdAuthor"].ToString());
                                author.Orcld = dataReader["Orcld"].ToString();
                                author.FirstName = dataReader["FirstName"].ToString();
                                author.LastName = dataReader["LastName"].ToString();
                                author.ArName = dataReader["ArName"].ToString();
                                if (dataReader["DateOfBirth"].ToString() != "")
                                {
                                    author.DateOfBirth = DateTime.Parse(dataReader["DateOfBirth"].ToString());
                                }
                                author.Civility = dataReader["Civility"].ToString();
                                author.City = dataReader["City"].ToString();
                                author.Adress = dataReader["Adress"].ToString();
                                author.PostalCode = Int32.Parse(dataReader["PostalCode"].ToString());
                                author.Country = dataReader["Country"].ToString();
                                author.Position = dataReader["Position"].ToString();
                                author.Email = dataReader["Email"].ToString();
                                author.Biography = dataReader["Biography"].ToString();
                                author.Phone = dataReader["Phone"].ToString();
                                author.Photo = dataReader["Photo"].ToString();

                                listAuthor.Add(author);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch { }
            return listAuthor;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        //Get the details of a particular Author
        public static Author getAuthorBy(string Field, string Value)
        {
            Author author = new Author();
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();

                    string sql = "select * from Author where [" + Field + "]=@Field"; 

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Field", Value);
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                author.IdAuthor = Int32.Parse(dataReader["IdAuthor"].ToString());
                                author.Orcld = dataReader["Orcld"].ToString();
                                author.FirstName = dataReader["FirstName"].ToString();
                                author.LastName = dataReader["LastName"].ToString();
                                author.ArName = dataReader["ArName"].ToString();
                                if (dataReader["DateOfBirth"].ToString() != "")
                                {
                                    author.DateOfBirth = DateTime.Parse(dataReader["DateOfBirth"].ToString());
                                }
                                author.Civility = dataReader["Civility"].ToString();
                                author.City = dataReader["City"].ToString();
                                author.Adress = dataReader["Adress"].ToString();
                                author.PostalCode = Int32.Parse(dataReader["PostalCode"].ToString());
                                author.Country = dataReader["Country"].ToString();
                                author.Position = dataReader["Position"].ToString();
                                author.Email = dataReader["Email"].ToString();
                                author.Biography = dataReader["Biography"].ToString();
                                author.Phone = dataReader["Phone"].ToString();
                                author.Photo = dataReader["Photo"].ToString();
                            }


                        }
                    }
                    connection.Close();
                }
            }
            catch { }
            return author;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        public static List<Author> getAllAuthorBy(string Field, string Value)
        {
            List<Author> listAuthor = new List<Author>();
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "select * from Author where [" + Field + "]=@Field";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Field", Value);
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                Author author = new Author();

                                author.IdAuthor = Int32.Parse(dataReader["IdAuthor"].ToString());
                                author.Orcld = dataReader["Orcld"].ToString();
                                author.FirstName = dataReader["FirstName"].ToString();
                                author.LastName = dataReader["LastName"].ToString();
                                author.ArName = dataReader["ArName"].ToString();
                                if (dataReader["DateOfBirth"].ToString() != "")
                                {
                                    author.DateOfBirth = DateTime.Parse(dataReader["DateOfBirth"].ToString());
                                }
                                author.Civility = dataReader["Civility"].ToString();
                                author.City = dataReader["City"].ToString();
                                author.Adress = dataReader["Adress"].ToString();
                                author.PostalCode = Int32.Parse(dataReader["PostalCode"].ToString());
                                author.Country = dataReader["Country"].ToString();
                                author.Position = dataReader["Position"].ToString();
                                author.Email = dataReader["Email"].ToString();
                                author.Biography = dataReader["Biography"].ToString();
                                author.Phone = dataReader["Phone"].ToString();
                                author.Photo = dataReader["Photo"].ToString();

                                listAuthor.Add(author);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch { }
            return listAuthor;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        //Add new Author record
        public static JsonResponse AddAuthor(Author author)
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
                    if (CheckAuthorUnicity(author.Orcld) == false)
                    {
                        string sql = "Insert into Author (Orcld, FirstName, LastName, ArName, DateOfBirth,Civility, City, Adress, PostalCode, Country, Position, Email, Biography, Phone, Photo )values(@Orcld, @FirstName, @LastName, @ArName, @DateOfBirth, @Civility, @City, @Adress, @PostalCode, @Country, @Position, @Email, @Biography, @Phone, @Photo )";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.CommandType = CommandType.Text;


                            if (String.IsNullOrEmpty(author.Orcld))
                                command.Parameters.AddWithValue("@Orcld", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Orcld", author.Orcld);
                            if (String.IsNullOrEmpty(author.FirstName))
                                command.Parameters.AddWithValue("@FirstName", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@FirstName", author.FirstName);
                            if (String.IsNullOrEmpty(author.LastName))
                                command.Parameters.AddWithValue("@LastName", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@LastName", author.LastName);
                            if (String.IsNullOrEmpty(author.ArName))
                                command.Parameters.AddWithValue("@ArName", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@ArName", author.ArName);
                            if (String.IsNullOrEmpty(author.DateOfBirth.ToString()))
                                command.Parameters.AddWithValue("@DateOfBirth", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@DateOfBirth", author.DateOfBirth);
                           


                            if (String.IsNullOrEmpty(author.Civility))
                                command.Parameters.AddWithValue("@Civility", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Civility", author.Civility);
                            if (String.IsNullOrEmpty(author.City))
                                command.Parameters.AddWithValue("@City", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@City", author.City);
                            if (String.IsNullOrEmpty(author.Adress))
                                command.Parameters.AddWithValue("@Adress", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Adress", author.Adress);


                            if (author.PostalCode ==0)
                                command.Parameters.AddWithValue("@PostalCode", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@PostalCode", author.PostalCode);
                            if (String.IsNullOrEmpty(author.Country))
                                command.Parameters.AddWithValue("@Country", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Country", author.Country);
                            if (String.IsNullOrEmpty(author.Position))
                                command.Parameters.AddWithValue("@Position", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Position", author.Position);

                            if (String.IsNullOrEmpty(author.Email))
                                command.Parameters.AddWithValue("@Email", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Email", author.Email);
                            if (String.IsNullOrEmpty(author.Biography))
                                command.Parameters.AddWithValue("@Biography", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Biography", author.Biography);
                            if (String.IsNullOrEmpty(author.Phone))
                                command.Parameters.AddWithValue("@Phone", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Phone", author.Phone);
                            if (String.IsNullOrEmpty(author.Photo))
                                command.Parameters.AddWithValue("@Photo", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Photo", author.Photo);


                            if (command.ExecuteNonQuery() == 1)
                            {
                                jr.success = true;
                                jr.message = "Ajout ok";
                            }
                        }
                    }
                    else
                    {
                        jr.message = "Orcld existe déjà !";
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
        //Update Author
        public static JsonResponse UpdateAuthor(Author author)
        {
            JsonResponse jr = new JsonResponse();
            jr.success = false;
            jr.message = "Erreur";
            Author ancienAuthor = getAuthorBy("IdAuthor", author.IdAuthor.ToString());
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "Update Author set Orcld=@Orcld, FirstName=@FirstName, LastName=@LastName, ArName=@ArName, DateOfBirth=@DateOfBirth, Civility=@Civility, City=@City, Adress=@Adress, PostalCode=@PostalCode, Country=@Country, Position=@Position, Email=@Email, Biography=@Biography, Phone=@Phone, Photo=@Photo   where IdAuthor=@IdAuthor";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;

                        if (String.IsNullOrEmpty(author.Orcld))
                            command.Parameters.AddWithValue("@Orcld", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Orcld", author.Orcld);
                        if (String.IsNullOrEmpty(author.FirstName))
                            command.Parameters.AddWithValue("@FirstName", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@FirstName", author.FirstName);
                        if (String.IsNullOrEmpty(author.LastName))
                            command.Parameters.AddWithValue("@LastName", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@LastName", author.LastName);
                        if (String.IsNullOrEmpty(author.ArName))
                            command.Parameters.AddWithValue("@ArName", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@ArName", author.ArName);
                        if (String.IsNullOrEmpty(author.DateOfBirth.ToString()))
                            command.Parameters.AddWithValue("@DateOfBirth", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@DateOfBirth", author.DateOfBirth);



                        if (String.IsNullOrEmpty(author.Civility))
                            command.Parameters.AddWithValue("@Civility", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Civility", author.Civility);
                        if (String.IsNullOrEmpty(author.City))
                            command.Parameters.AddWithValue("@City", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@City", author.City);
                        if (String.IsNullOrEmpty(author.Adress))
                            command.Parameters.AddWithValue("@Adress", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Adress", author.Adress);


                        if (author.PostalCode == 0)
                            command.Parameters.AddWithValue("@PostalCode", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@PostalCode", author.PostalCode);
                        if (String.IsNullOrEmpty(author.Country))
                            command.Parameters.AddWithValue("@Country", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Country", author.Country);
                        if (String.IsNullOrEmpty(author.Position))
                            command.Parameters.AddWithValue("@Position", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Position", author.Position);

                        if (String.IsNullOrEmpty(author.Email))
                            command.Parameters.AddWithValue("@Email", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Email", author.Email);
                        if (String.IsNullOrEmpty(author.Biography))
                            command.Parameters.AddWithValue("@Biography", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Biography", author.Biography);
                        if (String.IsNullOrEmpty(author.Phone))
                            command.Parameters.AddWithValue("@Phone", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Phone", author.Phone);
                        if (String.IsNullOrEmpty(author.Photo))
                            command.Parameters.AddWithValue("@Photo", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Photo", author.Photo);


                        command.Parameters.AddWithValue("@IdAuthor", author.IdAuthor);

                        command.ExecuteNonQuery();
                        if (CountAuthorUnicity(author.Orcld) > 1)
                        {
                            DeleteAuthorBy("IdAuthor", author.IdAuthor.ToString());
                            AddAuthor(ancienAuthor);
                            jr.message = "Orcld existe déjà !";
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
        //Delete Author
        public static JsonResponse DeleteAuthorBy(string Field, string Value)
        {
            JsonResponse jr = new JsonResponse();
            jr.success = false;
            jr.message = "Erreur";
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "delete from Author where [" + Field + "]=@Field";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Field", Value);
                        if (command.ExecuteNonQuery() == 1)
                        {
                            jr.success = true;
                            jr.message = "Suppression effectuée";
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
    }
}
