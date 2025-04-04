using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pryce_MVC.Models
{
    public class Address_Master
    {
        [Key]
       
        public int Trans_Id { get; set; }

        public int? Address_Category_Id { get; set; }

        [MaxLength(250)]
        public string? Address_Name { get; set; }

        [MaxLength(255)]
        public string? Address { get; set; }

        [MaxLength(50)]
        public string? Street { get; set; }

        [MaxLength(50)]
        public string? Block { get; set; }

        [MaxLength(50)]
        public string? Avenue { get; set; }

        public int CountryId { get; set; }

        [MaxLength(50)]
        public string? StateId { get; set; }

        [MaxLength(50)]
        public string CityId { get; set; }

        [MaxLength(15)]
        public string PinCode { get; set; }

        [Required, MaxLength(50)]
        public string PhoneNo1 { get; set; }

        [MaxLength(50)]
        public string PhoneNo2 { get; set; }

        [Required, MaxLength(50)]
        public string MobileNo1 { get; set; }

        [MaxLength(50)]
        public string MobileNo2 { get; set; }

        [Required, MaxLength(50)]
        public string EmailId1 { get; set; }

        [MaxLength(50)]
        public string EmailId2 { get; set; }

        [MaxLength(50)]
        public string FaxNo { get; set; }

        [MaxLength(50)]
        public string WebSite { get; set; }

        public decimal? Longitude { get; set; }

        public decimal? Latitude { get; set; }

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

        [Required, MaxLength(11)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required, MaxLength(11)]
        public string ModifiedBy { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}
