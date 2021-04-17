using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreProject.Models
{
    public class StockModel
    {
        public Guid ID { get; set; }
        public int CurrentQty { get; set; }
        public int LockedQty { get; set; }
        public DateTime? ModifyDate { get; set; }
        public Guid? Modifier { get; set; }
    }
}
