using System.ComponentModel.DataAnnotations;

namespace Pryce_MVC.Models
{


    public class IT_ModuleMaster
    {
        [Key]
        public int Module_Id { get; set; }

        public string? Module_Name { get; set; }
        public string? Module_Name_L { get; set; }

        public int? Sequence_No { get; set; }  // Nullable INT (Fix!)

        public string? Module_Image { get; set; }
        public string? Module_Banner { get; set; }
        public string? DashBoard_Url { get; set; }
        public string? DashBoardIconUrl { get; set; }

        public string? Field1 { get; set; }
        public string? Field2 { get; set; }
        public string? Field3 { get; set; }
        public string? Field4 { get; set; }
        public string? Field5 { get; set; }

        public bool? Field6 { get; set; }  // Nullable Boolean (Fix!)

        public DateTime? Field7 { get; set; }
        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }  // Nullable DateTime (Fix!)

        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }








}
