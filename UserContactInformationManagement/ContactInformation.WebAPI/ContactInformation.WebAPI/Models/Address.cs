using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ContactInformation.WebAPI.Models
{
    [Table("Addresses")]
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string AddressDescription { get; set; } = string.Empty;
        public string AddressType { get; set; } = string.Empty;
        [ForeignKey("ContactId")]
        public Contact? Contact { get; set; }
        public int ContactId { get; set; }

    }
}
