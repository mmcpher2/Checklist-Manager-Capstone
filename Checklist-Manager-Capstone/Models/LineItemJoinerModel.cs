using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NursingChecklistManager.Models
{
    public class LineItemJoinerModel
    {
        [Key]
        public int LineItemJoinerId { get; set; }

        [Required]
        public int ChecklistId { get; set; }
        public ChecklistModel Checklists { get; set; }

        [Required]
        public int ChecklistLineItemId { get; set; }
        public ChecklistLineItemModel ChecklistLineItems { get; set; }

    }
}
