using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NursingChecklistManager.Models
{
    public class Checklist
    {
        [Key]
        public int CheckListId { get; set; }

        [Required]
        public int ChecklistTitleId { get; set; }
        public ChecklistTitle ChecklistTitle { get; set; }

        [Required]
        public int ChecklistLineItemId { get; set; }
        public ChecklistLineItem ChecklistLineItems { get; set; }

        public virtual ICollection<UserChecklist> UserChecklists { get; set; }
    }
}
