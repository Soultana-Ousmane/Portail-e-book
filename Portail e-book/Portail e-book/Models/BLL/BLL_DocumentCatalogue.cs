using Portail_e_book.Models.DAL;
using Portail_e_book.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Models.BLL
{
    public class BLL_DocumentCatalogue
    {
        public static IEnumerable<DocumentCatalogue> getAllDocumentCatalogue()
        {
            return DAL_DocumentCatalogue.getAllDocumentCatalogue();
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        
        public static DocumentCatalogue getDocumentCatalogueBy(string Champ, string Valeur)
        {
            return DAL_DocumentCatalogue.getDocumentCatalogueBy(Champ, Valeur);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        public static IEnumerable<DocumentCatalogue> getAllDocumentCatalogueBy(string Field, string Value)
        {
            return DAL_DocumentCatalogue.getAllDocumentCatalogueBy(Field, Value);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        
        public static JsonResponse AddDocumentCatalogue(DocumentCatalogue DocumentCatalogue)
        {
            return DAL_DocumentCatalogue.AddDocumentCatalogue (DocumentCatalogue);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        
        public static JsonResponse UpdateDocumentCatalogue(DocumentCatalogue documentCatalogue)
        {
            return DAL_DocumentCatalogue.UpdateDocumentCatalogue(documentCatalogue);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
     
        public static JsonResponse DeleteDocumentCatalogue(int? idDocumentCatalogue)
        {
            return DAL_DocumentCatalogue.DeleteDocumentCatalogueBy("IdDocumentCatalogue", idDocumentCatalogue.ToString());
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        
        public static DocumentCatalogue Upsert(int? idDocumentCatalogue)
        {
            if (idDocumentCatalogue == null)
            {
                //create
                return new DocumentCatalogue();
            }
            else
            {
                //Update
                return getDocumentCatalogueBy("IdDocumentCatalogue", idDocumentCatalogue.ToString());
            }
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------*/
        #region API Calls
        public static JsonResponse UpsertApi(DocumentCatalogue documentCatalogue)
        {
            JsonResponse jr = new JsonResponse();
            jr.success = false;
            jr.message = "Erreur";
            if (documentCatalogue.IdDocumentCatalogue == 0)
            {
                //create
                jr = AddDocumentCatalogue(documentCatalogue);
            }
            else
            {
                //update
                jr = UpdateDocumentCatalogue(documentCatalogue);
            }
            return jr;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------*/
        public static JsonResponse DeleteApi(int idDocumentCatalogue)
        {
            JsonResponse jr = new JsonResponse();
            var DocumentCatalogueFromDb = getDocumentCatalogueBy("IdDocumentCatalogue", idDocumentCatalogue.ToString());
            if (DocumentCatalogueFromDb == null)
            {
                jr.success = false;
                jr.message = "Error while Deleting";
                return jr;
            }
            else
            {
                jr = DeleteDocumentCatalogue(idDocumentCatalogue);
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
