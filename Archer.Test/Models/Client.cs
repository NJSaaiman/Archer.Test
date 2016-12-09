using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Archer.Test.Models
{
    public class Client
    {
        public Client()
        {
            Data = new List<Models.ClientData>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        public List<ClientData> Data { get; set; }
        [StringLength(255)]
        public string IOWorkerName { get; set; }
    }
}
