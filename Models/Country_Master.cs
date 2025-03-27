using System.ComponentModel.DataAnnotations;

namespace Pryce_MVC.Models
{
    public class Country_Master
    {
        [Key]
        public long CountryId { get; set; }
        public string Country_Name { get; set; }

        // Navigation property for related Religions
        public ICollection<Religion_Master> Religions { get; set; }
    }
}
