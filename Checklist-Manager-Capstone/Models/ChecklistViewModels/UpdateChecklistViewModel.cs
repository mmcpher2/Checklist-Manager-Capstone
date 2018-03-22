﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NursingChecklistManager.Models.ChecklistViewModels
{
    public class UpdateChecklistViewModel
    {

        public string Title { get; set; }

        public List<LineItemJoinerModel> LineItemJoiners { get; set; }
        
    }
}
