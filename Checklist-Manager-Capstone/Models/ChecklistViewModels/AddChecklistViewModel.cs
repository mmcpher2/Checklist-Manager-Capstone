using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NursingChecklistManager.Models.ChecklistViewModels

{
    public class CreateChecklistViewModel
    {
        [Required]
        [Display(Name = "Add Procedure Name")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Add a Step")]
        public string LineItem { get; set; }

        public virtual ICollection<ChecklistLineItemModel> ChecklistLineItems { get; set; }

    }
}
