﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NursingChecklistManager.Models
{
    public class ChecklistLineItem
    {
        [Key]
        public int ChecklistLineItemId { get; set; }

        [Required]
        public string ActionToDo { get; set; }

        [Required]
        public bool Completed { get; set;}

        public virtual ICollection<Checklist> Checklists { get; set; }

    }
}
