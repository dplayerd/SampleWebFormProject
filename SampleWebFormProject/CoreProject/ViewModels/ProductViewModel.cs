using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreProject.ViewModels
{
    public class ProductViewModel
    {
        public Guid ID { get; set; }
        public int ProductType { get; set; }
        public string Caption { get; set; }
        public decimal Price { get; set; }
        public string Body { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid Creator { get; set; }
        public DateTime? ModifyDate { get; set; }
        public Guid? Modifier { get; set; }


        #region "Join columns"
        public int CurrentQty { get; set; }
        public int LockedQty { get; set; }

        public string ModifierAccount { get; set; }
        #endregion
    }
}
