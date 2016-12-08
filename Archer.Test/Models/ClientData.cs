using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Archer.Test.Models
{
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
    }
}
