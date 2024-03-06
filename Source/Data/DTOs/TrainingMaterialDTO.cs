using System.Collections.Generic;
using System.Web;
using Utility.Enums;

namespace Data.DTOs
{
    public class TrainingMaterialDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleH { get; set; }
        public string ShortDescription { get; set; }
        public string ShortDescriptionH { get; set; }
        public string Description { get; set; }
        public string DescriptionH { get; set; }
        public string Link { get; set; }
        public string Attachment { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public System.DateTime PublishDate { get; set; }
        public HttpPostedFileBase postedImage { get; set; }
        public HttpPostedFileBase postedFile { get; set; }
        public List<TrainingMaterial> lst { get; set; }
        public TrainingMaterial trMaterial { get; set; }
    }

    public class UpdateTrainingMaterialDTO : TrainingMaterialDTO
    { 
        
    }
    public class TrainingMaterialDTOMobile
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Attachment { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public System.DateTime PublishDate { get; set; }
    }
    public class TrainingMaterialRequestDTO : UserLogDTO
    {
        public Language Language { get; set; }
    }
    public class TrainingMaterialResponseDTO
    {
        public List<TrainingMaterialDTOMobile> lstTMaterial { get; set; }
    }

}
