using System.ComponentModel.DataAnnotations;

namespace Pryce_MVC.Models
{
    public class Company_Master
    {
        [Key]
        public int Company_Id { get; set; }

        public string? Company_Code { get; set; } // Nullable string

        public int? Parent_Company_Id { get; set; } // Nullable int

        public string? Company_Name { get; set; } // Nullable string

        public string Company_Name_L { get; set; } // Nullable string

        public string? Logo_Path { get; set; } // Nullable string

        public int? Emp_Id { get; set; } // Nullable int

        public int? Currency_Id { get; set; } // Nullable int

        public int? Country_Id { get; set; } // Nullable int

        public string? Commerical_License_No { get; set; } // Nullable string

        public string? Field1 { get; set; }
        public string? Field2 { get; set; }
        public string? Field3 { get; set; }
        public string? Field4 { get; set; }
        public string? Field5 { get; set; }

        public bool? Field6 { get; set; } // Nullable bool

        public DateTime? Field7 { get; set; } // Nullable DateTime

        public bool IsActive { get; set; } // Keep as non-nullable if always required

        public string? CreatedBy { get; set; } // Nullable string
        public DateTime? CreatedDate { get; set; } // Nullable DateTime

        public string? ModifiedBy { get; set; } // Nullable string
        public DateTime? ModifiedDate { get; set; } // Nullable DateTime

        public string? ParentCompany { get; set; } // Nullable string
    }
}
