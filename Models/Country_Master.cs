using System.ComponentModel.DataAnnotations;

namespace Pryce_MVC.Models
{
    public class Country_Master
    {
        [Key]
        public int Country_Id { get; set; }

        public string Country_Name { get; set; } = string.Empty;

        //public string? Country_Name_L { get; set; }

        public string Country_Code { get; set; } = string.Empty;

        public string? Field1 { get; set; }

        public string? Field2 { get; set; }

        public string? Field3 { get; set; }

        public string? Field4 { get; set; }

        public string? Field5 { get; set; }

        public bool? Field6 { get; set; }  // Nullable because it allows NULL in the database

        public DateTime? Field7 { get; set; } // Nullable because it allows NULL in the database

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        public string ModifiedBy { get; set; } = string.Empty;

        public DateTime ModifiedDate { get; set; }
    }
}
