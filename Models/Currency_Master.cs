using System.ComponentModel.DataAnnotations;

namespace Pryce_MVC.Models
{
    public class Currency_Master
    {
        [Key]
        public int Currency_ID { get; set; }

       
        public string Currency_Code { get; set; }

        [Required]
       
        public string Currency_Name { get; set; }

        
        public string Currency_Name_L { get; set; }

        
        public string Currency_Symbol { get; set; }

        
        public bool Is_BaseCurrency { get; set; }

        
        [Range(0, (double)decimal.MaxValue)]
        public decimal Currency_Value { get; set; }

       
        public string Field1 { get; set; }

        
        public string Field2 { get; set; }

       
        public string Field3 { get; set; }

        public string Field4 { get; set; }

        
        public string Field5 { get; set; }

        public bool? Field6 { get; set; }

        public DateTime? Field7 { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        
        public string ModifiedBy { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}
