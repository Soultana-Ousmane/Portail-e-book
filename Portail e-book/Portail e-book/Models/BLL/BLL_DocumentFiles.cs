using Portail_e_book.Models.DAL;
using Portail_e_book.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Models.BLL
{
    public class BLL_DocumentFiles
    {
        public static IEnumerable<DocumentFiles> getAllDocumentFiles()
        {
            return DAL_DocumentFiles.getAllDocumentFiles();
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        public static DocumentFiles getDocumentFilesBy(string Champ, string Valeur)
        {
            return DAL_DocumentFiles.getDocumentFilesBy(Champ, Valeur);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        public static IEnumerable<DocumentFiles> getAllDocumentFilesBy(string Field, string Value)
        {
            return DAL_DocumentFiles.getAllDocumentFilesBy(Field, Value);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        public static JsonResponse AddDocumentFiles(DocumentFiles documentFiles)
        {
            return DAL_DocumentFiles.AddDocumentFiles(documentFiles);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        public static JsonResponse UpdateDocumentFiles(DocumentFiles documentFiles)
        {
            return DAL_DocumentFiles.UpdateDocumentFiles(documentFiles);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        public static JsonResponse DeleteDocumentFiles(int? idDocumentFiles)
        {
            return DAL_DocumentFiles.DeleteDocumentFilesBy("IdDocumentFiles", idDocumentFiles.ToString());
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        //Create or Update DocumentFiles
        public static DocumentFiles Upsert(int? idDocumentFiles)
        {
            if (idDocumentFiles == null)
            {
                //create
                return new DocumentFiles();
            }
            else
            {
                //Update
                return getDocumentFilesBy("IdDocumentFiles", idDocumentFiles.ToString());
            }
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------*/
        #region API Calls
        public static JsonResponse UpsertApi(DocumentFiles documentFiles)
        {
            JsonResponse jr = new JsonResponse();
            jr.success = false;
            jr.message = "Erreur";
            if (documentFiles.IdDocumentFiles == 0)
            {
                //create
                jr = AddDocumentFiles(documentFiles);
            }
            else
            {
                //update
                jr = UpdateDocumentFiles(documentFiles);
            }
            return jr;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------*/
        public static JsonResponse DeleteApi(int idDocumentFiles)
        {
            JsonResponse jr = new JsonResponse();
            var DocumentFilesFromDb = getDocumentFilesBy("IdDocumentFiles", idDocumentFiles.ToString());
            if (DocumentFilesFromDb == null)
            {
                jr.success = false;
                jr.message = "Error while Deleting";
                return jr;
            }
            else
            {
                jr = DeleteDocumentFiles(idDocumentFiles);
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
