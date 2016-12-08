using Archer.Test.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archer.Test.Models
{
    
    public class DataMap
    {
        [Key]
        public int MappingKey { get; set; }
        public DataMapTypes Type { get; set; }




    }
}
