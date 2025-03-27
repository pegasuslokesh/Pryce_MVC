//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace Pryce_MVC.Models
//{
//    public class Pryce_Master_Module
//    {
//        [Key]
//        public int Id { get; set; }

//        [Required]
//        public string Module_name { get; set; } = string.Empty; // Fix: Initialize to empty string
//        public int? SubModuleId { get; set; } // Nullable, NULL means it's a main module
//        public string Module_Url { get; set; } = string.Empty; // Fix: Initialize

//        public List<Pryce_Object_Module> ObjectModules { get; set; } = new(); // Fix: Initialize list
//    }
//}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pryce_MVC.Models
{
    public class Pryce_Master_Module
    {
        [Key]
        public int Id { get; set; } // Primary Key

        [Required]
        [MaxLength(255)]
        public string Module_name { get; set; } = string.Empty; // Module Name

        public int? SubModuleId { get; set; } // Nullable, NULL means it's a main module

        [Required]
        [MaxLength(255)]
        public string Module_Url { get; set; } = string.Empty; // URL Path

        // Navigation property for parent module
        [ForeignKey("SubModuleId")]
        public virtual Pryce_Master_Module? ParentModule { get; set; }

        // Navigation property for submodules
        public virtual ICollection<Pryce_Master_Module> SubModules { get; set; } = new List<Pryce_Master_Module>();

        // Navigation property for object modules
        public virtual ICollection<Pryce_Object_Module> ObjectModules { get; set; } = new List<Pryce_Object_Module>();
    }
}

