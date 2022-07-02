using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPanel.Entities
{
    [Table("UserAccounts")]
    public class UserAccount : BaseModel
    {
        [Key]
        public Guid UserAccountId { get; set; }

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [Required(ErrorMessage ="Account number is required")]
        public string AccountNumber { get; set; }
    }
}
