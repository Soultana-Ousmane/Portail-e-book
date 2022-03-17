using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Models.Entity
{
    public class DocumentFiles
    {
        [Required]
        public int IdDocumentFiles { get; set; }
        public int IdDocument  { get; set; }
        public string Title { get; set; }
        public string FileDocument { get; set; }
        public string FileFormat { get; set; }
        public string StartPage { get; set; }
        public string EndPage{ get; set; }

        public DocumentFiles()
        { }

        public DocumentFiles(string Title, string FileDocument, string FileFormat, string StartPage, string EndPage)
        {
            this.Title = Title;
            this.FileDocument = FileDocument;
            this.FileFormat = FileFormat;
            this.StartPage = StartPage;
            this.EndPage = EndPage;
        }
    }
}
