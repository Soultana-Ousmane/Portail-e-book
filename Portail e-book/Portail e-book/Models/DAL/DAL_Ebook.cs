using Portail_e_book.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Models.DAL
{
    public class DAL_Ebook : DAL_Document
    {
		protected static bool CheckEbooktUnicity(string ISBN)
		{
			int NbOccs = 0;
			try
			{
				using (SqlConnection Connection = DBConnection.GetConnection())
				{
					string StrSQL = "select * from Ebook where ISBN = @ISBN";
					SqlCommand Cmd = new SqlCommand(StrSQL, Connection);
					Cmd.Parameters.AddWithValue("@ISBN", ISBN);
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
		/*-------------------------------------------------------------------------------------------------------------------*/
		protected static int CountEbookUnicity(string ISBN)
		{
			int NbOccs = 0;
			try
			{
				using (SqlConnection Connection = DBConnection.GetConnection())
				{
					string StrSQL = "select * from Ebook where ISBN = @ISBN";
					SqlCommand Cmd = new SqlCommand(StrSQL, Connection);
					Cmd.Parameters.AddWithValue("@ISBN", ISBN);
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
				string sql = "If not exists (select * from sysobjects where name = 'Ebook')  CREATE TABLE [dbo].[Ebook] ([IdEbook]      BIGINT        IDENTITY (1, 1) NOT NULL,[EditionNum]   INT           NULL,[EditionPlace] NVARCHAR (50) NULL,[ISBN]         NVARCHAR (50) NULL,[Genre]        NVARCHAR (50) NULL,[Category]     NVARCHAR (50) NULL,[NbPages]      INT           NULL,CONSTRAINT [PK_Ebook_1] PRIMARY KEY CLUSTERED ([IdEbook] ASC));";
				using (SqlCommand command = new SqlCommand(sql, cnn))
					command.ExecuteNonQuery();
				cnn.Close();
			}
			catch { }
		}
		/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
		public static List<Ebook> getAllEbook()
		{

			List<Ebook> listEbook = new List<Ebook>();
			try
			{
				using (SqlConnection connection = DBConnection.GetConnection())
				{
					connection.Open();
					string sql = "select * from Ebook left join Document on Ebook.IdEbook=Document.IdDocument";//
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.CommandType = CommandType.Text;
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								Ebook ebook = new Ebook();

								ebook.IdEbook = Int32.Parse(dataReader["IdEbook"].ToString());
								ebook.EditionNum = Int32.Parse(dataReader["EditionNum"].ToString());
								ebook.EditionPlace = dataReader["EditionPlace"].ToString();
								ebook.ISBN = dataReader["ISBN"].ToString();
								ebook.Genre = dataReader["Genre"].ToString();
								ebook.Category = dataReader["Category"].ToString();
								ebook.NbPages = Int32.Parse(dataReader["NbPages"].ToString());

								listEbook.Add(ebook);

								//
								ebook.IdDocument = Int32.Parse(dataReader["IdDocument"].ToString());
								ebook.IdCollection = Int32.Parse(dataReader["IdCollection"].ToString());
								ebook.Editor = dataReader["Editor"].ToString();
								ebook.Doi = dataReader["Doi"].ToString();


								ebook.OriginalTitle = dataReader["OriginalTitle"].ToString();
								ebook.TitlesVariants = dataReader["TitlesVariants"].ToString();
								ebook.Subtitle = dataReader["Subtitle"].ToString();
								ebook.Foreword = dataReader["Foreword"].ToString();
								ebook.Keywords = dataReader["Keywords"].ToString();
								ebook.Fichier = dataReader["Fichier"].ToString();
								ebook.FileFormat = dataReader["FileFormat"].ToString();
								ebook.CoverPage = dataReader["CoverPage"].ToString();
								ebook.Url = dataReader["Url"].ToString();
								ebook.DocumentType = dataReader["DocumentType"].ToString();
								ebook.OriginalLanguage = dataReader["OriginalLanguage"].ToString();
								ebook.LanguagesVariants = dataReader["LanguagesVariants"].ToString();
								ebook.Translator = dataReader["Translator"].ToString();
								ebook.AccessType = dataReader["AccessType"].ToString();
								ebook.State = dataReader["State"].ToString();


								ebook.Price = Decimal.Parse(dataReader["Price"].ToString());
								ebook.SellingPrice = Decimal.Parse(dataReader["SellingPrice"].ToString());
								ebook.DigitalPrice = Decimal.Parse(dataReader["DigitalPrice"].ToString());
								if (dataReader["PublicationDate"].ToString() != "")
								{
									ebook.PublicationDate = DateTime.Parse(dataReader["PublicationDate"].ToString());
								}
								ebook.Country = dataReader["Country"].ToString();
								ebook.PhysicalDescription = dataReader["PhysicalDescription"].ToString();


								ebook.AccompanyingMaterials = dataReader["AccompanyingMaterials"].ToString();
								ebook.AccompanyingMaterialsNb = Int32.Parse(dataReader["AccompanyingMaterialsNb"].ToString());
								ebook.VolumeNb = Int32.Parse(dataReader["VolumeNb"].ToString());
								ebook.Abstract = dataReader["Abstract"].ToString();
								ebook.Notes = dataReader["Notes"].ToString();

								
							}


						}
					}
					connection.Close();
				}
			}
			catch { }

			return listEbook;
		}
		/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
		//Get the details of a particular Document
		public static Ebook getEbookBy(string Field, string Value)
		{
			Ebook ebook = new Ebook();
			try
			{
				using (SqlConnection connection = DBConnection.GetConnection())
				{
					connection.Open();

					string sql = "select * from Ebook left join Document on Ebook.IdEbook = Document.IdDocument where [" + Field + "]=@Field"; //

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.CommandType = CommandType.Text;
						command.Parameters.AddWithValue("@Field", Value);
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								ebook.IdEbook = Int32.Parse(dataReader["IdEbook"].ToString());
								ebook.EditionNum = Int32.Parse(dataReader["EditionNum"].ToString());
								ebook.EditionPlace = dataReader["EditionPlace"].ToString();
								ebook.ISBN = dataReader["ISBN"].ToString();
								ebook.Genre = dataReader["Genre"].ToString();
								ebook.Category = dataReader["Category"].ToString();
								ebook.NbPages = Int32.Parse(dataReader["NbPages"].ToString());

								//
								ebook.IdDocument = Int32.Parse(dataReader["IdDocument"].ToString());
								ebook.IdCollection = Int32.Parse(dataReader["IdCollection"].ToString());
								ebook.Editor = dataReader["Editor"].ToString();
								ebook.Doi = dataReader["Doi"].ToString();


								ebook.OriginalTitle = dataReader["OriginalTitle"].ToString();
								ebook.TitlesVariants = dataReader["TitlesVariants"].ToString();
								ebook.Subtitle = dataReader["Subtitle"].ToString();
								ebook.Foreword = dataReader["Foreword"].ToString();
								ebook.Keywords = dataReader["Keywords"].ToString();
								ebook.Fichier = dataReader["Fichier"].ToString();
								ebook.FileFormat = dataReader["FileFormat"].ToString();
								ebook.CoverPage = dataReader["CoverPage"].ToString();
								ebook.Url = dataReader["Url"].ToString();
								ebook.DocumentType = dataReader["DocumentType"].ToString();
								ebook.OriginalLanguage = dataReader["OriginalLanguage"].ToString();
								ebook.LanguagesVariants = dataReader["LanguagesVariants"].ToString();
								ebook.Translator = dataReader["Translator"].ToString();
								ebook.AccessType = dataReader["AccessType"].ToString();
								ebook.State = dataReader["State"].ToString();


								ebook.Price = Decimal.Parse(dataReader["Price"].ToString());
								ebook.SellingPrice = Decimal.Parse(dataReader["SellingPrice"].ToString());
								ebook.DigitalPrice = Decimal.Parse(dataReader["DigitalPrice"].ToString());
								if (dataReader["PublicationDate"].ToString() != "")
								{
									ebook.PublicationDate = DateTime.Parse(dataReader["PublicationDate"].ToString());
								}
								ebook.Country = dataReader["Country"].ToString();
								ebook.PhysicalDescription = dataReader["PhysicalDescription"].ToString();


								ebook.AccompanyingMaterials = dataReader["AccompanyingMaterials"].ToString();
								ebook.AccompanyingMaterialsNb = Int32.Parse(dataReader["AccompanyingMaterialsNb"].ToString());
								ebook.VolumeNb = Int32.Parse(dataReader["VolumeNb"].ToString());
								ebook.Abstract = dataReader["Abstract"].ToString();
								ebook.Notes = dataReader["Notes"].ToString();
							}


						}
					}
					connection.Close();
				}
			}
			catch { }
			return ebook;
		}
		/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
		public static List<Ebook> getAllEbookBy(string Field, string Value)
		{
			List<Ebook> listEbook = new List<Ebook>();
			try
			{
				using (SqlConnection connection = DBConnection.GetConnection())
				{
					connection.Open();
					string sql = "select * from Ebook left join Document on Ebook.IdEbook = Document.IdDocument where [" + Field + "]=@Field";//

					using (SqlCommand command = new SqlCommand(sql, connection))
					{

						command.CommandType = CommandType.Text;
						command.Parameters.AddWithValue("@Field", Value);
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								Ebook ebook = new Ebook();
								ebook.IdEbook = Int32.Parse(dataReader["IdEbook"].ToString());
								ebook.EditionNum = Int32.Parse(dataReader["EditionNum"].ToString());
								ebook.EditionPlace = dataReader["EditionPlace"].ToString();
								ebook.ISBN = dataReader["ISBN"].ToString();
								ebook.Genre = dataReader["Genre"].ToString();
								ebook.Category = dataReader["Category"].ToString();
								ebook.NbPages = Int32.Parse(dataReader["NbPages"].ToString());

								listEbook.Add(ebook);

								//
								ebook.IdDocument = Int32.Parse(dataReader["IdDocument"].ToString());
								ebook.IdCollection = Int32.Parse(dataReader["IdCollection"].ToString());
								ebook.Editor = dataReader["Editor"].ToString();
								ebook.Doi = dataReader["Doi"].ToString();


								ebook.OriginalTitle = dataReader["OriginalTitle"].ToString();
								ebook.TitlesVariants = dataReader["TitlesVariants"].ToString();
								ebook.Subtitle = dataReader["Subtitle"].ToString();
								ebook.Foreword = dataReader["Foreword"].ToString();
								ebook.Keywords = dataReader["Keywords"].ToString();
								ebook.Fichier = dataReader["Fichier"].ToString();
								ebook.FileFormat = dataReader["FileFormat"].ToString();
								ebook.CoverPage = dataReader["CoverPage"].ToString();
								ebook.Url = dataReader["Url"].ToString();
								ebook.DocumentType = dataReader["DocumentType"].ToString();
								ebook.OriginalLanguage = dataReader["OriginalLanguage"].ToString();
								ebook.LanguagesVariants = dataReader["LanguagesVariants"].ToString();
								ebook.Translator = dataReader["Translator"].ToString();
								ebook.AccessType = dataReader["AccessType"].ToString();
								ebook.State = dataReader["State"].ToString();


								ebook.Price = Decimal.Parse(dataReader["Price"].ToString());
								ebook.SellingPrice = Decimal.Parse(dataReader["SellingPrice"].ToString());
								ebook.DigitalPrice = Decimal.Parse(dataReader["DigitalPrice"].ToString());
								if (dataReader["PublicationDate"].ToString() != "")
								{
									ebook.PublicationDate = DateTime.Parse(dataReader["PublicationDate"].ToString());
								}
								ebook.Country = dataReader["Country"].ToString();
								ebook.PhysicalDescription = dataReader["PhysicalDescription"].ToString();


								ebook.AccompanyingMaterials = dataReader["AccompanyingMaterials"].ToString();
								ebook.AccompanyingMaterialsNb = Int32.Parse(dataReader["AccompanyingMaterialsNb"].ToString());
								ebook.VolumeNb = Int32.Parse(dataReader["VolumeNb"].ToString());
								ebook.Abstract = dataReader["Abstract"].ToString();
								ebook.Notes = dataReader["Notes"].ToString();

							}
						}
					}
					connection.Close();
				}
			}
			catch { }
			return listEbook;
		}
		/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
		//Add new Document Ebook
		public static JsonResponse AddEbook(Ebook ebook)
		{
			JsonResponse jr = new JsonResponse();
			jr.success = false;
			jr.message = "Erreur d'ajout";
			try
			{

				if (DAL_Document.AddDocument(ebook).success)
				{
					Document document = DAL_Document.getDocumentBy("Doi", ebook.Doi);
					ebook.IdEbook = document.IdDocument;



					CreateTable();
					using (SqlConnection connection = DBConnection.GetConnection())
					{
						connection.Open();
						if (CheckDocumentUnicity(ebook.ISBN) == false)
						{
							string sql = "Insert into Ejournal (EditionNum,EditionPlace,ISBN,Genre,Category,NbPages)values( @EditionNum,@EditionPlace,@ISBN,@Genre,@Category,@NbPages)";

							using (SqlCommand command = new SqlCommand(sql, connection))
							{
								command.CommandType = CommandType.Text;

							

								if ( ebook.EditionNum ==0)
									command.Parameters.AddWithValue("@EditionNum", DBNull.Value);
								else
									command.Parameters.AddWithValue("@EditionNum", ebook.EditionNum);
								if (String.IsNullOrEmpty(ebook.EditionPlace))
									command.Parameters.AddWithValue("@EditionPlace", DBNull.Value);
								else
									command.Parameters.AddWithValue("@EditionPlace", ebook.EditionPlace);

								if (String.IsNullOrEmpty(ebook.ISBN))
									command.Parameters.AddWithValue("@ISBN", DBNull.Value);
								else
									command.Parameters.AddWithValue("@ISBN", ebook.ISBN);
								if (String.IsNullOrEmpty(ebook.Genre))
									command.Parameters.AddWithValue("@Genre", DBNull.Value);
								else
									command.Parameters.AddWithValue("@Genre", ebook.Genre);
								if (String.IsNullOrEmpty(ebook.Category))
									command.Parameters.AddWithValue("@Category", DBNull.Value);
								else
									command.Parameters.AddWithValue("@Category", ebook.Category);
								if ((ebook.NbPages) == 0)
									command.Parameters.AddWithValue("@NbPages", DBNull.Value);
								else
									command.Parameters.AddWithValue("@NbPages", ebook.NbPages);




								if (command.ExecuteNonQuery() == 1)
								{
									jr.success = true;
									jr.message = "Ajout effectué";
								}
							}
						}
						else
						{
							jr.message = "ISBN existe déjà !";
						}
						connection.Close();
					}


				}

			}
			catch (Exception e)
			{
				jr.message = "Erreur : " + e.Message;
			}
			return jr;
		}
		/*-------------------------------------------------------------------------------------------------------------------------------------------*/
		//Update Document
		public static JsonResponse UpdateEbook(Ebook ebook)
		{
			JsonResponse jr = new JsonResponse();
			jr.success = false;
			jr.message = "Erreur";
			Ebook ancienEbook = getEbookBy("Id", ebook.IdEbook.ToString());
			try
			{
				if (DAL_Document.UpdateDocument(ebook).success)
				{
					Document document = DAL_Document.getDocumentBy("Doi", ebook.Doi);
					ebook.IdEbook = document.IdDocument;

					using (SqlConnection connection = DBConnection.GetConnection())
					{
						connection.Open();
						string sql = "Update Ebook set EditionNum=@EditionNum,EditionPlace=@EditionPlace,ISBN=@ISBN,Genre=@Genre,Category=@Category,NbPages=@NbPages  where IdEbook=@IdEbook;";

						using (SqlCommand command = new SqlCommand(sql, connection))
						{
							command.CommandType = CommandType.Text;
						
							if (ebook.EditionNum==0)
								command.Parameters.AddWithValue("@EditionNum", DBNull.Value);
							else
								command.Parameters.AddWithValue("@EditionNum", ebook.EditionNum);
							if (String.IsNullOrEmpty(ebook.EditionPlace))
								command.Parameters.AddWithValue("@EditionPlace", DBNull.Value);
							else
								command.Parameters.AddWithValue("@EditionPlace", ebook.EditionPlace);

							if (String.IsNullOrEmpty(ebook.ISBN))
								command.Parameters.AddWithValue("@ISBN", DBNull.Value);
							else
								command.Parameters.AddWithValue("@ISBN", ebook.ISBN);
							if (String.IsNullOrEmpty(ebook.Genre))
								command.Parameters.AddWithValue("@Genre", DBNull.Value);
							else
								command.Parameters.AddWithValue("@Genre", ebook.Genre);
							if (String.IsNullOrEmpty(ebook.Category))
								command.Parameters.AddWithValue("@Category", DBNull.Value);
							else
								command.Parameters.AddWithValue("@Category", ebook.Category);
							if ((ebook.NbPages) == 0)
								command.Parameters.AddWithValue("@NbPages", DBNull.Value);
							else
								command.Parameters.AddWithValue("@NbPages", ebook.NbPages);



							command.Parameters.AddWithValue("@IdEbook", ebook.IdEbook);

							command.ExecuteNonQuery();
							if (CountEbookUnicity(ebook.ISBN) > 1)
							{
								DeleteEbookBy("IdEbook", ebook.IdEbook.ToString());
								AddEbook(ancienEbook);
								jr.message = "ISBN existe déjà !";
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
			}
			catch (Exception e)
			{
				jr.message = "Erreur : " + e.Message;
			}
			return jr;
		}
		/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
		//Delete Ebook
		public static JsonResponse DeleteEbookBy(string Field, string Value)
		{
			JsonResponse jr = new JsonResponse();
			jr.success = false;
			jr.message = "Erreur";
			try
			{
				using (SqlConnection connection = DBConnection.GetConnection())
				{
					connection.Open();
					string sql = "delete from Ebook where [" + Field + "]=@Field";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.CommandType = CommandType.Text;
						command.Parameters.AddWithValue("@Field", Value);
						if (command.ExecuteNonQuery() == 1)
						{
							jr.success = true;
							jr.message = "Suppression Effectuée";
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
