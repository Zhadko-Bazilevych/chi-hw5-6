using System;
using System.Collections.Generic;

namespace HW5_6.Models
{
    public partial class Group
    {
        public Group()
        {
            Analyses = new HashSet<Analysis>();
        }

        public int GrId { get; set; }
        public string GrName { get; set; } = null!;
        public decimal GrTemp { get; set; }

        public virtual ICollection<Analysis> Analyses { get; set; }
    }
}
