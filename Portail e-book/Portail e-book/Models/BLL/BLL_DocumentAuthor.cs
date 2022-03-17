using Portail_e_book.Models.DAL;
using Portail_e_book.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Models.BLL
{
    public class BLL_DocumentAuthor
    {
        public static IEnumerable<DocumentAuthor> getAllDocumentAuthor()
        {
            return DAL_DocumentAuthor.getAllDocumentAuthor();
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        //Get the details of a particular DocumentAuthor
        public static DocumentAuthor getDocumentAuthorBy(string Champ, string Valeur)
        {
            return DAL_DocumentAuthor.getDocumentAuthorBy(Champ, Valeur);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        public static IEnumerable<DocumentAuthor> getAllDocumentAuthorBy(string Field, string Value)
        {
            return DAL_DocumentAuthor.getAllDocumentAuthorBy(Field, Value);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        //Add new Collection record
        public static JsonResponse AddDocumentAuthor(DocumentAuthor documentAuthor)
        {
            return DAL_DocumentAuthor.AddDocumentAuthor(documentAuthor);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        //Update the records of a particluar DocumentAuthor
        public static JsonResponse UpdateDocumentAuthor(DocumentAuthor documentAuthor)
        {
            return DAL_DocumentAuthor.UpdateDocumentAuthor(documentAuthor);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        //Delete the records of a particular DocumentAuthor
        public static JsonResponse DeleteDocumentAuthor(int? idDocumentAuthor)
        {
            return DAL_DocumentAuthor.DeleteDocumentAuthorBy("IdDocumentAuthor", idDocumentAuthor.ToString());
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        //Create or Update DocumentAuthor
        public static DocumentAuthor Upsert(int? idDocumentAuthor)
        {
            if (idDocumentAuthor == null)
            {
                //create
                return new DocumentAuthor();
            }
            else
            {
                //Update
                return getDocumentAuthorBy("IdDocumentAuthor", idDocumentAuthor.ToString());
            }
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------*/
        #region API Calls
        public static JsonResponse UpsertApi(DocumentAuthor documentAuthor)
        {
            JsonResponse jr = new JsonResponse();
            jr.success = false;
            jr.message = "Erreur";
            if (documentAuthor.IdDocumentAuthor == 0)
            {
                //create
                jr = AddDocumentAuthor(documentAuthor);
            }
            else
            {
                //update
                jr = UpdateDocumentAuthor(documentAuthor);
            }
            return jr;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------*/
        public static JsonResponse DeleteApi(int idDocumentAuthor)
        {
            JsonResponse jr = new JsonResponse();
            var DocumentAuthorFromDb = getDocumentAuthorBy("IdDocumentAuthor", idDocumentAuthor.ToString());
            if (DocumentAuthorFromDb == null)
            {
                jr.success = false;
                jr.message = "Error while Deleting";
                return jr;
            }
            else
            {
                jr = DeleteDocumentAuthor(idDocumentAuthor);
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
