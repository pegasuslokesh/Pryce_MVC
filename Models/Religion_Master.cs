using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pryce_MVC.Models
{
    public class Religion_Master
    {
        [Key]
       // public long Religion_Id { get; set; }
        public string Religion_Name { get; set; }
        public string Religion_L { get; set; }
        public string Religion { get; set; }
       // public int Religion_Id { get; set; }
        // Foreign Key to CountryMaster
        public long Country_Id { get; set; }
        public int Religion_Id { get; set; }
        //public bool Is_Active { get; set; }
        // Navigation property to related Country
        [NotMapped]
        public Country_Master Country { get; set; }

    }
}
