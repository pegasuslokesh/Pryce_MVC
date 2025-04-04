using System.ComponentModel.DataAnnotations;

namespace Pryce_MVC.Models
{
    public class IT_ObjectEntry
    {
        [Key]
        public int Object_Id { get; set; }  // Primary Key (Assumed NOT NULL)

        public string? Object_Name { get; set; }   // Allow null
        public string? Object_Name_L { get; set; } // Allow null
        public string? Page_Url { get; set; }      // Allow null
        public int? Order_Appear { get; set; }     // Allow null

        public string? Form_Type { get; set; }     // Allow null
        public bool? IsActive { get; set; }        // Allow null (if SQL allows)

        public string? CreatedBy { get; set; }     // Allow null
        public DateTime? CreatedDate { get; set; } // Allow null
        public string? ModifiedBy { get; set; }    // Allow null
        public DateTime? ModifiedDate { get; set; } // Allow null

        public int? Approval_id { get; set; }          // Allow null
        public int? notification_type_id { get; set; } // Allow null
        public bool? ShowInNavigationMenu { get; set; } // Allow null
    }
}
