using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Models.Entity
{
    public class DocumentCatalogue
    {
        [Required]
        public int IdDocumentCatalogue { get; set; }
        public int IdDocument { get; set; }
        public int IdCatalogue { get; set; }

        public DocumentCatalogue()
        { }

        public DocumentCatalogue(int IdDocument, int IdCatalogue)
        {
            this.IdDocument = IdDocument;
            this.IdCatalogue = IdCatalogue;
        }
    }
}
