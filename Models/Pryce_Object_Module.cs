//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace Pryce_MVC.Models
//{
//    public class Pryce_Object_Module
//    {
//        [Key]
//        public int ObjectModule_id { get; set; } // Primary Key

//        [Required]
//        public string ObjectModule_name { get; set; } = string.Empty; // Module Name

//        [Required]
//        public string ObjectModule_Url { get; set; } = string.Empty; // URL

//        // Foreign Key linking to Master Module
//        [ForeignKey("MasterModule")]
//        public int Module_id { get; set; }

//        // Navigation property for Master Module
//        public Pryce_Master_Module? MasterModule { get; set; }
//    }
//}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pryce_MVC.Models
{
    public class Pryce_Object_Module
    {
        [Key]
        public int ObjectModule_id { get; set; } // Primary Key

        [Required]
        [MaxLength(255)]
        public string ObjectModule_name { get; set; } = string.Empty; // Object Name

        [Required]
        [MaxLength(255)]
        public string ObjectModule_Url { get; set; } = string.Empty; // URL Path

        // Foreign Key linking to Master Module
        [ForeignKey("MasterModule")]
        public int Module_id { get; set; }

        // Navigation property for Master Module
        public virtual Pryce_Master_Module? MasterModule { get; set; }
    }
}
