using System.ComponentModel.DataAnnotations;

namespace Pryce_MVC.Models
{
    public class IT_App_Mod_Object
    {
        [Key]
        public int Trans_Id { get; set; }
        public int Application_Id { get; set; }
        public int Module_Id { get; set; }
        public int Object_Id { get; set; }

    }
}
