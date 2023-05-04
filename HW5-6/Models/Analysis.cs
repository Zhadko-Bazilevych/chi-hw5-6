﻿using System;
using System.Collections.Generic;

namespace HW5_6.Models
{
    public partial class Analysis
    {
        public Analysis()
        {
            Orders = new HashSet<Order>();
        }

        public int AnId { get; set; }
        public string AnName { get; set; } = null!;
        public decimal AnCost { get; set; }
        public decimal AnPrice { get; set; }
        public int AnGroup { get; set; }

        public virtual Group AnGroupNavigation { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
