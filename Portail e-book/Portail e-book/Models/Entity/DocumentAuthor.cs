using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Models.Entity
{
    public class DocumentAuthor
    {
        [Required]
        public int IdDocumentAuthor { get; set; }
        public int IdAuthor { get; set; }
        public int IdDocument { get; set; }
        public string Role { get; set; }

        public DocumentAuthor()
        { }

        public DocumentAuthor(int IdAuthor, int IdDocument, string Role)
        {
            this.IdAuthor = IdAuthor;
            this.IdDocument = IdDocument;
            this.Role = Role;
        }
    }
}
