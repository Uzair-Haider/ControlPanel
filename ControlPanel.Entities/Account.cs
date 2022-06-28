using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPanel.Entities
{
    [Table("Accounts")]
    public class Account : BaseModel
    {
        [Key]

        public Guid AccountId { get; set; }
        public string AccountName { get; set; }
    }
}
