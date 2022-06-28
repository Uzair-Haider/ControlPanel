using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPanel.Entities
{
    [Table("Addresses")]
    public class Address:BaseModel
    {
        [Key]
        public Guid AddressId { get; set; }
        public string Address1 { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
    }
}
