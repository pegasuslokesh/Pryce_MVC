using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pryce_MVC.Models
{
    public class Set_AddressCategory
    {
        [Key]
       
        public int AddressCategoryId { get; set; }

        [MaxLength(255)]
        public string AddressName { get; set; }

        [MaxLength(255)]
        public string AddressNameL { get; set; }

        [MaxLength(255)]
        public string Field1 { get; set; }

        [MaxLength(255)]
        public string Field2 { get; set; }

        [MaxLength(255)]
        public string Field3 { get; set; }

        [MaxLength(255)]
        public string Field4 { get; set; }

        [MaxLength(255)]
        public string Field5 { get; set; }

        public bool? Field6 { get; set; }

        public DateTime? Field7 { get; set; }

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
    }
}
