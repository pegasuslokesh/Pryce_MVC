using System.ComponentModel.DataAnnotations;

namespace Pryce_MVC.Models
{
    public class Employee_Master
    {
        [Key]
        public int Emp_Id { get; set; }

        //public int Company_Id { get; set; }
        public int Brand_Id { get; set; }
        public int Location_Id { get; set; }    

        [Required, StringLength(11)]
        public string Emp_Code { get; set; }

       // [StringLength(50)]
        //public string Civil_Id { get; set; }

        [Required, StringLength(255)]
        public string Emp_Name { get; set; }
        [StringLength(255)]
        public string Field2 { get; set; }
        public int Department_Id { get; set; }

        //[StringLength(255)]
        //public string Emp_Name_L { get; set; }

        //[StringLength(255)]
        //public string Emp_Image { get; set; }


        //public int Designation_Id { get; set; }
        //public int Religion_Id { get; set; }
        //public int Nationality_Id { get; set; }
        //public int? Qualification_Id { get; set; }

        //public DateTime DOB { get; set; }
        //public DateTime DOJ { get; set; }

        //[StringLength(50)]
        //public string Emp_Type { get; set; }

        //public DateTime? Termination_Date { get; set; }

        //[Required, StringLength(1)]
        //public string Gender { get; set; }

        //[StringLength(255)]
        //public string Email_Id { get; set; }

        //[StringLength(255)]
        //public string Phone_No { get; set; }

        //[StringLength(255)]
        //public string Field1 { get; set; }



        //[StringLength(255)]
        //public string Field3 { get; set; }

        //[StringLength(255)]
        //public string Field4 { get; set; }

        //[StringLength(255)]
        //public string Field5 { get; set; }

        //public bool? Field6 { get; set; }
        //public DateTime? Field7 { get; set; }

        //public bool IsActive { get; set; }

        //[Required, StringLength(11)]
        //public string CreatedBy { get; set; }

        //public DateTime CreatedDate { get; set; }

        //[Required, StringLength(11)]
        //public string ModifiedBy { get; set; }

        //public DateTime ModifiedDate { get; set; }

        //[StringLength(255)]
        //public string company_phone_no { get; set; }

        //[StringLength(20)]
        //public string Pan { get; set; }

        //[StringLength(50)]
        //public string FatherName { get; set; }

        //public bool? IsMarried { get; set; }

        //[StringLength(20)]
        //public string DLNo { get; set; }

        //public int? Device_Group_Id { get; set; }   

        //[StringLength(50)]
        //public string Grade { get; set; }

        // public DateTime? CiviId_Expire { get; set; }

        //[StringLength(50)]
        //public string Passport_No { get; set; }

        //public DateTime? Passport_Expire { get; set; }

        //public string Previous_Balance_LeaveReport { get; set; }
    }
}
