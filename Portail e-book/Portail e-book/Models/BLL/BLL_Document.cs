using Portail_e_book.Models.DAL;
using Portail_e_book.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Models.BLL
{
    public class BLL_Document
    {
        public static IEnumerable<Document> getAllDocument()
        {
            return DAL_Document.getAllDocument();
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        //Get the details of a particular Document
        public static Document getDocumentBy(string Champ, string Valeur)
        {
            return DAL_Document.getDocumentBy(Champ, Valeur);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        public static IEnumerable<Document> getAllDocumentBy(string Field, string Value)
        {
            return DAL_Document.getAllDocumentBy(Field, Value);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        //Add new Document record
        public static JsonResponse AddDocument(Document d)
        {
            return DAL_Document.AddDocument(d);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        //Update the records of a particluar Document
        public static JsonResponse UpdateDocument(Document d)
        {
            return DAL_Document.UpdateDocument(d);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        //Delete the records of a particular Document
        public static JsonResponse DeleteDocument(int? idDocument)
        {
            /*----------------------------------*/
            List<DocumentAuthor> listDocumentAuthor = DAL_DocumentAuthor.getAllDocumentAuthorBy("IdDocument", idDocument.ToString());
            for (int i = 0; i < listDocumentAuthor.Count; i++)
            {
                BLL_DocumentAuthor.DeleteDocumentAuthor(listDocumentAuthor[i].IdDocumentAuthor);
            }

            List<DocumentFiles> listDocumentFiles = DAL_DocumentFiles.getAllDocumentFilesBy("IdDocument", idDocument.ToString());
            for (int i = 0; i < listDocumentFiles.Count; i++)
            {
                BLL_DocumentFiles.DeleteDocumentFiles(listDocumentFiles[i].IdDocumentFiles);
            }

            List<DocumentCatalogue> listDocumentCatalogue = DAL_DocumentCatalogue.getAllDocumentCatalogueBy("IdDocument", idDocument.ToString());
            for (int i = 0; i < listDocumentCatalogue.Count; i++)
            {
                BLL_DocumentCatalogue.DeleteDocumentCatalogue(listDocumentCatalogue[i].IdDocumentCatalogue);
            }
            /*------------------------------*/
            return DAL_Document.DeleteDocumentBy("IdDocument", idDocument.ToString());
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        //Create or Update Document
        public static Document Upsert(int? idDocument)
        {
            if (idDocument == null)
            {
                //create
                return new Document();
            }
            else
            {
                //Update
                return getDocumentBy("IdDocument", idDocument.ToString());
            }
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------*/
        #region API Calls
        public static JsonResponse UpsertApi(Document document)
        {
            JsonResponse jr = new JsonResponse();
            jr.success = false;
            jr.message = "Erreur";
            if (document.IdDocument == 0)
            {
                //create
                jr = AddDocument(document);
            }
            else
            {
                //update
                jr = UpdateDocument(document);
            }
            return jr;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------*/
        public static JsonResponse DeleteApi(int idDocument)
        {
            JsonResponse jr = new JsonResponse();
            var DocumentFromDb = getDocumentBy("IdDocument", idDocument.ToString());
            if (DocumentFromDb == null)
            {
                jr.success = false;
                jr.message = "Error while Deleting";
                return jr;
            }
            else
            {
                jr = DeleteDocument(idDocument);
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
