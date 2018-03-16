using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NursingChecklistManager.Models
{
    public class UserChecklistModel
    {
        [Key]
        public int UserChecklistId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public int LineItemJoinerId { get; set; }
        public LineItemJoinerModel Checklists { get; set; }
    }
}
