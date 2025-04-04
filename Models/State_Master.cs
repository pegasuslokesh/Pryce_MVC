using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pryce_MVC.Models
{
    public class State_Master
    {
        [Key]
       
        public int Trans_Id { get; set; }

        [Required]
        public int Country_Id { get; set; }

        [MaxLength(250)]
        public string State_Name { get; set; }

        [MaxLength(250)]
        public string State_Name_Local { get; set; }

        [MaxLength(255)]
        public string Field1 { get; set; }

        [MaxLength(255)]
        public string Field2 { get; set; }

        [MaxLength(255)]
        public string Field3 { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        [MaxLength(11)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        [MaxLength(11)]
        public string ModifiedBy { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }
        public string Country_Name { get; set; }
    }
}
