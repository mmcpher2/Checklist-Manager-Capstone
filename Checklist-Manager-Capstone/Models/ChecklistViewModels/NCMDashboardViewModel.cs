﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NursingChecklistManager.Models.ChecklistViewModels
{
    public class NCMDashboardViewModel
    {
        public ICollection<UserChecklistModel> UserChecklists { get; set; }
    }
}
