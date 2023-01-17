using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.ViewModel
{
    public class AddressModel
    {
        [Required]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public int Postalcode { get; set; }
        [Required]
        public string Country { get; set; }
    }
}
