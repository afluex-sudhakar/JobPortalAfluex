using System.Collections.Generic;
namespace Data.DTOs
{
    public class DocumentTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DocumentType> lst { get; set; }
    }

    public class UpdateDocumentTypeDTO : DocumentTypeDTO
    { 
    }

}
