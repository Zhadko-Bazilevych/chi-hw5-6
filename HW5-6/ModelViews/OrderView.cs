using HW5_6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW5_6.ModelViews
{
    internal class OrderView
    {
        public int OrdId { get; set; }
        public DateTime OrdDatetime { get; set; }
        public string AnName { get; set; }


        public override string ToString()
        {
            return $"{OrdId}\t{OrdDatetime}\t{AnName}";
        }
    }
}
