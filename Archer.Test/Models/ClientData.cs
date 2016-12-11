using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Archer.Test.Models
{
    [Table("ClientData")]
    public class ClientData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string CellNumber { get; set; }

        [StringLength(255)]
        public string EmailAddress { get; set; }

        public bool IsValid { get; set; }

        [ForeignKey("Client")]
        public int ClientID { get; set; }

        public Client Client { get; set; }
    }
}
