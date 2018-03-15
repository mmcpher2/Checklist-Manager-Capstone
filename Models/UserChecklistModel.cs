using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NursingChecklistManager.Models
{
    public class UserChecklist
    {
        [Key]
        public int UserChecklistId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public int ChecklistId { get; set; }
        public Checklist Checklists { get; set; }
    }
}
