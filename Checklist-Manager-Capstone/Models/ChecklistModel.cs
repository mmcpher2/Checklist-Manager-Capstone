using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NursingChecklistManager.Models
{
    public class ChecklistModel
    {
        [Key]
        public int CheckListId { get; set; }

        [Required]
        public string ChecklistTitle { get; set; }

        public virtual ICollection<LineItemJoinerModel> LineItems { get; set; }
    }
}
