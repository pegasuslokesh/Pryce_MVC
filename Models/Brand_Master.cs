using System.ComponentModel.DataAnnotations;

namespace Pryce_MVC.Models
{
    public class Brand_Master
    {
        
        public int Company_Id { get; set; }
        [Key]
        public int Brand_Id { get; set; }
        public string Brand_Name { get; set; }
        public string Brand_Name_L { get; set; }
        public string Brand_Code { get; set; }
        public int? Emp_Id { get; set; }
        public string Logo_Path { get; set; }
        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }
        public string Field4 { get; set; }
        public string Field5 { get; set; }
        public bool? Field6 { get; set; }
        public DateTime? Field7 { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
