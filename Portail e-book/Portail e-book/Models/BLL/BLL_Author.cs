using Portail_e_book.Models.DAL;
using Portail_e_book.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Models.BLL
{
    public class BLL_Author
    {
        public static IEnumerable<Author> getAllAuthor()
        {
            return DAL_Author.getAllAuthor();
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        //Get the details of a particular Author
        public static Author getAuthorBy(string Champ, string Valeur)
        {
            return DAL_Author.getAuthorBy(Champ, Valeur);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        public static IEnumerable<Author> getAllAuthorBy(string Field, string Value)
        {
            return DAL_Author.getAllAuthorBy(Field, Value);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        //Add new Author record
        public static JsonResponse AddAuthor(Author author)
        {
            return DAL_Author.AddAuthor(author);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        //Update the records of a particluar Author
        public static JsonResponse UpdateAuthor(Author author)
        {
            return DAL_Author.UpdateAuthor(author);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        //Delete the records of a particular Author
        public static JsonResponse DeleteAuthor(int? idAuthor)
        {
            /*----------------------------------*/
            List<DocumentAuthor> listDocumentAuthor = DAL_DocumentAuthor.getAllDocumentAuthorBy("IdAuthor", idAuthor.ToString());

            foreach (DocumentAuthor documentAuthor in listDocumentAuthor)
                BLL_DocumentAuthor.DeleteDocumentAuthor(documentAuthor.IdDocumentAuthor);
            
            /*------------------------------*/
            return DAL_Author.DeleteAuthorBy("IdAuthor", idAuthor.ToString());
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        //Create or Update Author
        public static Author Upsert(int? idAuthor)
        {
            if (idAuthor == null)
            {
                //create
                return new Author();
            }
            else
            {
                //Update
                return getAuthorBy("IdDocument", idAuthor.ToString());
            }
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------*/
        #region API Calls
        public static JsonResponse UpsertApi(Author author)
        {
            JsonResponse jr = new JsonResponse();
            jr.success = false;
            jr.message = "Erreur";
            if (author.IdAuthor == 0)
            {
                //create
                jr = AddAuthor(author);
            }
            else
            {
                //update
                jr = UpdateAuthor(author);
            }
            return jr;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------*/
        public static JsonResponse DeleteApi(int idAuthor)
        {
            JsonResponse jr = new JsonResponse();
            var AuthorFromDb = getAuthorBy("IdAuthor", idAuthor.ToString());
            if (AuthorFromDb == null)
            {
                jr.success = false;
                jr.message = "Error while Deleting";
                return jr;
            }
            else
            {
                jr = DeleteAuthor(idAuthor);
                if (jr.message == "1")
                {
                    jr.success = true;
                    jr.message = "Delete successful";
                }
                else
                {
                    jr.success = false;
                    jr.message = jr.message;
                }
            }
            return jr;
        }
        #endregion
    }
}
