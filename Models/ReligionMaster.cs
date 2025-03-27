using System.ComponentModel.DataAnnotations;

namespace Pryce_MVC.Models
{
    public class ReligionMaster
    {
        [Key]
        public int Religion_Id { get; set; }
        public string Religion { get; set; }
        public string Religion_L { get; set; }
        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }
        public string Field4 { get; set; }
        public string Field5 { get; set; }
        public bool Field6 { get; set; }
        public DateTime Field7 { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        //public string Created_User { get; set; }  // Extra field for the joined table
        //public string Modified_User { get; set; } // Extra field for the joined table
    }

}
