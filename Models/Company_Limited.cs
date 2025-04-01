using System.ComponentModel.DataAnnotations;

namespace Pryce_MVC.Models
{
    public class Company_Limited
    {
        [Key]
        public int Company_Id { get; set; }
       // public string? Company_Code { get; set; }
        //public int? Parent_Company_Id { get; set; }
        public string? Company_Name { get; set; }
        //public string Company_Name_L { get; set; }
        public string? Logo_Path { get; set; }
        //public int? Emp_Id { get; set; }
        //public int? Currency_Id { get; set; }
       // public int? Country_Id { get; set; }
        // ... (other properties excluding Commerical_License_No) ...
        //public string? Field1 { get; set; }
       // public string? Field2 { get; set; }
        //public string? Field3 { get; set; }
       // public string? Field4 { get; set; }
       // public string? Field5 { get; set; }
        //public bool? Field6 { get; set; } // Nullable bool

        //public DateTime? Field7 { get; set; } // Nullable DateTime
        //public bool IsActive { get; set; }
        //public string? CreatedBy { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public string? ModifiedBy { get; set; }
        //public DateTime? ModifiedDate { get; set; }
        //public string? ParentCompany { get; set; }

    }
}
