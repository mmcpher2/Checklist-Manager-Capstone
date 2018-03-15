using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NursingChecklistManager.Models
{
    public class ChecklistTitle
    {
        [Key]
        public int ChecklistTitleId { get; set; }

        [Required]
        public string Title { get; set; }

    }
}
